using System;
using Xunit;

namespace ArgsDotNet.Tests
{
    public class LearningTest
    {
        [Fact]
        public void MainTest()
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
    }
}
