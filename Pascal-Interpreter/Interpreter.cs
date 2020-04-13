using System;
using System.Collections.Generic;

namespace Pascal_Interpreter
{

    public class Interpreter
    {
        private Parser ParserParm;

        public Interpreter(Parser ParserParm)
        {
            this.ParserParm = ParserParm;
            this.GlobalScope = new Dictionary<string, dynamic>();
        }

        public Dictionary<string,dynamic> GlobalScope { get; set; }

        public void Visit(ASTNode node)
        {
            var nodeName = node.GetType().Name;
            var visitorMethod = this.GetType().GetMethod("Visit" + nodeName);
            visitorMethod.Invoke(this,new object[] { node });
        }

        public dynamic Visit(NumericalNode node)
        {
            var nodeName = node.GetType().Name;
            var visitorMethod = this.GetType().GetMethod("Visit" + nodeName);
            return visitorMethod.Invoke(this, new object[] { node });
        }

        public void VisitProgram(ASTNode node)
        {
            var program = node as Program;
            Visit(program.ExecutionBlock);
        }

        public void VisitBlock(ASTNode node)
        {
            var block=node as Block;
            foreach (var decl in block.Declarations)
                Visit(decl);
            Visit(block.Statement);
        }

        public void VisitVariableDeclaration(ASTNode node)
        {
            
        }

        public void VisitType(ASTNode node)
        {
            
        }

        public dynamic VisitBinaryOperator(NumericalNode node)
        {
            dynamic result;
            var TmpNode = node as BinaryOperator;
            switch (TmpNode.Op.Type)
            {
                case TokenType.ADD:
                    result = Visit(TmpNode.Left) + Visit(TmpNode.Right);
                    break;
                case TokenType.SUBTRACT:
                    result = Visit(TmpNode.Left) - Visit(TmpNode.Right);
                    break;
                case TokenType.TIMES:
                    result = Visit(TmpNode.Left) * Visit(TmpNode.Right);
                    break;
                case TokenType.INTEGER_DIV:
                    result = Visit(TmpNode.Left) / Visit(TmpNode.Right);
                    break;
                case TokenType.FLOAT_DIV:
                    result = Visit(TmpNode.Left) / Visit(TmpNode.Right);
                    break;
                default:
                    throw new Exception("Unrecognised op");
            }

            return result;
        }

        public void VisitEmptyStatement(ASTNode node)

        {
            
        }

        public int VisitUnaryOperator(NumericalNode node)
        {
            var tmpNode = node as UnaryOperator;
            if (tmpNode.Op.Type == TokenType.ADD)
            {
                return Visit(tmpNode.Node);
            }
            else if (tmpNode.Op.Type == TokenType.SUBTRACT)
            {
                return -Visit(tmpNode.Node);
            }
            else
            {
                throw new Exception("Runtime Error: Could not interpret unary operator");
            }
        }

        public void VisitAssignment(ASTNode node)
        {
            var assignNode=node as Assignment;
            var varName = assignNode.Left as Variable;
            GlobalScope[varName.Value.Value] = Visit(assignNode.Right);
        }

        public dynamic VisitNum(NumericalNode node)
        {
            var num = node as Num;
            if (num.Value.Value.Contains('.'))
                return float.Parse(num.Value.Value);
            return int.Parse((node as Num).Value.Value);
        }

        public void VisitCompoundStatement(ASTNode node)
        {
            var compoundNode = node as CompoundStatement;
            foreach(var statement in compoundNode.Children)
            {
                Visit(statement);
            }
        }

        public int VisitVariable(NumericalNode node)
        {
            var varNode = node as Variable;
            return GlobalScope[varNode.Value.Value];
        }

        

        public void Interpret()
        {
            var tree = ParserParm.Parse();
            Visit(tree);
            //return Traverse(tree);
        }
        

    }
}
 