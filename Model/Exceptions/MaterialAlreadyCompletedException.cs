using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal class MaterialAlreadyCompletedException : Exception
    {
        public MaterialAlreadyCompletedException()
        {
        }

        public MaterialAlreadyCompletedException(string? message) : base(message)
        {
        }

        public MaterialAlreadyCompletedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MaterialAlreadyCompletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
