using System.Runtime.Serialization;

namespace ReadingHub.Cores.Validations.Exceptions
{
    [Serializable]
    public class NotFoundException:Exception
    {
        private readonly string message;
        public NotFoundException(string message) : base(message) { 
        this.message = message;
        }
        protected NotFoundException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }

        public override string ToString()
        {

            return $"Can't Add ${message} Exception";
        }
    }
    }

