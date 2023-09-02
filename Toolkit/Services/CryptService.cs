using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Services.Crypt;

namespace Toolkit.Services
{
    public class CryptService : ICryptService
    {
        public string Decrypt(string src, string password)
        {
            string result = src + "|" + password;

            Debug.Assert(result != null);

            return result;
        }

        public string Encrypt(string src, string password)
        {
            return src;
        }
    }
}