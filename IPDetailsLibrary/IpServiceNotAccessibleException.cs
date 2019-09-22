using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IPDetailsLibrary
{
    public class IpServiceNotAccessibleException : Exception
    {
        public IpServiceNotAccessibleException()
        {
        }

        public IpServiceNotAccessibleException(string message) : base(message)
        {
        }

        public IpServiceNotAccessibleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IpServiceNotAccessibleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
