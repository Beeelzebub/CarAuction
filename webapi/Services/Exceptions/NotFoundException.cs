using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message)
            :base(message)
        {
        }
    }
}
