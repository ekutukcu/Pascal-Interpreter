using System;
using Xunit;
using Pascal_Interpreter;

namespace Unit_Tests
{
    public class TestArithmetic
    {
        [Fact]
        public void TestInterpret_SingleIntAssignment_GlobalScopeHasCorrectVals()
        {
            var intp = InterpretExpression(
                "BEGIN " +
                    "x:= 9-4; " +
                    "y:= 40+3; " +
                "END.");
            Assert.Equal(5, intp.GlobalScope["x"]);
            Assert.Equal(43, intp.GlobalScope["y"]);
        }
        

        [Fact]
        public void TestInterpret_MutliOperatorArithmeticThenAssignment_GlobalScopeHasCorrectVals()
        {
            var intp = InterpretExpression(
                "BEGIN " +
                    "x:= 51+5+-2; " +
                    "y:= 10+++30/5;" +
                    "z:=50*2-20 " +
                "END.");
            Assert.Equal(54, intp.GlobalScope["x"]);
            Assert.Equal(16, intp.GlobalScope["y"]);
            Assert.Equal(80, intp.GlobalScope["z"]);
        }

        [Fact]
        public void TestInterpret_ArithmeticWithVarsThenAssignment_GlobalScopeHasCorrectVals()
        {
            var intp = InterpretExpression(
                "BEGIN " +
                    "x:= 51+5-2; " +
                    "y:= x+30/5;" +
                    "z:=50*2-y+x; " +
                "END.");
            Assert.Equal(54, intp.GlobalScope["x"]);
            Assert.Equal(60, intp.GlobalScope["y"]);
            Assert.Equal(94, intp.GlobalScope["z"]);
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
