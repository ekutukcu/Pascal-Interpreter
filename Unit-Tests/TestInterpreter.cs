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

        [Fact]
        public void TestAdditionWithWhitespace()
        {
            var intp = new Interpreter("5+  5");
            Assert.Equal(10, intp.Expr());

            intp = new Interpreter("   0 +12 ");
            Assert.Equal(12, intp.Expr());

            intp = new Interpreter("10 +  30");
            Assert.Equal(40, intp.Expr());

            intp = new Interpreter("                    12327+0");
            Assert.Equal(12327, intp.Expr());

        }

        [Fact]
        public void TestSubtraction()
        {
            var intp = new Interpreter("5- 5");
            Assert.Equal(0, intp.Expr());

            intp = new Interpreter("   0 -12 ");
            Assert.Equal(-12, intp.Expr());

            intp = new Interpreter("100 -  30");
            Assert.Equal(70, intp.Expr());

            intp = new Interpreter("                    12327-0");
            Assert.Equal(12327, intp.Expr());

        }

        [Fact]
        public void TestMultiplication()
        {
            var intp = new Interpreter("5* 5");
            Assert.Equal(25, intp.Expr());

            intp = new Interpreter("   0 *12 ");
            Assert.Equal(0, intp.Expr());

            intp = new Interpreter("100 *  30");
            Assert.Equal(3000, intp.Expr());

            intp = new Interpreter("                    12327*10");
            Assert.Equal(123270, intp.Expr());

        }


        [Fact]
        public void TestDivision()
        {
            var intp = new Interpreter("5/ 5");
            Assert.Equal(1, intp.Expr());

            intp = new Interpreter("   0 /12 ");
            Assert.Equal(0, intp.Expr());

            intp = new Interpreter("100 /  30");
            Assert.Equal(3, intp.Expr());

            intp = new Interpreter("                    12327/10");
            Assert.Equal(1232, intp.Expr());

        }

        [Fact]
        public void TestMultipleTerms ()
        {
            var intp = new Interpreter("5+ 5-10");
            Assert.Equal(0, intp.Expr());

            intp = new Interpreter("   0 -12+15 +10 -2 -2");
            Assert.Equal(9, intp.Expr());

            intp = new Interpreter("100 -  30-100");
            Assert.Equal(-30, intp.Expr());

            intp = new Interpreter("                   1+ 12327+10");
            Assert.Equal(12338, intp.Expr());

        }


    }
}
