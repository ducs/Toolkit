using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Toolkit.Infrastructure.Common;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Toolkit.Launcher
{
    internal class Program
    {
        private static Mutex mutex = new Mutex(true, "Toolkit_" + Environment.UserName);

        [STAThread]
        private static void Main(string[] argc)
        {
            string AppArguments = String.Empty;

            //App还没启动，重新创建配置实例，取是否可以启动多实例
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configRoot = configurationBuilder.Build();

            Boolean MultiRuntime = Convert.ToBoolean(configRoot["AppConfig:MultiRuntime"]);

            if (!IsAdministrator())
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                //startInfo.FileName = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(".dll", ".exe", StringComparison.CurrentCultureIgnoreCase);

                startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                if (argc.Count() > 0)
                {
                    StringBuilder appArguments = new StringBuilder();
                    foreach (string arg in argc)
                        appArguments.Append($"{arg}{Constant.AppArgumentsSpliteChar}");
                    startInfo.Arguments = appArguments.ToString(); ;
                }

                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                try
                {
                    Process.Start(startInfo);
                }
                catch
                {
                    return;
                }
            }
            else
            {
                if (MultiRuntime)
                {
                    AppExt.Main(argc);
                }
                else
                {
                    if (mutex.WaitOne(TimeSpan.Zero, true))
                    {
                        AppExt.Main(argc);
                    }
                    else
                    {
                        NativeMethods.PostMessage(
                            (IntPtr)NativeMethods.HWND_BROADCAST,
                            NativeMethods.WM_SHOWME,
                            IntPtr.Zero,
                            IntPtr.Zero);
                    }
                }
            }
            return;
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}