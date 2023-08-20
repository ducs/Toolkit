using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Services;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Configuration.Json;
using System.Diagnostics.Eventing.Reader;
using Toolkit.Models;

namespace Toolkit.Services
{
    //配置分为本地配置，和扩展配置，本地配置保存本地环境，比如数据库连接，主题
    //扩展配置包含公共属性配置，比如系统模块，公共设置，一般存储在数据库里
    //本设置用于扩展公共设置,程序自带用于设置本地配置
    public class JsonConfigService : IConfigService
    {
        private IConfiguration _configuration;

        public JsonConfigService(IConfiguration configuration)
        {
            _configuration = configuration;

            //_configuration<AppConfig>(_configuration.GetSection(nameof(AppConfig)));
        }

        public string GetValue(string key)
        {
            return _configuration[key];
        }

        public string GetConfigValue(string key)
        {
            return _configuration[$"{nameof(AppConfig)}:{key}"];
        }

        public T GetConfig<T>(string key) //where T : class, new()
        {
            return _configuration.GetSection(key)!.Get<T>();
        }
    }
}