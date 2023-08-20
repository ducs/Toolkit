using System;
using Toolkit;

namespace Toolkit.Launcher.Debugger
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            AppExt.Main(args);
        }
    }
}