using System;
using System.Text.RegularExpressions;

namespace Pascal_Interpreter
{
    public enum TokenType { INTEGER, TIMES, DIVIDE, ADD, SUBTRACT, OPERATOR, EOF}

    public class Interpreter
    {
        private readonly string Text;
        private int Pos;
        private Token CurrentToken;
        private char CurrentChar;
        

        public Interpreter(string Text)
        {
            this.Text = Text;
            Pos = 0;
            CurrentChar = Text[Pos];
            CurrentToken = GetNextToken();
            
        }

        public int Expr()
        {
            int result = Term();

            while(CurrentToken.Type!=TokenType.EOF)
            {
                if(CurrentToken.Type==TokenType.ADD)
                {
                    Eat(TokenType.ADD);
                    result += Term();
                } else if(CurrentToken.Type == TokenType.SUBTRACT) {
                    Eat(TokenType.SUBTRACT);
                    result -= Term();
                }
            }
            return result;

        }

        public int Term()
        {
            int result = Factor();

            while (CurrentToken.Type != TokenType.EOF)
            {
                if(CurrentToken.Type==TokenType.TIMES)
                {
                    Eat(TokenType.TIMES);
                    result *= Factor();
                } else if(CurrentToken.Type == TokenType.DIVIDE)
                {
                    Eat(TokenType.DIVIDE);
                    result /= Factor();
                } else
                {
                    break;
                }
            }
            return result;
        }

        public int Factor()
        {
            var token = CurrentToken;
            Eat(TokenType.INTEGER);
            return int.Parse(token.Value);
        }

        public int Integer()
        {
            string res = "";
            while(Char.IsDigit(CurrentChar))
            {
                res += CurrentChar;
                Advance();
            }
            return int.Parse(res);
        }

        private void Advance()
        {
            Pos++;
            if(Pos>=Text.Length)
            {
                CurrentChar = '\0';
            } else
            {
                CurrentChar = Text[Pos];
            }
        }
        

        public Token GetNextToken()
        {
            while (CurrentChar!='\0')
            {

                if("\n\t ".Contains(CurrentChar))
                {
                    Advance();
                    continue;
                }

                if(Char.IsDigit(CurrentChar))
                {
                    return new Token(TokenType.INTEGER, Integer().ToString());
                }

                if (CurrentChar == '*')
                {
                    Advance();
                    return new Token(TokenType.TIMES, "*");
                }

                if (CurrentChar == '/')
                {
                    Advance();
                    return new Token(TokenType.DIVIDE, "/");
                }

                if (CurrentChar == '+')
                {
                    Advance();
                    return new Token(TokenType.ADD, "+");
                }

                if (CurrentChar == '-')
                {
                    Advance();
                    return new Token(TokenType.SUBTRACT, "-");
                }

                throw new Exception("Error parsing input");

            }

            return new Token(TokenType.EOF, "\0");

        }

        private void Eat(TokenType NextTokenType)
        {
            if(CurrentToken.Type == NextTokenType)
            {
                
                CurrentToken = GetNextToken();
            } else
            {
                throw new Exception(String.Format("Token not recognised. Expected: {0} but got: {1}",NextTokenType,CurrentToken.Type));
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
