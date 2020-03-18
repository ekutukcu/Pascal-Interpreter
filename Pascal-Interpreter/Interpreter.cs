using System;
using System.Text.RegularExpressions;

namespace Pascal_Interpreter
{
    public enum TokenType { INTEGER, TIMES, DIVIDE, ADD, SUBTRACT, OPERATOR, BRACKET, EOF}

    public class Interpreter
    {
        private Token CurrentToken;
        private Lexer MyLexer;

        public Interpreter(Lexer LexerParm)
        {
            this.MyLexer = LexerParm;
            CurrentToken = MyLexer.GetNextToken();


        }

        public int Expr()
        {
            int result = Term();

            while(CurrentToken.Type!=TokenType.EOF && CurrentToken.Type != TokenType.BRACKET)
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
            if (CurrentToken.Type == TokenType.BRACKET)
            {
                return Bracket();
            } else
            {
                Eat(TokenType.INTEGER);
                return int.Parse(token.Value);
            }
        }

        public int Bracket()
        {
            int result = 0;
            var token = CurrentToken;
            Eat(TokenType.BRACKET);
            result= Expr();
            Eat(TokenType.BRACKET);
            return result;
        }

        private void Eat(TokenType NextTokenType)
        {
            if(CurrentToken.Type == NextTokenType)
            {
                
                CurrentToken = MyLexer.GetNextToken();
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

    public class Lexer
    {

        private char CurrentChar;
        private int Pos;

        private readonly string Text;

        public Lexer(string Text)
        {
            this.Text = Text;
            Pos = 0;
            CurrentChar = Text[Pos];
        }

        public int Integer()
        {
            string res = "";
            while (Char.IsDigit(CurrentChar))
            {
                res += CurrentChar;
                Advance();
            }
            return int.Parse(res);
        }

        public Token GetNextToken()
        {
            while (CurrentChar != '\0')
            {

                if ("\n\t ".Contains(CurrentChar))
                {
                    Advance();
                    continue;
                }

                if (Char.IsDigit(CurrentChar))
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

                if (CurrentChar == '(' || CurrentChar == ')')
                {
                    Advance();
                    return new Token(TokenType.BRACKET, CurrentChar.ToString());
                }

                throw new Exception("Error parsing input");

            }

            return new Token(TokenType.EOF, "\0");

        }

        private void Advance()
        {
            Pos++;
            if (Pos >= Text.Length)
            {
                CurrentChar = '\0';
            }
            else
            {
                CurrentChar = Text[Pos];
            }
        }
    }
}
