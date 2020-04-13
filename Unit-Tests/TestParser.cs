using System;
using Pascal_Interpreter;
using Xunit;
namespace Unit_Tests
{
    public class TestParser
    {

        [Fact]
        public void Parse_SingleStatement_BlockHasSingleChild()
        {
            string input= @"
PROGRAM Part10;
VAR
   number     : INTEGER;

BEGIN
   number := 2
END.";
            var parser = new Parser(new Lexer(input));
            var output = parser.Parse() as Program;
            Assert.Single(output.ExecutionBlock.Declarations);
            Assert.Single(output.ExecutionBlock.Statement.Children);
        }

        [Fact]
        public void Parse_ThreeStatements_BlockHasFourChildren()
        {
            string input = @"
PROGRAM Part10;
VAR
   number     : INTEGER;

BEGIN
   number := 2;
   number := 3;
   number := 4;
END.";
            var parser = new Parser(new Lexer(input));
            var output = parser.Parse() as Program;
            Assert.Single(output.ExecutionBlock.Declarations);
            Assert.Equal(4,output.ExecutionBlock.Statement.Children.Count);
        }
        [Fact]
        public void Parse_ThreeVarDeclarations_DeclarationCountIsThree()
        {
            string input = @"
PROGRAM Part10;
VAR
   number     : INTEGER;
   number2    : INTEGER;
   number3    : REAL;

BEGIN
   number := 2;
   number := 3;
   number := 4;
END.";
            var parser = new Parser(new Lexer(input));
            var output = parser.Parse() as Program;
            Assert.Equal(3, output.ExecutionBlock.Declarations.Count);
            Assert.Equal(4, output.ExecutionBlock.Statement.Children.Count);
        }

        [Fact]
        public void Parse_ProgramEmptyStatement_ReturnsCompoundWith1Child()
        {
            string input = @"
PROGRAM Part10;
VAR

BEGIN
END.";
            var parser = new Parser(new Lexer(input));
            var output = parser.Parse() as Program;
            Assert.Empty(output.ExecutionBlock.Declarations);
            Assert.Single(output.ExecutionBlock.Statement.Children);

        }

        [Fact]
        public void Parse_ProgramWithComments_ReturnsCorrectTree()
        {
            string input = @"
PROGRAM Part10;
VAR{sdaf}
{dsfkdjsfl;kadsfadsf}
BEGIN {adsfklhdosif}
END.";
            var parser = new Parser(new Lexer(input));
            var output = parser.Parse() as Program;
            Assert.Empty(output.ExecutionBlock.Declarations);
            Assert.Single(output.ExecutionBlock.Statement.Children);

        }


        [Fact]
        public void Parse_FullProgram_ReturnsCorrectAST()
        {
            string program = @"
PROGRAM Part10;
VAR
   number     : INTEGER;
   a, b, c, x : INTEGER;
   y          : REAL;

BEGIN {Part10}
   BEGIN
      number := 2;
      a := number;
      b := 10 * a + 10 * number DIV 4;
      c := a - - b
   END;
   x := 11;
   y := 20 / 7 + 3.14;
   { writeln('a = ', a); }
   { writeln('b = ', b); }
   { writeln('c = ', c); }
   { writeln('number = ', number); }
   { writeln('x = ', x); }
   { writeln('y = ', y); }
END.  {Part10}";

            var parser = new Parser(new Lexer(program));
            var output = parser.Parse() as Program;
            Assert.Equal("Part10",output.Name);

        }
    }
}
