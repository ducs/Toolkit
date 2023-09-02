using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Services;
using Toolkit.Core.Contracts.Services.Crypt;

#nullable disable

namespace Toolkit.Services
{
    public class GetJsonCryptCodeService : IGetCryptCodeService
    {
        private class JsonCryptCode
        {
            public string Key { get; set; }
            public string Code { get; set; }
        }

        public async Task<string> GetCryptCodeAsync(string key)
        //public string GetCryptCode(string key)
        {
            Debug.Assert(App.GetRequiredService<IConfigService>()
                .GetConfig<List<JsonCryptCode>>("CryptCode")
                .Where<JsonCryptCode>(i => i.Key == key).FirstOrDefault()
                 != null);

            return await Task<string>.Run(
                () => App.GetRequiredService<IConfigService>()
                .GetConfig<List<JsonCryptCode>>("CryptCode")
                .Where<JsonCryptCode>(i => i.Key == key).FirstOrDefault().Code
                );
        }
    }
}