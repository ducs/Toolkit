using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Core.Contracts.Services
{
    public interface IConfigService
    {
        T GetConfig<T>(string key);//where T : class, new();

        string GetValue(string key);

        string GetConfigValue(string key);
    }
}