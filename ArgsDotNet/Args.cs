using ArgsDotNet.Marchalers;
using System;
using System.Collections.Generic;
using static ArgsDotNet.ArgsException;

namespace ArgsDotNet
{
    public class Args
    {
        private IDictionary<char, IArgumentMarshaler> marshalers = new Dictionary<char, IArgumentMarshaler>();
        private ISet<char> argsFound = new HashSet<char>();

        public int Cardinality { get => argsFound.Count; }

        public Args(string schema, string[] args)
        {
            ParseSchema(schema);
            ParseArgumentStrings(new LinkedList<string>(args));
        }

        private void ParseSchema(string schema)
        {
            foreach (var element in schema.Split(','))
            {
                if (element.Length > 0)
                {
                    ParseSchemaElement(element.Trim());
                }
            }
        }

        private void ParseSchemaElement(string element)
        {
            var elementId = element[0];
            var elementTail = element.Substring(1);
            ValidateSchemaElementId(elementId);

            if (elementTail.Length == 0)
                marshalers.Add(elementId, new BooleanArgumentMarshaler());
            else if (elementTail == "*")
                marshalers.Add(elementId, new StringArgumentMarshaler());
            else if (elementTail == "#")
                marshalers.Add(elementId, new IntegerArgumentMarshaler());
            else if (elementTail == "##")
                marshalers.Add(elementId, new DoubleArgumentMarshaler());
            else if (elementTail == "[*]")
                marshalers.Add(elementId, new StringArrayArgumentMarshaler());
            else
                throw new ArgsException(ErrorCode.INVALID_ARGUMENT_FORMAT, elementId, elementTail);
        }

        private void ValidateSchemaElementId(char elementId)
        {
            if (!char.IsLetter(elementId))
                throw new ArgsException(ErrorCode.INVALID_ARGUMENT_NAME, elementId, null);
        }

        private void ParseArgumentStrings(LinkedList<string> argsList)
        {
            for (var currentArgument = argsList.First; currentArgument != null; currentArgument = currentArgument.Next)
            {
                var argString = currentArgument.Value;
                if (argString.StartsWith("-"))
                {
                    ParseArgumentCharacters(argString.Substring(1), currentArgument);
                }
            }
        }

        private void ParseArgumentCharacters(string argChars, LinkedListNode<string> argument)
        {
            foreach (var argChar in argChars)
            {
                ParseArgumentCharacter(argChar, argument);
            }
        }

        private void ParseArgumentCharacter(char argChar, LinkedListNode<string> argument)
        {
            var isMarshalerFound = marshalers.TryGetValue(argChar, out var marshaler);
            if (!isMarshalerFound)
                throw new ArgsException(ErrorCode.UNEXPECTED_ARGUMENT, argChar, null);

            argsFound.Add(argChar);
            try
            {
                marshaler.Set(argument);
            }
            catch (ArgsException e)
            {
                e.ErrorArgumentId = argChar;
                throw e;
            }
        }

        public bool Has(char arg)
        {
            return argsFound.Contains(arg);
        }

        public bool GetBoolean(char arg)
        {
            return BooleanArgumentMarshaler.GetValue(marshalers[arg]);
        }

        public int GetInt(char arg)
        {
            return IntegerArgumentMarshaler.GetValue(marshalers[arg]);
        }

        public string GetString(char arg)
        {
            return StringArgumentMarshaler.GetValue(marshalers[arg]);
        }

        public double GetDouble(char arg)
        {
            return DoubleArgumentMarshaler.GetValue(marshalers[arg]);
        }

        public string[] GetStringArray(char arg)
        {
            return StringArrayArgumentMarshaler.GetValue(marshalers[arg]);
        }

    }
}
