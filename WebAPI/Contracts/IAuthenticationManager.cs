using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.DTO;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(string userName, string password);
        Task<string> CreateToken();
    }
}
