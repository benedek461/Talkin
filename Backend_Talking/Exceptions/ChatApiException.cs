namespace ChatAPI.Exceptions
{
    public class ChatApiException : Exception
    {
        public int ErrorCode { get; set; }

        public ChatApiException() { }

        public ChatApiException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ChatApiException(string message)
        : base(message)
        {
        }

        public ChatApiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
