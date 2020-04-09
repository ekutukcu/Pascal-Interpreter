using System;

namespace Pascal_Interpreter
{
    public enum TokenType { INTEGER, TIMES, DIVIDE, ADD, SUBTRACT, OPERATOR, BRACKET,LBRACKET,RBRACKET, EOF }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Token(TokenType Type, string Value)
        {
            this.Type = Type;
            this.Value = Value;
        }

        public override string ToString() {
            return String.Format("Token({0},{1})", Type, Value);
        }
    }
}
 