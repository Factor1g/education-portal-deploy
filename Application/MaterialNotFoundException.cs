using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal class MaterialNotFoundException : Exception
    {
        public MaterialNotFoundException()
        {
        }

        public MaterialNotFoundException(string? message) : base(message)
        {
        }

        public MaterialNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MaterialNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
