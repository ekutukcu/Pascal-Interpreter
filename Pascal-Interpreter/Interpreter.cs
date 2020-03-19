using System;
using System.Text.RegularExpressions;

namespace Pascal_Interpreter
{
    public enum TokenType { INTEGER, TIMES, DIVIDE, ADD, SUBTRACT, OPERATOR, BRACKET, EOF}

    public class ASTNode
    {

    }

    public class BinaryOperator: ASTNode
    {
        public ASTNode Left;
        public ASTNode Right;
        public Token Op;

        public BinaryOperator(ASTNode Left, Token Op, ASTNode Right)
        {
            this.Left = Left;
            this.Right = Right;
            this.Op = Op;
        }
    }

    public class Num: ASTNode
    {
        public Token Value;


        public Num(Token Value)
        {
            this.Value = Value;
        }
    }

    public class Parser
    {
        private Lexer MyLexer;
        private Token CurrentToken;

        public Parser(Lexer LexerParm)
        {
            MyLexer = LexerParm;
            CurrentToken = MyLexer.GetNextToken();
        }

        public ASTNode Expr()
        {
            ASTNode result = Term();
            Token token;
            while (CurrentToken.Type != TokenType.EOF && CurrentToken.Type != TokenType.BRACKET)
            {
                token = CurrentToken;
                if (CurrentToken.Type == TokenType.ADD)
                {
                    Eat(TokenType.ADD);
                    
                }
                else if (CurrentToken.Type == TokenType.SUBTRACT)
                {
                    Eat(TokenType.SUBTRACT);
                   
                }
                result = new BinaryOperator(result, token, Term());
            }
            return result;

        }

        public ASTNode Term()
        {
            ASTNode result = Factor();
            Token token;
            while (CurrentToken.Type != TokenType.EOF)
            {
                token = CurrentToken;
                if (CurrentToken.Type == TokenType.TIMES)
                {
                    Eat(TokenType.TIMES);
                    
                }
                else if (CurrentToken.Type == TokenType.DIVIDE)
                {
                    Eat(TokenType.DIVIDE);
                    
                }
                else
                {
                    break;
                }
                result = new BinaryOperator(result, token, Factor());
            }
            return result;
        }

        public ASTNode Factor()
        {
            var token = CurrentToken;
            if (CurrentToken.Type == TokenType.BRACKET)
            {
                return Bracket();
            }
            else
            {
                Eat(TokenType.INTEGER);
                return new Num(token);
            }
        }

        public ASTNode Bracket()
        {
            ASTNode result;
            var token = CurrentToken;
            Eat(TokenType.BRACKET);
            result = Expr();
            Eat(TokenType.BRACKET);
            return result;
        }

        private void Eat(TokenType NextTokenType)
        {
            if (CurrentToken.Type == NextTokenType)
            {

                CurrentToken = MyLexer.GetNextToken();
            }
            else
            {
                throw new Exception(String.Format("Token not recognised. Expected: {0} but got: {1}", NextTokenType, CurrentToken.Type));
            }
        }

        public ASTNode Parse()
        {
            return Expr();
        }
    }


    public class Interpreter
    {
        private Token CurrentToken;
        private Parser ParserParm;

        public Interpreter(Parser ParserParm)
        {
            this.ParserParm = ParserParm;

        }
        
        public int Traverse(ASTNode Node)
        {
            if(Node.GetType().Name=="Num")
            {
                return int.Parse((Node as Num).Value.Value);
            } else if (Node.GetType().Name == "BinaryOperator")
            {
                int result = 0;
                var TmpNode = Node as BinaryOperator;
                switch (TmpNode.Op.Type)
                {
                    case TokenType.ADD:
                        result= Traverse(TmpNode.Left) + Traverse(TmpNode.Right);
                        break;
                    case TokenType.SUBTRACT:
                        result = Traverse(TmpNode.Left) - Traverse(TmpNode.Right);
                        break;
                    case TokenType.TIMES:
                        result = Traverse(TmpNode.Left) * Traverse(TmpNode.Right);
                        break;
                    case TokenType.DIVIDE:
                        result = Traverse(TmpNode.Left) / Traverse(TmpNode.Right);
                        break;
                }

                return result;
            } else
            {
                throw new Exception("Runtime error");
            }
        }

        public int Interpret()
        {
            var tree = ParserParm.Parse();
            return Traverse(tree);
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
