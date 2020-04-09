using System;

namespace Pascal_Interpreter
{
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



                if (CurrentChar == '(')
                {
                    Advance();
                    return new Token(TokenType.LBRACKET, CurrentChar.ToString());
                }
                if (CurrentChar == ')')
                {
                    Advance();
                    return new Token(TokenType.RBRACKET, CurrentChar.ToString());
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
 