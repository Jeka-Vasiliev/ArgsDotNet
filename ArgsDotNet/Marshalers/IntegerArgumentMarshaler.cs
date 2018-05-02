using System.Collections.Generic;

namespace ArgsDotNet.Marshalers
{
    internal class IntegerArgumentMarshaler : IArgumentMarshaler
    {
        private int integerValue = default(int);

        public void Set(LinkedListNode<string> currentArgument)
        {
            var argValue = currentArgument.Next?.Value
                ?? throw new ArgsException(ArgsException.ErrorCode.MISSING_INTEGER);
            if (!int.TryParse(argValue, out integerValue))
                throw new ArgsException(ArgsException.ErrorCode.INVALID_INTEGER, argValue);
        }

        internal static int GetValue(IArgumentMarshaler argumentMarshaler)
        {
            return argumentMarshaler is IntegerArgumentMarshaler am
                ? am.integerValue
                : default(int);
        }
    }
}