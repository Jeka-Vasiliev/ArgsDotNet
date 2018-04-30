using FluentAssertions;
using System;
using Xunit;
using static ArgsDotNet.ArgsException;

namespace ArgsDotNet.Tests
{
    public class ArgsTests
    {
        [Fact]
        public void TestWithNoSchemaButWithnoSchemaOrArguments()
        {
            var args = new Args("", new string[0]);
            args.Cardinality.Should().Be(0);
        }

        [Fact]
        public void TestWithNoSchemaButWithOneArgument()
        {
            Action act = () => { new Args("", new[] { "-x" }); };
            var exception = act.Should().Throw<ArgsException>().And;
            exception.Code.Should().Be(ErrorCode.UNEXPECTED_ARGUMENT);
            exception.ErrorArgumentId.Should().Be('x');
        }

        [Fact]
        public void TestWithNoSchemaButWithMultipleArguments()
        {

        }

        [Fact]
        public void TestFromCleanCodeBook()
        {
            string[] args = new[] { "-l", "true", "-d", "test", "-p", "42" };
            var argsParser = new Args("l,p#,d*", args);

            var logging = argsParser.GetBoolean('l');
            var port = argsParser.GetInt('p');
            var directory = argsParser.GetString('d');

            Assert.True(logging);
            Assert.Equal(42, port);
            Assert.Equal("test", directory);
        }

        [Fact]
        public void GivenPassedBooleanArgument_WhenGetBoolean_ThenReturnTrue()
        {
            var args = new[] { "-b" };
            var argsParser = new Args("b", args);

            var isBoolSet = argsParser.GetBoolean('b');

            Assert.True(isBoolSet);
        }

        [Fact]
        public void GivenNotPassedBooleanArgument_WhenGetBoolean_ThenReturnFalse()
        {
            var args = new string[0];
            var argsParser = new Args("b", args);

            var isBoolSet = argsParser.GetBoolean('b');

            Assert.False(isBoolSet);
        }

        [Fact]
        public void GivenPassedIntegerArgument_WhenGetBoolean_ThenReturnFalse()
        {
            var args = new[] { "-i", "123" };
            var argsParser = new Args("i#", args);

            var integerArgument = argsParser.GetInt('i');

            Assert.Equal(123, integerArgument);
        }

        [Fact]
        public void GivenPassedDoubleArgument_WhenGetDouble_ThenReturnDouble()
        {
            var args = new[] { "-d", "12,34" };
            var argsParser = new Args("d##", args);

            var doubleArgument = argsParser.GetDouble('d');

            Assert.Equal(12.34d, doubleArgument);
        }

        [Fact]
        public void GivenPassedStringArgument_WhenGetString_ThenReturnStringArray()
        {
            var args = new[] { "-s", "one", "two", "-b" };
            var argsParser = new Args("s[*],b", args);

            var stringArrayArgument = argsParser.GetStringArray('s');

            Assert.Equal(new[] { "one", "two" }, stringArrayArgument);
        }
    }
}
