using System;
using System.Collections.Generic;
using System.Globalization;
using static ArgsDotNet.ArgsException;

namespace ArgsDotNet.Marchalers
{
    internal class DoubleArgumentMarshaler : IArgumentMarshaler
    {
        private double doubleValue = default(double); 

        public void Set(LinkedListNode<string> currentArgument)
        {
            var argValue = currentArgument.Next?.Value
                ?? throw new ArgsException(ErrorCode.MISSING_DOUBLE);
            if (!double.TryParse(argValue, NumberStyles.Any, CultureInfo.InvariantCulture, out doubleValue))
                throw new ArgsException(ErrorCode.INVALID_DOUBLE);
        }

        internal static double GetValue(IArgumentMarshaler argumentMarshaler)
        {
            return argumentMarshaler is DoubleArgumentMarshaler am
                ? am.doubleValue
                : default(double);
        }
    }
}