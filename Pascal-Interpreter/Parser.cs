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
                else if (CurrentToken.Type == TokenType.INTEGER_DIV)
                {
                    Eat(TokenType.INTEGER_DIV);
                    
                } else if(CurrentToken.Type==TokenType.FLOAT_DIV)
                {
                    Eat(TokenType.FLOAT_DIV);
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
            Eat(TokenType.PROGRAM);
            var name = CurrentToken;
            Eat(TokenType.ID);
            Eat(TokenType.SEMI);
            var block =new Block(VariableDeclarations(), CompoundStatement());
            var output=new Program(name.Value,block);
            Eat(TokenType.DOT);
            return output;

        }

        private List<VariableDeclaration> VariableDeclarations()
        {
            
            var varDecls = new List<VariableDeclaration>();
            if(CurrentToken.Type==TokenType.VAR)
            {
                Eat(TokenType.VAR);
                while (CurrentToken.Type == TokenType.ID)
                {
                    varDecls.AddRange(VarDecl());
                    Eat(TokenType.SEMI);
                }
            }


            return varDecls;

        }

        private List<VariableDeclaration> VarDecl()
        {
            var varDecls = new List<VariableDeclaration>();
            var varNodes = new List<Variable>();


            Variable varNode = new Variable(CurrentToken);
            Eat(TokenType.ID);
            varNodes.Add(varNode);

            while (CurrentToken.Type == TokenType.COMMA)
            {
                Eat(TokenType.COMMA);
                varNode = new Variable(CurrentToken);
                varNodes.Add(varNode);
                Eat(TokenType.ID);
            }

            Eat(TokenType.COLON);
            var typeSpec = TypeSpec();
            foreach (var node in varNodes)
            {
                varDecls.Add(new VariableDeclaration(typeSpec, node));
            }

            return varDecls;
        }

        private Type TypeSpec()
        {
            var token = CurrentToken;
            if (CurrentToken.Type == TokenType.INTEGER)
                Eat(TokenType.INTEGER);
            else
                Eat(TokenType.REAL);

            return new Type(token);

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
            else if (CurrentToken.Type == TokenType.INTEGER_CONST)
            {
                Eat(TokenType.INTEGER_CONST);
                return new Num(token);
            }
            else if(CurrentToken.Type == TokenType.REAL_CONST)
            {
                Eat(TokenType.REAL_CONST);
                return new Num(token);
            }
            else
            {
                throw new Exception("");
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
 