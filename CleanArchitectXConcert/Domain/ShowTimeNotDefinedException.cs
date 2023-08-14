using System.Runtime.Serialization;

namespace Domain.ConcertTickets
{
    /// <summary>
    /// 票種場次未定義的自訂錯誤 Exception
    /// </summary>
    [Serializable]
    internal class ShowTimeNotDefinedException : Exception
    {
        public ShowTimeNotDefinedException()
        {
        }

        public ShowTimeNotDefinedException(string? message) : base(message)
        {
        }

        public ShowTimeNotDefinedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ShowTimeNotDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}