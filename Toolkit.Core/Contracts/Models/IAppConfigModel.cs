using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Appearance;

namespace Toolkit.Core.Contracts.Models
{
    /// <summary>
    /// 本地配置
    /// </summary>
    public interface IAppConfigModel
    {
        string AppName { get; set; }
        string AppVersion { get; set; }
        string AppTitle { get; set; }

        /// <summary>
        /// 是否可以运行多个程序副本
        /// </summary>
        bool MultiRuntime { get; set; }

        Wpf.Ui.Appearance.ApplicationTheme Theme { get; set; }

        string ConfigurationsFolder { get; set; }

        string AppPropertiesFileName { get; set; }
    }
}