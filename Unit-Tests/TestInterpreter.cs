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
            var lexer = new Lexer("5+5");
            var intp = new Interpreter(lexer);
            Assert.Equal(10, intp.Expr());

            lexer = new Lexer("4+2");
            intp = new Interpreter(lexer);
            Assert.Equal(6, intp.Expr());

            lexer = new Lexer("1+0");
            intp = new Interpreter(lexer);
            Assert.Equal(1, intp.Expr());

            lexer = new Lexer("7+8");
            intp = new Interpreter(lexer);
            Assert.Equal(15, intp.Expr());

        }

        [Fact]
        public void TestMultiDigitAddition()
        {
            var lexer = new Lexer("51+5");

            var intp = new Interpreter(lexer);
            Assert.Equal(56, intp.Expr());

            lexer = new Lexer("4+2");

            intp = new Interpreter(lexer);
            Assert.Equal(6, intp.Expr());

            lexer = new Lexer("10+30");
            intp = new Interpreter(lexer);
            Assert.Equal(40, intp.Expr());

            lexer = new Lexer("12327+0");
            intp = new Interpreter(lexer);
            Assert.Equal(12327, intp.Expr());

        }

        [Fact]
        public void TestAdditionWithWhitespace()
        {
            var lexer = new Lexer("5+  5");
            var intp = new Interpreter(lexer);
            Assert.Equal(10, intp.Expr());

            lexer = new Lexer("   0 +12 ");
            intp = new Interpreter(lexer);
            Assert.Equal(12, intp.Expr());

            lexer = new Lexer("10 +  30");
            intp = new Interpreter(lexer);
            Assert.Equal(40, intp.Expr());

            lexer = new Lexer("                    12327+0");
            intp = new Interpreter(lexer);
            Assert.Equal(12327, intp.Expr());

        }

        [Fact]
        public void TestSubtraction()
        {
            var lexer = new Lexer("5- 5");
            var intp = new Interpreter(lexer);
            Assert.Equal(0, intp.Expr());

            lexer = new Lexer("   0 -12 ");
            intp = new Interpreter(lexer);
            Assert.Equal(-12, intp.Expr());

            lexer = new Lexer("100 -  30");
            intp = new Interpreter(lexer);
            Assert.Equal(70, intp.Expr());

            lexer = new Lexer("                    12327-0");
            intp = new Interpreter(lexer);
            Assert.Equal(12327, intp.Expr());

        }

        [Fact]
        public void TestMultiplication()
        {
            var lexer = new Lexer("5* 5");
            var intp = new Interpreter(lexer);
            Assert.Equal(25, intp.Expr());

            lexer = new Lexer("   0 *12 ");
            intp = new Interpreter(lexer);
            Assert.Equal(0, intp.Expr());

            lexer = new Lexer("100 *  30");
            intp = new Interpreter(lexer);
            Assert.Equal(3000, intp.Expr());

            lexer = new Lexer("                    12327*10");
            intp = new Interpreter(lexer);
            Assert.Equal(123270, intp.Expr());

        }


        [Fact]
        public void TestDivision()
        {
            var lexer = new Lexer("5/ 5");
            var intp = new Interpreter(lexer);
            Assert.Equal(1, intp.Expr());

            lexer = new Lexer("   0 /12 ");
            intp = new Interpreter(lexer);
            Assert.Equal(0, intp.Expr());

            lexer = new Lexer("100 /  30");
            intp = new Interpreter(lexer);
            Assert.Equal(3, intp.Expr());

            lexer = new Lexer("                    12327/10");
            intp = new Interpreter(lexer);
            Assert.Equal(1232, intp.Expr());

        }

        [Fact]
        public void TestMultipleTerms ()
        {
            var lexer = new Lexer("5+ 5-10");
            var intp = new Interpreter(lexer);
            Assert.Equal(0, intp.Expr());

            lexer = new Lexer("   0 -12+15 +10 -2 -2");
            intp = new Interpreter(lexer);
            Assert.Equal(9, intp.Expr());

            lexer = new Lexer("100 -  30-100");
            intp = new Interpreter(lexer);
            Assert.Equal(-30, intp.Expr());

            lexer = new Lexer("                   1+ 12327+10");
            intp = new Interpreter(lexer);
            Assert.Equal(12338, intp.Expr());

            lexer = new Lexer("14 + 2 * 3 - 6 / 2");
            intp = new Interpreter(lexer);
            Assert.Equal(17, intp.Expr());
        }

        [Fact]
        public void TestBracketsAddition()
        {
            var lexer = new Lexer("5+ (5-10)");
            var intp = new Interpreter(lexer);
            Assert.Equal(0, intp.Expr());

            lexer = new Lexer("   0 -(12+15) +10 -2 -2");
            intp = new Interpreter(lexer);
            Assert.Equal(-21, intp.Expr());

            lexer = new Lexer("100 -  (30-100)");
            intp = new Interpreter(lexer);
            Assert.Equal(170, intp.Expr());

            lexer = new Lexer("                   1+ 12327+10");
            intp = new Interpreter(lexer);
            Assert.Equal(12338, intp.Expr());

            lexer = new Lexer("14 + 2 * (3 - 6 ) / 2");
            intp = new Interpreter(lexer);
            Assert.Equal(11, intp.Expr());
        }

        [Fact]
        public void TestBracketsMultipleAddition()
        {
            var lexer = new Lexer("34+(2*(3+5))");
            var intp = new Interpreter(lexer);
            Assert.Equal(50, intp.Expr());
        }


    }
}
