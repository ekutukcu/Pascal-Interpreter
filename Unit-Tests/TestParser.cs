using System;
using Pascal_Interpreter;
using Xunit;
namespace Unit_Tests
{
    public class TestParser
    {

        [Fact]
        public void Parse_AddTwoInts_ReturnsBinaryOp()
        {
            var parser = new Parser(new Lexer("BEGIN x:=345+23 END."));
            var output = parser.Parse() as CompoundStatement;
            var assignment = output.Children[0] as Assignment;
            var expr = assignment.Right as BinaryOperator;
            Assert.Equal("23", (expr.Right as Num).Value.Value);
            Assert.Equal("345", (expr.Left as Num).Value.Value);
        }

        [Fact]
        public void Parse_AddThreeInts_ReturnsCorrectTree()
        {
            var parser = new Parser(new Lexer("BEGIN y:=345+23+2342 END."));
            var output = parser.Parse() as CompoundStatement;
            var assignment = output.Children[0] as Assignment;
            var expr = assignment.Right as BinaryOperator;
            Assert.Equal(typeof(BinaryOperator), expr.Left.GetType());
            Assert.Equal("2342", (expr.Right as Num).Value.Value);
        }
        [Fact]
        public void Parse_AddVariables_ReturnsCorrectTree()
        {
            var parser = new Parser(new Lexer("BEGIN x:=5; y:=345+23+x END."));
            var output = parser.Parse() as CompoundStatement;
            var assignment = output.Children[1] as Assignment;
            var expr = assignment.Right as BinaryOperator;
            Assert.Equal(typeof(BinaryOperator), expr.Left.GetType());
            Assert.Equal("x", (expr.Right as Variable).Value.Value);
        }

        [Fact]
        public void Parse_ProgramEmptyStatement_ReturnsCompoundWith1Child()
        {
            var parser = new Parser(new Lexer("BEGIN  END."));
            var output = parser.Parse() as CompoundStatement;
            Assert.Single(output.Children);

        }

        [Fact]
        public void Parse_ProgramTwoStatements_ReturnsCompoundWith2Children()
        {
            var parser = new Parser(new Lexer("BEGIN x:=5; y:=43 END."));
            var output = parser.Parse() as CompoundStatement;
            Assert.Equal(2,output.Children.Count);

        }

        [Fact]
        public void Parse_ProgramTwoAssignments_Returns3Children()
        {
           string input="BEGIN " +
                    "x:= 9-4; " +
                    "y:= 40+3; " +
                "END.";
            var parser = new Parser(new Lexer(input));
            var output = parser.Parse() as CompoundStatement;
            Assert.Equal(3, output.Children.Count);
        }
    }
}
