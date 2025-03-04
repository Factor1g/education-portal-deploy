﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException()
        {
        }

        public AuthorizationFailedException(string? message) : base(message)
        {
        }

        public AuthorizationFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AuthorizationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
