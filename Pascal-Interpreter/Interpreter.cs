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
            this.GlobalScope = new Dictionary<string, int>();
        }

        public Dictionary<string,int> GlobalScope { get; set; }

        public void Visit(ASTNode node)
        {
            var nodeName = node.GetType().Name;
            var visitorMethod = this.GetType().GetMethod("Visit" + nodeName);
            visitorMethod.Invoke(this,new object[] { node });
        }

        public int Visit(NumericalNode node)
        {
            var nodeName = node.GetType().Name;
            var visitorMethod = this.GetType().GetMethod("Visit" + nodeName);
            return (int)visitorMethod.Invoke(this, new object[] { node });
        }

        public int VisitBinaryOperator(NumericalNode node)
        {
            int result = 0;
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
                case TokenType.DIVIDE:
                    result = Visit(TmpNode.Left) / Visit(TmpNode.Right);
                    break;
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

        public int VisitNum(NumericalNode node)
        {
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
 