using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Windows;
using Toolkit.Modularity.Modularity;

namespace Toolkit.Modularity
{
    /// <summary>
    /// Defines a store for the module metadata.
    /// </summary>
    public class ConfigurationStore : IConfigurationStore
    {
        /// <summary>
        /// Gets the module configuration data.
        /// </summary>
        /// <returns>A <see cref="ModulesConfigurationSection"/> instance.</returns>
        public ModulesConfigurationSection RetrieveModuleConfigurationSection()
        {
            return System.Configuration.ConfigurationManager.GetSection("modules") as ModulesConfigurationSection;
        }
    }
}