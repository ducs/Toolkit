using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Services;

namespace Toolkit.Services
{
    public class LoginService : ILoginService
    {
        public async Task<bool> LoginAsync(string username, string password)
        {
            return await Task<bool>.Run(() => true);
        }
    }
}