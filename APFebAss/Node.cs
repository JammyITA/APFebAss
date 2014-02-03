using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; //assert

namespace APFebAss
{
    class Node
    {
        public Token token;
        public Env env;

        public Node(Token t, Env e = null)
        {
            this.token = t;
            this.env = e;
        }
        
        public virtual bool eval()
        {
            if (token.Type == TokenType.BOOL)
                return ((WordToken)token).Lexeme == "true" ? true : false; 
            
            //never reached, because eval of a token is called only on a ide or a bool
            Debug.Assert(false, "Error: eval invoked on invalid token: " + token.Type);
            return false;
        }

        public virtual void compile(StringBuilder c)
        {
             if (token.Type == TokenType.BOOL)
                 c.Append(((WordToken)token).Lexeme);
        }
        
    }

    class LetNode : Node
    {
        IdeNode ide;
        Node definition;
        Node body;


        public LetNode(Token t, IdeNode ide, Node definition, Node body, Env e) 
            : base(t, e)
        {
            this.ide = ide;
            this.definition = definition;
            this.body = body;
        }

        public override bool eval()
        {
            return body.eval();
        }

        public override void compile(StringBuilder c)
        {
            c.AppendLine("{");
            c.AppendLine("bool " + ((WordToken)ide.token).Lexeme + " = " + definition.eval().ToString() + ";");
            body.compile(c);
            c.AppendLine("}");
        }
    }

    class IdeNode : Node
    {
        public IdeNode(Token t, Env e)
            : base(t, e)
        {
        }

        public override bool eval()
        {
            return base.env.get(base.token);
        }
    }

    class AndNode : Node
    {
        List<Node> nodes;

        public AndNode(Token t)
            :base(t)
        {
            nodes = new List<Node>();
        }

        public override bool eval()
        {
            foreach (Node n in nodes)
            {
                if (!n.eval())
                    return false;
            }
            return true;
        }

        public override void compile(StringBuilder c)
        {
            c.Append("result = result ");
            foreach (Node e in nodes)
                c.Append("&& " + e.eval().ToString());

            c.Append(";\n");
        }

        public void addExpression(Node e)
        {
            nodes.Add(e);
        }
    }
}
