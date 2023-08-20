using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Services;
using System.Text.Json;
using Toolkit.Core.Contracts.Dto;

#nullable disable

namespace Toolkit.Services
{
    /// <summary>
    /// 从appsetting.json时读取配置
    /// </summary>
    public class NavigationViewJson : INavigationViewModelEntityService
    {
        private readonly string JsonConfigFile =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appsettings.json");

        public Task<IEnumerable<INavigationViewDto>> Load()
        {
            /*
            var path = Path.Combine();
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<INavigationViewEntity>(json);
            }
            */
            return default;
        }

        public Task<bool> Save(IEnumerable<INavigationViewDto> navigationViewEntities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(IEnumerable<INavigationViewDto> navigationViewEntities)
        {
            throw new NotImplementedException();
        }

        public T ReadJsonFile<T>(string JsonConfigFile)
        {
            if (File.Exists(JsonConfigFile))
            {
                var json = File.ReadAllText(JsonConfigFile);
                return JsonSerializer.Deserialize<T>(json);
            }

            return default;
        }

        private Task<bool> SaveJsonFile<T>(string JsonConfigFile, T content)
        {
            string folderPath = Path.GetDirectoryName(JsonConfigFile);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileContent = JsonSerializer.Serialize(content);
            File.WriteAllText(JsonConfigFile, fileContent, Encoding.UTF8);

            return default(Task<bool>);
        }

        private Task<bool> DeleteJsonFile(string JsonConfigFile)
        {
            if (JsonConfigFile != null && File.Exists(JsonConfigFile))
            {
                File.Delete(JsonConfigFile);
            }
            return default(Task<bool>);
        }
    }
}