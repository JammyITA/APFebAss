using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APFebAss
{
    enum TokenType { BOOL, IDE, LET, EQUAL, IN, AND, OPENB, CLOSEB, COMMA, EOF };

    class Token
    {
        TokenType type;
        public TokenType Type
        {
            get { return type; }
            //set { type = value; }
        }


        public Token(TokenType type)
        {
            this.type = type;

        }


        public static Token
            openb = new Token(TokenType.OPENB),
            closeb = new Token(TokenType.CLOSEB),
            comma = new Token(TokenType.COMMA),
            equals = new Token(TokenType.EQUAL),
            eof = new Token(TokenType.EOF);
    }

    //class BoolToken : Token
    //{
    //    bool value;
    //    public bool Value
    //    {
    //        get { return this.value; }
    //        //set { this.value = value; }
    //    }

    //    public BoolToken(bool value)
    //        : base(TokenType.BOOL)
    //    {
    //        this.value = value;
    //    }

    //    public static BoolToken
    //        tt = new BoolToken(true),
    //        ff = new BoolToken(false);
    //}

    //class IdeToken : Token 
    //{
    //    string value;
    //    public string Value
    //    {
    //        get { return this.value; }
    //    }

    //    public IdeToken(string value)
    //        : base(TokenType.IDE)
    //    {
    //        this.value = value;
    //    }
    //}

    class WordToken : Token
    {
        string lexeme;
        public string Lexeme
        {
            get { return this.lexeme; }
        }

        public WordToken(TokenType type, string value)
            : base(type)
        {
            this.lexeme = value;
        }

        public override string ToString()
        {
            return base.Type.ToString() + ":" + lexeme;
        }

        public static WordToken
            //let = new WordToken(TokenType.LET, "let"),
            //letin = new WordToken(TokenType.IN, "in"),
            //and = new WordToken(TokenType.AND, "and"),
            tt = new WordToken(TokenType.BOOL, "true"),
            ff = new WordToken(TokenType.BOOL, "false");

    }

}
