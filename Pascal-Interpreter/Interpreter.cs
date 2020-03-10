using System;
namespace Pascal_Interpreter
{
    public enum TokenType { INTEGER, PLUS, EOF}

    public class Interpreter
    {
        private readonly string Text;
        private int Pos;
        private Token CurrentToken;

        public Interpreter(string Text)
        {
            this.Text = Text;
            Pos = 0;
        }

        public int Expr()
        {
            CurrentToken = GetNextToken();

            var Left = CurrentToken;
            Eat(TokenType.INTEGER);

            var Op = CurrentToken;
            Eat(TokenType.PLUS);

            var Right = CurrentToken;
            Eat(TokenType.INTEGER);

            
            return Int32.Parse(Left.Value) + Int32.Parse(Right.Value);

        }

        private void SkipWhitespace()
        {
            if (Pos >= Text.Length)
            {
                return;
            }
            char CurrentChar = Text[Pos];
            while (char.IsWhiteSpace(CurrentChar) && Pos< Text.Length)
            {
                CurrentChar = Text[Pos];

                Pos++;

            }
        }

        public Token GetNextToken()
        {
            char CurrentChar;

            SkipWhitespace();

            if (Pos >= Text.Length)
                return new Token(TokenType.EOF, null);

            

            CurrentChar = Text[Pos];


            if(Char.IsDigit(CurrentChar))
            {
                string tmpStr="";

                while (Char.IsDigit(CurrentChar))
                {
                    tmpStr += CurrentChar.ToString();
                    Pos++;
                    Console.WriteLine(Pos);

                    if (Pos < Text.Length)
                        CurrentChar = Text[Pos];
                    else
                        break;
                }
                //Pos++;
                return new Token(TokenType.INTEGER, tmpStr);
            } else if(CurrentChar=='+')
            {
                Pos++;
                return new Token(TokenType.PLUS, CurrentChar.ToString());
            }

            throw new Exception("Error parsing input: " + (int)(CurrentChar));
        }

        private void Eat(TokenType NextTokenType)
        {
            
            if(CurrentToken.Type == NextTokenType)
            {
                
                CurrentToken = GetNextToken();
            } else
            {
                throw new Exception("Token not recognised. Expected {0} but got {1}.");
            }
        }

    }

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
