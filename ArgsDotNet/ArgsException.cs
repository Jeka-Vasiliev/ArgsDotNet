using System;
using System.Runtime.Serialization;

namespace ArgsDotNet
{
    [Serializable]
    public class ArgsException : Exception
    {
        public ErrorCode Code { get; set; }
        public char ErrorArgumentId { get; set; }
        public string ErrorParameter { get; set; }

        public ArgsException(ErrorCode code)
        {
            Code = code;
        }

        public ArgsException(ErrorCode code, string errorParameter)
        {
            Code = code;
            ErrorParameter = errorParameter;
        }

        public ArgsException(string message) : base(message)
        {
        }

        public ArgsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ArgsException(ErrorCode code, char argumentId, string errorParameter) : this(code)
        {
            ErrorArgumentId = argumentId;
            ErrorParameter = errorParameter;
        }

        protected ArgsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            switch (Code)
            {
                case ErrorCode.UNEXPECTED_ARGUMENT:
                    return $"Argument -{ErrorArgumentId} unexpected.";
                case ErrorCode.MISSING_STRING:
                    return $"Could not find string parameter for -{ErrorArgumentId}.";
                case ErrorCode.INVALID_INTEGER:
                    return $"Argument -{ErrorArgumentId} expects an integer but was '{ErrorParameter}'.";
                case ErrorCode.MISSING_INTEGER:
                    return $"Could not find integer parameter for {ErrorArgumentId}";
                case ErrorCode.INVALID_DOUBLE:
                    return $"Argument {ErrorArgumentId} expects a double but was {ErrorParameter}";
                case ErrorCode.MISSING_DOUBLE:
                    return $"Could not find double parameter for {ErrorArgumentId}";
                case ErrorCode.INVALID_ARGUMENT_NAME:
                    return $"{ErrorArgumentId} is not valid argument name";
                case ErrorCode.INVALID_ARGUMENT_FORMAT:
                    return $"{ErrorParameter} is not valid argument format";
                default:
                    return "TILT: Should not get here";
            }
        }

        public enum ErrorCode
        {
            UNEXPECTED_ARGUMENT,
            MISSING_STRING,
            INVALID_INTEGER,
            MISSING_INTEGER,
            INVALID_DOUBLE,
            MISSING_DOUBLE,
            INVALID_ARGUMENT_NAME,
            INVALID_ARGUMENT_FORMAT,
        }
    }
}