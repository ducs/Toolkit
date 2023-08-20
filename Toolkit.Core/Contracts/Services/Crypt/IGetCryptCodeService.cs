using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Core.Contracts.Services.Crypt;

//取加密字符串接口
public interface IGetCryptCodeService
{
    Task<string> GetCryptCodeAsync(string key);

    //string GetCryptCode(string key);
}