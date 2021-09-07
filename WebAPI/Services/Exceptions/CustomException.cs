using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public abstract class CustomException : Exception, ICustomException
    {
        protected CustomException(string message)
            : base(message)
        {

        }

        public string ToJson() => Data.Count == 0 ? JsonSerializer.Serialize(new { ErrorMessage = Message })
            : JsonSerializer.Serialize(Data);
    }
}