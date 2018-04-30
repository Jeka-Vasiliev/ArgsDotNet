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
    }
}
