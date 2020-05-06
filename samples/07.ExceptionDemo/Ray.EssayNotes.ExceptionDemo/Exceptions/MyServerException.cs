using System;

namespace Ray.EssayNotes.ExceptionDemo.Exceptions
{
    public class MyServerException : Exception, IKnownException
    {
        public MyServerException(string message, int errorCode, params object[] errorData) : base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorData = errorData;
        }

        public int ErrorCode { get; private set; }
        public object[] ErrorData { get; private set; }
    }
}
