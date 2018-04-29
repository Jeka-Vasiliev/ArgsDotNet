﻿using System;
using System.Collections.Generic;

namespace ArgsDotNet.Marchalers
{
    internal class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        private bool boolValue = false;

        public void Set(LinkedListNode<string> currentArgument)
        {
            boolValue = true;
        }

        internal static bool GetValue(IArgumentMarshaler argumentMarshaler)
        {
            return argumentMarshaler is BooleanArgumentMarshaler am 
                ? am.boolValue 
                : false;
        }
    }
}