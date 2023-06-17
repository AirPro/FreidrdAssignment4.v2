using System.Runtime.Serialization;

namespace OrderDetailsMaintenance
{
    [Serializable]
    internal class DataAccessException : Exception
    {
        public DataAccessException()
        {
        }

        public DataAccessException(string? message) : base(message)
        {
        }

        public DataAccessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public bool IsConcurrencyError { get; internal set; }
        public bool IsDeleted { get; internal set; }
        public string ErrorType { get; internal set; }
    }
}