using System;
using Xunit;
using Pascal_Interpreter;

namespace Unit_Tests
{
    public class TestArithmetic
    {
        [Fact]
        public void TestSingleDigitAddition()
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

        [Fact]
        public void TestMultiDigitAddition()
        {
            var intp = new Interpreter("51+5");
            Assert.Equal(56, intp.Expr());

            intp = new Interpreter("4+2");
            Assert.Equal(6, intp.Expr());

            intp = new Interpreter("10+30");
            Assert.Equal(40, intp.Expr());

            intp = new Interpreter("12327+0");
            Assert.Equal(12327, intp.Expr());

        }
    }
}
