using System;
using System.Runtime.Serialization;

namespace ArgsDotNet
{
    [Serializable]
    internal class ArgsException : Exception
    {
        private char elementId;
        private string elementTail;

        public ArgsException(ErrorCode code)
        {
        }

        public ArgsException(string message) : base(message)
        {
        }

        public ArgsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ArgsException(ErrorCode code, char elementId, string elementTail) : this(code)
        {
            this.elementId = elementId;
            this.elementTail = elementTail;
        }

        protected ArgsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        internal void SetErrorArgumentId(char argChar)
        {
            throw new NotImplementedException();
        }

        internal enum ErrorCode
        {
            INVALID_ARGUMENT_FORMAT,
            INVALID_ARGUMENT_NAME,
            UNEXPECTED_ARGUMENT,
            MISSING_INTEGER,
            INVALID_INTEGER,
            MISSING_STRING,
            MISSING_DOUBLE,
            INVALID_DOUBLE
        }
    }
}