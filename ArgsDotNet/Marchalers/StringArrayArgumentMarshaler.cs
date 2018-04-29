using System;
using System.Collections.Generic;

namespace ArgsDotNet.Marchalers
{
    internal class StringArrayArgumentMarshaler : IArgumentMarshaler
    {
        private List<string> stringArrayArgument = new List<string>();

        public void Set(LinkedListNode<string> currentArgument)
        {
            currentArgument = currentArgument.Next;
            while(currentArgument != null && !currentArgument.Value.StartsWith("-"))
            {
                stringArrayArgument.Add(currentArgument.Value);
                currentArgument = currentArgument.Next;
            }
        }
            
        internal static string[] GetValue(IArgumentMarshaler argumentMarshaler)
        {
            return argumentMarshaler is StringArrayArgumentMarshaler am
                ? am.stringArrayArgument.ToArray()
                : new string[0];
        }
    }
}