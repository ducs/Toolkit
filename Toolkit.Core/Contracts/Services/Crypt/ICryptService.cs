using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Core.Contracts.Services.Crypt;

//加密解密接口
public interface ICryptService
{
    string Decrypt(string src, string CryptPassword);

    string Encrypt(string src, string CryptPassword);
}