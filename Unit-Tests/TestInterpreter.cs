using System;
using Xunit;
using Pascal_Interpreter;

namespace Unit_Tests
{
    public class TestArithmetic
    {
        [Fact]
        public void TestAddition()
        {
            var intp = new Interpreter("5+5");
            Assert.Equal(10, intp.Expr());

            intp = new Interpreter("4+2");
            Assert.Equal(6, intp.Expr());

            intp = new Interpreter("1+0");
            Assert.Equal(1, intp.Expr());

            intp = new Interpreter("7+8");
            Assert.Equal(15, intp.Expr());

        }
    }
}
