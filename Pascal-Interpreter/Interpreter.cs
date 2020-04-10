using System;
using System.Text.RegularExpressions;

namespace Pascal_Interpreter
{

    

    public class Interpreter
    {
        private Parser ParserParm;

        public Interpreter(Parser ParserParm)
        {
            this.ParserParm = ParserParm;

        }
        
        public int Traverse(ASTNode Node)
        {
            if (Node.GetType().Name == "Num")
            {
                return int.Parse((Node as Num).Value.Value);
            } else if (Node.GetType().Name == "BinaryOperator")
            {
                int result = 0;
                var TmpNode = Node as BinaryOperator;
                switch (TmpNode.Op.Type)
                {
                    case TokenType.ADD:
                        result = Traverse(TmpNode.Left) + Traverse(TmpNode.Right);
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
            }else if(Node.GetType().Name=="UnaryOperator")
            {
                var tmpNode = Node as UnaryOperator;
                if(tmpNode.Op.Type==TokenType.ADD)
                {
                    return Traverse(tmpNode.Node);
                } else if(tmpNode.Op.Type==TokenType.SUBTRACT)
                {
                    return -Traverse(tmpNode.Node);
                } else
                {
                    throw new Exception("Runtime Error: Could not interpret unary operator");
                }
            
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
}
 