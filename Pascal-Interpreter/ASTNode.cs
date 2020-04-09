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

    public class Num : ASTNode
    {
        public Token Value;


        public Num(Token Value)
        {
            this.Value = Value;
        }
    }
}
 