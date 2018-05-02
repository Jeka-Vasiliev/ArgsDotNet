using System.Collections.Generic;

namespace ArgsDotNet.Marshalers
{
    internal class StringArgumentMarshaler : IArgumentMarshaler
    {
        private string stringValue = string.Empty;

        public void Set(LinkedListNode<string> currentArgument)
        {
            stringValue = currentArgument.Next?.Value
                ?? throw new ArgsException(ArgsException.ErrorCode.MISSING_STRING);
        }

        internal static string GetValue(IArgumentMarshaler argumentMarshaler)
        {
            return argumentMarshaler is StringArgumentMarshaler am
                ? am.stringValue
                : string.Empty;
        }
    }
}