using System.Collections.Generic;

namespace Pascal_Interpreter
{
    public class ASTNode
    {


    }
    //Program, Block, VarDecl, Type

    public class Program:ASTNode
    {
        public string Name;
        public Block ExecutionBlock;
        public Program(string name, Block block)
        {
            Name = name;
            this.ExecutionBlock = block;
        }
    }

    public class Block:ASTNode
    {
        public List<VariableDeclaration> Declarations;
        public CompoundStatement Statement;

        public Block(List<VariableDeclaration> declarations, CompoundStatement compoundStatement)
        {
            Declarations = declarations;
            Statement = compoundStatement;
        }
    }

    public class VariableDeclaration:ASTNode
    {
        public Type TypeNode;
        public Variable VarNode;
        public VariableDeclaration(Type typeNode, Variable varNode)
        {
            TypeNode = typeNode;
            VarNode = varNode;
        }
    }

    public class Type:ASTNode
    {
        public Token value;

        public Type(Token token)
        {
            value = token;
        }

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
 