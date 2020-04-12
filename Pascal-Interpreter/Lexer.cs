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

        private int Integer()
        {
            string res = "";
            while (Char.IsDigit(CurrentChar))
            {
                res += CurrentChar;
                Advance();
            }
            return int.Parse(res);
        }

        private Token Id()
        {
            string idStr = "";
            while (Char.IsLetterOrDigit(CurrentChar))
            {
                idStr += CurrentChar;
                Advance();

            }
            switch(idStr)
            {
                case "BEGIN":
                    return new Token(TokenType.BEGIN, idStr);
                case "END":
                    return new Token(TokenType.END, idStr);
                default:
                    return new Token(TokenType.ID, idStr);
            }

        }

        public Token GetNextToken()
        {
            while (CurrentChar != '\0')
            {
                if(Char.IsLetter(CurrentChar))
                {
                    return Id();
                }

                if ("\n\t ".Contains(CurrentChar))
                {
                    Advance();
                    continue;
                }

                if (Char.IsDigit(CurrentChar))
                {
                    return new Token(TokenType.INTEGER, Integer().ToString());
                }

                switch(CurrentChar)
                {
                    case ':':
                        Advance();
                        if (CurrentChar == '=')
                        {
                            Advance();
                            return new Token(TokenType.ASSIGN, ":=");
                        } else
                        {
                            throw new Exception("Error parsing input");

                        }
                    case '*':
                        Advance();
                        return new Token(TokenType.TIMES, "*");
                    case '/':
                        Advance();
                        return new Token(TokenType.DIVIDE, "/");
                    case '+':
                        Advance();
                        return new Token(TokenType.ADD, "+");
                    case '-':
                        Advance();
                        return new Token(TokenType.SUBTRACT, "-");
                    case '(':
                        Advance();
                        return new Token(TokenType.LBRACKET, CurrentChar.ToString());
                    case ')':
                        Advance();
                        return new Token(TokenType.RBRACKET, CurrentChar.ToString());
                    case '.':
                        Advance();
                        return new Token(TokenType.DOT, CurrentChar.ToString());
                    case ';':
                        Advance();
                        return new Token(TokenType.SEMI, CurrentChar.ToString());
                    default:
                        throw new Exception("Error parsing input");

                }

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
 