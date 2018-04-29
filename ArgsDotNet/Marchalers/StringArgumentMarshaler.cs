using System.Collections.Generic;

namespace ArgsDotNet.Marchalers
{
    internal class StringArgumentMarshaler : IArgumentMarshaler
    {
        private string stringValue = "";

        public void Set(LinkedListNode<string> currentArgument)
        {
            stringValue = currentArgument.Next?.Value
                ?? throw new ArgsException(ErrorCode.MISSING_STRING);
        }

        internal static string GetValue(IArgumentMarshaler argumentMarshaler)
        {
            return argumentMarshaler is StringArgumentMarshaler am
                ? am.stringValue
                : "";
        }
    }
}