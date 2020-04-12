using System.Collections.Generic;

namespace Pascal_Interpreter
{
    public class ASTNode
    {


    }

    public class NumericalNode:ASTNode
    {

    }

    public class Assignment:ASTNode
    {
        public ASTNode Left;
        public NumericalNode Right;
        public Token Op;

        public Assignment(ASTNode Left, Token Op, NumericalNode Right)
        {
            this.Left = Left;
            this.Right = Right;
            this.Op = Op;
        }
    }

    public class CompoundStatement:ASTNode
    {
        public List<ASTNode> Children;

        public CompoundStatement(List<ASTNode> Children)
        {
            this.Children = Children;
        }
    }

    public class Variable:NumericalNode
    {
        public Token Value;

        public Variable(Token value)
        {
            this.Value = value;
        }
    }

    public class EmptyStatement:ASTNode
    {

    }
    public class BinaryOperator : NumericalNode
    {
        public NumericalNode Left;
        public NumericalNode Right;
        public Token Op;

        public BinaryOperator(NumericalNode Left, Token Op, NumericalNode Right)
        {
            this.Left = Left;
            this.Right = Right;
            this.Op = Op;
        }
    }

    public class UnaryOperator:NumericalNode
    {
        public NumericalNode Node;
        public Token Op;

        public UnaryOperator(NumericalNode node, Token op)
        {
            Node = node;
            Op = op;
        }
    }

    public class Num : NumericalNode
    {
        public Token Value;


        public Num(Token Value)
        {
            this.Value = Value;
        }
    }
}
 