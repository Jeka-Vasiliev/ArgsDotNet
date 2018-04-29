using System;
using System.Collections.Generic;

namespace ArgsDotNet.Marchalers
{
    internal class IntegerArgumentMarshaler : IArgumentMarshaler
    {
        private int integerValue = 0;

        public void Set(LinkedListNode<string> currentArgument)
        {
            var argValue = currentArgument.Next?.Value
                ?? throw new ArgsException(ErrorCode.MISSING_INTEGER);
            if (!int.TryParse(argValue, out var integerValue))
                throw new ArgsException(ErrorCode.INVALID_INTEGER);
            this.integerValue = integerValue;
        }

        internal static int GetValue(IArgumentMarshaler argumentMarshaler)
        {
            return argumentMarshaler is IntegerArgumentMarshaler am
                ? am.integerValue
                : 0;
        }
    }
}