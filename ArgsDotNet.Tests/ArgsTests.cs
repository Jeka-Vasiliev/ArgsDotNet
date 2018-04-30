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
            Action act = () => { new Args("", new[] { "-x", "-y" }); };
            var exception = act.Should().Throw<ArgsException>().And;
            exception.Code.Should().Be(ErrorCode.UNEXPECTED_ARGUMENT);
            exception.ErrorArgumentId.Should().Be('x');
        }

        [Fact]
        public void TestNonLetterSchema()
        {
            Action act = () => { new Args("*", new string[0]); };
            act.Should().Throw<ArgsException>().And.ErrorArgumentId.Should().Be('*');
        }

        [Fact]
        public void TestInvalidArgumentFormat()
        {
            Action act = () => { new Args("f~", new string[0]); };
            var exception = act.Should().Throw<ArgsException>().And;
            exception.Code.Should().Be(ErrorCode.INVALID_ARGUMENT_FORMAT);
            exception.ErrorArgumentId.Should().Be('f');
        }

        [Fact]
        public void TestSimpleBooleanPresent()
        {
            var args = new Args("x", new[] { "-x" });
            args.Cardinality.Should().Be(1);
            args.GetBoolean('x').Should().BeTrue();
        }

        [Fact]
        public void TestSimpleStringPresent()
        {
            var args = new Args("x*", new[] { "-x", "param" });
            args.Cardinality.Should().Be(1);
            args.Has('x').Should().BeTrue();
            args.GetString('x').Should().Be("param");
        }

        [Fact]
        public void TestMissingStringArgument()
        {
            Action act = () => { new Args("x*", new[] { "-x" }); };
            var exception = act.Should().Throw<ArgsException>().And;
            exception.Code.Should().Be(ErrorCode.MISSING_STRING);
            exception.ErrorArgumentId.Should().Be('x');
        }

        [Fact]
        public void TestSpacesInFormat()
        {
            var args = new Args("x, y", new[] { "-xy" });
            args.Cardinality.Should().Be(2);
            args.GetBoolean('x').Should().BeTrue();
            args.GetBoolean('y').Should().BeTrue();
        }

        [Fact]
        public void TestSimpleIntPresent()
        {
            var args = new Args("x#", new[] { "-x", "42" });
            args.Cardinality.Should().Be(1);
            args.Has('x').Should().BeTrue();
            args.GetInt('x').Should().Be(42);
        }

        [Fact]
        public void TestInvalidInteger()
        {
            Action act = () => { new Args("x#", new[] { "-x", "Fourty two" }); };
            var exception = act.Should().Throw<ArgsException>().And;
            exception.Code.Should().Be(ErrorCode.INVALID_INTEGER);
            exception.ErrorArgumentId.Should().Be('x');
            exception.ErrorParameter.Should().Be("Fourty two");
        }

        [Fact]
        public void TestMissingInteger()
        {
            Action act = () => { new Args("x#", new[] { "-x" }); };
            var exception = act.Should().Throw<ArgsException>().And;
            exception.Code.Should().Be(ErrorCode.MISSING_INTEGER);
            exception.ErrorArgumentId.Should().Be('x');
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
