using System;
using System.Text.RegularExpressions;

namespace Pascal_Interpreter
{
    public enum TokenType { INTEGER, OPERATOR, EOF}

    public class Interpreter
    {
        private readonly string Text;
        private int Pos;
        private Token CurrentToken;
        

        public Interpreter(string Text)
        {
            //this.Text = Text;
            this.Text = Regex.Replace(Text, @"\s+", "");
            Pos = 0;
        }

        public int Expr()
        {
            int result=0;
            CurrentToken = GetNextToken();

            var Left = CurrentToken;
            Eat(TokenType.INTEGER);

            result = int.Parse(Left.Value);

            while (CurrentToken.Type!=TokenType.EOF)
            {
                var Op = CurrentToken;
                Eat(TokenType.OPERATOR);

                var Right = CurrentToken;
                Eat(TokenType.INTEGER);

                result =EvalOperator(result, int.Parse(Right.Value), Op);
            }

            return result;
            //return Int32.Parse(Left.Value) + Int32.Parse(Right.Value);

        }

        private int EvalOperator(int Left, int Right, Token Op)
        {
            switch(Op.Value)
            {
                case "+":
                    return Left + Right;
                case "-":
                    return Left - Right;
                case "/":
                    return (int)Left / Right;
                case "*":
                    return Left * Right;
            }
            throw new Exception("Error parsing input");
        }

        public Token GetNextToken()
        {
            char current_char;

            if (Pos >= Text.Length)
                return new Token(TokenType.EOF, null);

            current_char = Text[Pos];

            
            if(Char.IsDigit(current_char))
            {
                string tmpStr="";

                while (Char.IsDigit(current_char))
                {
                    tmpStr += current_char.ToString();
                    Pos++;
                    Console.WriteLine(Pos);

                    if (Pos < Text.Length)
                        current_char = Text[Pos];
                    else
                        break;
                }
                //Pos++;
                return new Token(TokenType.INTEGER, tmpStr);
            } else if("+-*/".Contains(current_char))
            {
                Pos++;
                return new Token(TokenType.OPERATOR, current_char.ToString());
            }

            throw new Exception("Error parsing input");
        }

        private void Eat(TokenType NextTokenType)
        {
            
            if(CurrentToken.Type == NextTokenType)
            {
                
                CurrentToken = GetNextToken();
            } else
            {
                throw new Exception("Token not recognised.");
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
