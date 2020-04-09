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
            Assert.Equal(10, InterpretExpression("5+5"));
            Assert.Equal(6, InterpretExpression("4+2"));
            Assert.Equal(1, InterpretExpression("1+0"));
            Assert.Equal(15, InterpretExpression("7+8"));
        }
        
        [Fact]
        public void TestMultiDigitAddition()
        {

            Assert.Equal(56, InterpretExpression("51+5"));
            Assert.Equal(40, InterpretExpression("10+30"));
            Assert.Equal(6, InterpretExpression("4+2"));
            Assert.Equal(12327, InterpretExpression("12327+0"));
        }

        [Fact]
        public void TestAdditionWithWhitespace()
        {
            Assert.Equal(10, InterpretExpression("5+   5"));
            Assert.Equal(12, InterpretExpression("  0+ 12"));
            Assert.Equal(40, InterpretExpression("10   +   30"));
            Assert.Equal(12327, InterpretExpression("               12327+0"));
        }

        [Fact]
        public void TestSubtraction()
        {
            Assert.Equal(0, InterpretExpression("5- 5"));
            Assert.Equal(-12, InterpretExpression("    0-  12"));
            Assert.Equal(70, InterpretExpression("100 -     30"));
            Assert.Equal(12327, InterpretExpression("         12327-  0"));
        }

        [Fact]
        public void TestMultiplication()
        { 

            Assert.Equal(25, InterpretExpression("5* 5"));
            Assert.Equal(0, InterpretExpression("    0*12"));
            Assert.Equal(3000, InterpretExpression("100 *   30"));
            Assert.Equal(123270, InterpretExpression("   12327*10"));

        }


        [Fact]
        public void TestDivision()
        {
            Assert.Equal(1, InterpretExpression("5/ 5"));
            Assert.Equal(0, InterpretExpression("   0/12"));
            Assert.Equal(3, InterpretExpression("100  / 30"));
            Assert.Equal(1232, InterpretExpression("        12327/10"));
        }

        [Fact]
        public void TestMultipleTerms ()
        {
            Assert.Equal(0, InterpretExpression("5+ 5-10"));
            Assert.Equal(9, InterpretExpression("   0 -12+15 +10 -2 -2"));
            Assert.Equal(-30, InterpretExpression("100 -  30-100"));
            Assert.Equal(12338, InterpretExpression("                   1+ 12327+10"));
            Assert.Equal(17, InterpretExpression("14 + 2 * 3 - 6 / 2"));
        }
        
        [Fact]
        public void TestBracketsAddition()
        {
            Assert.Equal(-21, InterpretExpression("   0 -(12+15) +10 -2 -2"));
            Assert.Equal(0, InterpretExpression("5+ (5-10)"));
            Assert.Equal(170, InterpretExpression("100 - (30 - 100)"));
            Assert.Equal(11, InterpretExpression("14 + 2 * (3 - 6 ) / 2"));

        }
        [Fact]
        public void Interpret_MismatchedBrackets_Error()
        {
            Assert.Throws<Exception>(()=>InterpretExpression("34 +(2*(3+5(("));
        }
        [Fact]
        public void TestBracketsMultipleAddition()
        {
            Assert.Equal(50, InterpretExpression("34 +(2*(3+5))"));
        }

        [Fact]
        public void Interpret_SingleUnary_OutputEqualInput()
        {
            Assert.Equal(-50, InterpretExpression("-50"));
            Assert.Equal(-1, InterpretExpression("-1"));
            Assert.Equal(0, InterpretExpression("-0"));
        }

        [Fact]
        public void Interpret_MultipleTermsWithUnary_ReturnCorrectResult()
        {
            Assert.Equal(5, InterpretExpression("5--5+---5"));
        }
        
        private int InterpretExpression(string Expression)
        {
            var lexer = new Lexer(Expression);
            var parser = new Parser(lexer);
            var intp = new Interpreter(parser);
            return intp.Interpret();
        }

    }
}
