using System.Collections.Generic;

namespace ArgsDotNet.Marshalers
{
    internal interface IArgumentMarshaler
    {
        void Set(LinkedListNode<string> currentArgument);
    }
}