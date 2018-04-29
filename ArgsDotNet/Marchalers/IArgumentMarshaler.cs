using System;
using System.Collections.Generic;

namespace ArgsDotNet.Marchalers
{
    internal interface IArgumentMarshaler
    {
        void Set(LinkedListNode<string> currentArgument);
    }
}