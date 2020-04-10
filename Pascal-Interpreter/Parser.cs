using System;

namespace Pascal_Interpreter
{
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
            while (CurrentToken.Type == TokenType.ADD || CurrentToken.Type == TokenType.SUBTRACT)
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
            if (CurrentToken.Type == TokenType.LBRACKET)
            {
                return Bracket();
            } else if(CurrentToken.Type == TokenType.ADD)
            {
                
                Eat(TokenType.ADD);
                return new UnaryOperator(Factor(),token);
            }
            else if (CurrentToken.Type == TokenType.SUBTRACT)
            {
                Eat(TokenType.SUBTRACT);
                return new UnaryOperator(Factor(), token);

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
            Eat(TokenType.LBRACKET);
            result = Expr();
            Eat(TokenType.RBRACKET);
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
                throw new Exception(String.Format("Parsing Error: Token not recognised. Expected: {0} but got: {1}", NextTokenType, CurrentToken.Type));
            }
        }

        public ASTNode Parse()
        {
            return Expr();
        }
    }
}
 