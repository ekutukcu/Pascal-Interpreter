namespace Pascal_Interpreter
{
    public class ASTNode
    {

    }

    public class BinaryOperator : ASTNode
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

    public class UnaryOperator:ASTNode
    {
        public ASTNode Node;
        public Token Op;

        public UnaryOperator(ASTNode node, Token op)
        {
            Node = node;
            Op = op;
        }
    }

    public class Num : ASTNode
    {
        public Token Value;


        public Num(Token Value)
        {
            this.Value = Value;
        }
    }
}
 