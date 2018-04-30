using FluentAssertions;
using Xunit;
using static ArgsDotNet.ArgsException;

namespace ArgsDotNet.Tests
{
    public class ArgsExceptionTests
    {
        [Fact]
        public void TestUnexpectedMessage()
        {
            var e = new ArgsException(ErrorCode.UNEXPECTED_ARGUMENT, 'x', null);
            e.ToString().Should().Be("Argument -x unexpected.");
        }

        [Fact]
        public void TestMissingStringMessage()
        {
            var e = new ArgsException(ErrorCode.MISSING_STRING, 'x', null);
            e.ToString().Should().Be("Could not find string parameter for -x.");
        }

        [Fact]
        public void TestInvalidIntegerMessage()
        {
            var e = new ArgsException(ErrorCode.INVALID_INTEGER, 'x', "Forty two");
            e.ToString().Should().Be("Argument -x expects an integer but was 'Forty two'.");
        }

        [Fact]
        public void TestMissingIntegerMessage()
        {
            var e = new ArgsException(ErrorCode.MISSING_INTEGER, 'x', null);
            e.ToString().Should().Be("Could not find integer parameter for -x.");
        }

        [Fact]
        public void TestInvalidDoubleMessage()
        {
            var e = new ArgsException(ErrorCode.INVALID_DOUBLE, 'x', "Forty two");
            e.ToString().Should().Be("Argument -x expects a double but was 'Forty two'.");
        }

        [Fact]
        public void TestMissingDoubleMessage()
        {
            var e = new ArgsException(ErrorCode.MISSING_DOUBLE, 'x', null);
            e.ToString().Should().Be("Could not find double parameter for -x.");
        }
    }
}
