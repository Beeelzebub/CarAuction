using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class BadRequestException : Exception, ICustomException
    {
        private object _errorObject;

        public BadRequestException(string message, object errorObject)
            :base(message)
        {

        }

        public object ErrorObject => 
    }
}
