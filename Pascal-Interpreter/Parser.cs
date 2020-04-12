using System;
using System.Collections.Generic;

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

        private NumericalNode Expr()
        {
            NumericalNode result = Term();
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

        private NumericalNode Term()
        {
            NumericalNode result = Factor();
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

        private ASTNode EmptyStatement()
        {
            return new EmptyStatement();
        }

        private ASTNode Variable()
        {
            var token = CurrentToken;
            Eat(TokenType.ID);
            return new Variable(token); ;

        }

        private ASTNode Assignment()
        {
            var left= Variable();
            Eat(TokenType.ASSIGN);
            var right = Expr() as NumericalNode;
            return new Assignment(left,new Token(TokenType.ASSIGN,":="),right);
            

        }

        private ASTNode Statement()
        {
            switch(CurrentToken.Type)
            {
                case TokenType.BEGIN:
                    return CompoundStatement();
                case TokenType.ID:
                    return Assignment();
                case TokenType.END:
                    return EmptyStatement();
            }
            throw new Exception("Could not parse statement");
        }

        private List<ASTNode> StatementList()
        {
            var output = new List<ASTNode>();

            output.Add(Statement());
            while (CurrentToken.Type == TokenType.SEMI)
            {
                Eat(TokenType.SEMI);
                output.Add(Statement());
                    
            }

            return output;

        }

        private CompoundStatement CompoundStatement()
        {
            Eat(TokenType.BEGIN);
            var outputList=StatementList();
            Eat(TokenType.END);
            return new CompoundStatement(outputList);

        }

        private ASTNode Program()
        {

            var output=CompoundStatement();
            Eat(TokenType.DOT);
            return output;

        }

        private NumericalNode Factor()
        {
            var token = CurrentToken;
            if (CurrentToken.Type == TokenType.LBRACKET)
            {
                return Bracket();
            }
            else if (CurrentToken.Type == TokenType.ADD)
            {
                Eat(TokenType.ADD);
                return new UnaryOperator(Factor(), token);
            }
            else if (CurrentToken.Type == TokenType.SUBTRACT)
            {
                Eat(TokenType.SUBTRACT);
                return new UnaryOperator(Factor(), token);
            }
            else if (CurrentToken.Type == TokenType.ID)
            {
                Eat(TokenType.ID);
                return new Variable(token);
            }
            else
            {
                Eat(TokenType.INTEGER);
                return new Num(token);
            }
        }

        private NumericalNode Bracket()
        {
            NumericalNode result;
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
            return Program();
        }
    }
}
 