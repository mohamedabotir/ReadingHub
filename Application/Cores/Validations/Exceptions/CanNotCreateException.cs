using System.Runtime.Serialization;

namespace ReadingHub.Cores.Validations.Exceptions
{
    [Serializable]
    public class CanNotCreateException : Exception
    {
        private readonly string message;
        public CanNotCreateException(string message):base(message)
        {
            this.message = message;
        }
        protected CanNotCreateException(SerializationInfo info, StreamingContext context) : base(info, context) { 
        
        }
        public override string ToString()
        {

            return $"Can't Add {message} Exception";
        }
    }
} 
