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
        Token token;
        public Token Token
        { get { return this.token; } }

        Env env;
        public Env Env
        { get { return this.env; } }
        
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
                c.Append(((WordToken)token).Lexeme == "true" ? "True" : "False");
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

            c.AppendLine("bool " + ((WordToken)ide.Token).Lexeme + " = " + definition.eval().ToString() + ";");
            c.AppendLine("{");

            if (body.GetType() != typeof(LetNode))
                c.Append("result = result && ");

            body.compile(c);

            if (body.GetType() != typeof(LetNode))
                c.Append(";\n");

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
            return Env.get(Token);
        }

        public override void compile(StringBuilder c)
        {
            c.Append(((WordToken)Token).Lexeme);
        }
    }

    class AndNode : Node
    {
        List<Node> nodes;

        public AndNode(Token t)
            : base(t)
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

        
        //This code is a little tricky because I want to keep the structure of the imput file.
        //For achieve this result I created this code incrementally, starting with a simple expression.
        public override void compile(StringBuilder c)
        {
            
            c.Append("result ");
            foreach (Node e in nodes)
            {
                
                if (e.GetType() == typeof(LetNode))
                {
                    c.Append(";\n");
                    e.compile(c);
                    c.Append("result = result");
                }
                else
                {
                    c.Append(" && ");
                    e.compile(c);
                }
            }
        }

        public void addExpression(Node e)
        {
            nodes.Add(e);
        }
    }
}
