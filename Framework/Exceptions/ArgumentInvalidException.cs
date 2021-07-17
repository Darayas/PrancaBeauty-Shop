using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Exceptions
{
    public class ArgumentInvalidException : Exception
    {
        //public override string Message { get; }
        public ArgumentInvalidException()
        {
            // Message = "";
        }

        public ArgumentInvalidException(string message) : base(message)
        {

        }

        public ArgumentInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArgumentInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
