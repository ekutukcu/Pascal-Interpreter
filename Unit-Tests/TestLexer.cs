using System;
using Xunit;
using Pascal_Interpreter;

namespace Unit_Tests
{
    public class TestLexer
    {

        [Fact]
        public void GetNextToken_SingleArithmeticSymbols_ReturnsMatchingTokens()
        {
            var lexer = new Lexer("+");
            Assert.Equal(TokenType.ADD, lexer.GetNextToken().Type);

            lexer = new Lexer("-");
            Assert.Equal(TokenType.SUBTRACT, lexer.GetNextToken().Type);

            lexer = new Lexer("DIV");
            Assert.Equal(TokenType.INTEGER_DIV, lexer.GetNextToken().Type);

            lexer = new Lexer("*");
            Assert.Equal(TokenType.TIMES, lexer.GetNextToken().Type);

        }


        [Fact]
        public void GetNextToken_Assign_ReturnsAssignToken()
        {
            var lexer = new Lexer(":=");
            Assert.Equal(TokenType.ASSIGN, lexer.GetNextToken().Type);

        }

        [Fact]
        public void GetNextToken_Begin_ReturnsBeginToken()
        {
            var lexer = new Lexer("BEGIN");
            Assert.Equal(TokenType.BEGIN, lexer.GetNextToken().Type);

        }

        [Fact]
        public void GetNextToken_End_ReturnsEndToken()
        {
            var lexer = new Lexer("END");
            Assert.Equal(TokenType.END, lexer.GetNextToken().Type);

        }

        [Fact]
        public void GetNextToken_LBracket_ReturnsLBracketToken()
        {
            var lexer = new Lexer("(");
            Assert.Equal(TokenType.LBRACKET, lexer.GetNextToken().Type);

        }

        [Fact]
        public void GetNextToken_RBracket_ReturnsRBracketToken()
        {
            var lexer = new Lexer(")");
            Assert.Equal(TokenType.RBRACKET, lexer.GetNextToken().Type);

        }

        [Fact]
        public void GetNextToken_Identifier_ReturnsIdentifierToken()
        {
            var lexer = new Lexer("Joh234j");
            var nextToken = lexer.GetNextToken();
            Assert.Equal(TokenType.ID, nextToken.Type);
            Assert.Equal("Joh234j", nextToken.Value);

        }

        [Fact]
        public void GetNextToken_Integer_ReturnsIntegerToken()
        {
            var lexer = new Lexer("23423");
            var nextToken = lexer.GetNextToken();
            Assert.Equal(TokenType.INTEGER_CONST, nextToken.Type);
            Assert.Equal("23423", nextToken.Value);

        }

        [Fact]
        public void GetNextToken_Real_ReturnsRealToken()
        {
            var lexer = new Lexer("23423.234");
            var nextToken = lexer.GetNextToken();
            Assert.Equal(TokenType.REAL_CONST, nextToken.Type);
            Assert.Equal("23423.234", nextToken.Value);

        }

        [Fact]
        public void GetNextToken_MultipleArithmeticTokens_ReturnsCorrectTokens()
        {
            var lexer = new Lexer("(566+344)");
            Assert.Equal(TokenType.LBRACKET, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.INTEGER_CONST, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.ADD, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.INTEGER_CONST, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.RBRACKET, lexer.GetNextToken().Type);

        }

        [Fact]
        public void GetNextToken_MultipleTokens_ReturnsCorrectTokens()
        {
            var lexer = new Lexer("BEGIN   sdfsdf  (566+344)END ENDsdjklfahsdjkfh32423");
            Assert.Equal(TokenType.BEGIN, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.ID, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.LBRACKET, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.INTEGER_CONST, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.ADD, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.INTEGER_CONST, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.RBRACKET, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.END, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.ID, lexer.GetNextToken().Type);

        }

        [Fact]
        public void GetNextToken_InvalidToken_ThrowsError()
        {
            var lexer = new Lexer("^+");
            Assert.Throws<Exception>(() => lexer.GetNextToken());

            lexer = new Lexer("dgdf+3453^");
            lexer.GetNextToken();
            lexer.GetNextToken();
            lexer.GetNextToken();
            Assert.Throws<Exception>(() => lexer.GetNextToken());

        }

        [Fact]
        public void GetNextToken_ExampleProgram_ReturnsCorrectTokens()
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

            var lexer = new Lexer(program);
            Assert.Equal(TokenType.PROGRAM, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.ID, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.SEMI, lexer.GetNextToken().Type);

            Assert.Equal(TokenType.VAR, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.ID, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.COLON, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.INTEGER, lexer.GetNextToken().Type);
            Assert.Equal(TokenType.SEMI, lexer.GetNextToken().Type);
            //Assert.Equal(TokenType.BEGIN, lexer.GetNextToken().Type);


        }
    }
}
