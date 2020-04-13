using System;
using System.Collections.Generic;

namespace Pascal_Interpreter
{
    public class Lexer
    {
        public static readonly Dictionary<string, Token> KEYWORDS = new Dictionary<string, Token>(){
            ["PROGRAM"] = new Token(TokenType.PROGRAM, "PROGRAM"),
            ["BEGIN"] = new Token(TokenType.BEGIN, "BEGIN"),
            ["VAR"] = new Token(TokenType.VAR, "VAR"),
            ["DIV"] = new Token(TokenType.INTEGER_DIV, "DIV"),
            ["INTEGER"] = new Token(TokenType.INTEGER, "INTEGER"),
            ["REAL"] = new Token(TokenType.REAL, "REAL"),
            ["END"] = new Token(TokenType.END, "END")
        };


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
            if(KEYWORDS.ContainsKey(idStr))
            {
                return KEYWORDS[idStr];
            }

            return new Token(TokenType.ID, idStr);

        }

        private void SkipWhitespace()
        {
            while ("\n\t ".Contains(CurrentChar))
            {
                Advance();
                
            }

        }

        private void SkipComment()
        {
            if(CurrentChar=='{')
            {
                while (CurrentChar != '}')
                {
                    if (CurrentChar == '\0')
                        throw new Exception("Error parsing input: comment without closing brace.");
                   
                    Advance();
                }
                Advance();
            }
        }

        private Token Number()
        {
            string res = "";
            bool passedDot = false;
            while (Char.IsDigit(CurrentChar))
            {
                res += CurrentChar;
                Advance();
                if (CurrentChar == '.' && !passedDot)
                {
                    passedDot = true;
                    res += CurrentChar;
                    Advance();
                }
            }
            if(res.Contains('.'))
            {
                return new Token(TokenType.REAL_CONST, res);
            }
            else
            {
                return new Token(TokenType.INTEGER_CONST, res);
            }

        }

        public Token GetNextToken()
        {
            while (CurrentChar != '\0')
            {//fixme
                if ("\n\t ".Contains(CurrentChar) || CurrentChar == '{')
                {
                    SkipWhitespace();
                    SkipComment();
                    continue;
                }
                

                if(Char.IsLetter(CurrentChar))
                {
                    return Id();
                }
                
                
                if (Char.IsDigit(CurrentChar))
                {
                    return Number();
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
                            return new Token(TokenType.COLON, ":");
                        }
                    case '*':
                        Advance();
                        return new Token(TokenType.TIMES, "*");
                    case '/':
                        Advance();
                        return new Token(TokenType.FLOAT_DIV, "/");
                    case '+':
                        Advance();
                        return new Token(TokenType.ADD, "+");
                    case '-':
                        Advance();
                        return new Token(TokenType.SUBTRACT, "-");
                    case '(':
                        Advance();
                        return new Token(TokenType.LBRACKET, "(");
                    case ',':
                        Advance();
                        return new Token(TokenType.COMMA, ",");
                    case ')':
                        Advance();
                        return new Token(TokenType.RBRACKET, ")");
                    case '.':
                        Advance();
                        return new Token(TokenType.DOT, ".");
                    case ';':
                        Advance();
                        return new Token(TokenType.SEMI, ";");
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
 