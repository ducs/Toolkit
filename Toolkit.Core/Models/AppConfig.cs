using Toolkit.Core.Contracts.Models;
using Wpf.Ui.Appearance;

namespace Toolkit.Models
{
    public class AppConfig : IAppConfigModel
    {
        public string AppName { get; set; }

        public string AppVersion { get; set; }

        public string AppTitle { get; set; }

        public bool MultiRuntime { get; set; }
        public ThemeType Theme { get; set; }

        public string ConfigurationsFolder { get; set; }

        public string AppPropertiesFileName { get; set; }
    }
}