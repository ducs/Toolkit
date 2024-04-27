using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Toolkit.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Toolkit.Models;

#nullable disable

namespace Toolkit.Launcher
{
    internal class Program
    {
        private static Mutex mutex = new Mutex(true, "Toolkit_" + Environment.UserName);

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RunApp(string[] args, bool RunAsAdmin)
        {
            if (RunAsAdmin && !IsAdministrator())
            {
                //�޹���Ա������Ҫ����Ա���������
                //������������
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                //startInfo.FileName = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(".dll", ".exe", StringComparison.CurrentCultureIgnoreCase);

                startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                if (args.Count() > 0)
                {
                    StringBuilder appArguments = new StringBuilder();
                    foreach (string arg in args)
                        appArguments.Append($"{arg}{Constant.AppArgumentsSpliteChar}");
                    startInfo.Arguments = appArguments.ToString();
                }

                //������������,ȷ���Թ���Ա�������
                startInfo.Verb = "runas";
                try
                {
                    Process.Start(startInfo);
                }
                catch (Exception e)
                {
                    return;
                }
            }
            else
            {
                App.Main(); //args
            }
        }

        [STAThread]
        private static void Main(string[] args)
        {
            string AppArguments = String.Empty;

            //App��û���������´�������ʵ����ȡ�Ƿ����������ʵ��
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configRoot = configurationBuilder.Build();
            Boolean MultiRuntime = Convert.ToBoolean(
                configRoot[$"{nameof(AppConfig)}:{nameof(AppConfig.MultiRuntime)}"]
            );
            Boolean RunAsAdmin = Convert.ToBoolean(
                configRoot[$"{nameof(AppConfig)}:{nameof(AppConfig.RunAsAdmin)}"]
            );

            if (MultiRuntime)
            {
                RunApp(args, RunAsAdmin);
            }
            else
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    App.Main(); //argc
                }
                else
                {
                    NativeMethods.PostMessage(
                        (IntPtr)NativeMethods.HWND_BROADCAST,
                        NativeMethods.WM_SHOWME,
                        IntPtr.Zero,
                        IntPtr.Zero
                    );
                }
            }
            return;
        }
    }
}
