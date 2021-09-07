﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            :base(message)
        {

        }
    }
}
