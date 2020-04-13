using System;
using Xunit;
using Pascal_Interpreter;

namespace Unit_Tests
{
    public class TestArithmetic
    {

        [Fact]
        public void TestInterpret_ArithmeticWithVarsThenAssignment_GlobalScopeHasCorrectVals()
        {
            string program = @"
            PROGRAM ProgName;
            VAR
               a     : INTEGER;
               b     : INTEGER;

            BEGIN
                  a := 2+6 DIV (3-1);
                  b := a*10;
            END.";

            var intp = InterpretExpression(program);
            Assert.Equal(5, intp.GlobalScope["a"]);
            Assert.Equal(typeof(int), intp.GlobalScope["a"].GetType());
            Assert.Equal(50, intp.GlobalScope["b"]);
            Assert.Equal(typeof(int), intp.GlobalScope["b"].GetType());
        }

        [Fact]
        public void TestInterpret_ArithmeticWithRealVar_GlobalScopeHasCorrectVals()
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
               y := 20 / 10 + 3.14;
               { writeln('a = ', a); }
               { writeln('b = ', b); }
               { writeln('c = ', c); }
               { writeln('number = ', number); }
               { writeln('x = ', x); }
               { writeln('y = ', y); }
            END.  {Part10}";

            var intp = InterpretExpression(program);
            Assert.Equal(11,intp.GlobalScope["x"]);
            Assert.Equal(25,intp.GlobalScope["b"]);
            Assert.True((5.14-intp.GlobalScope["y"])<0.00001);
        }

        
        private Interpreter InterpretExpression(string Expression)
        {
            var lexer = new Lexer(Expression);
            var parser = new Parser(lexer);
            var intp = new Interpreter(parser);
            intp.Interpret();
            return intp;
        }

    }
}
