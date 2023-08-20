using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Toolkit.Infrastructure.Common;

namespace Toolkit
{
    public class AppExt : App
    {
        /*
        public static void Main(string AppArguments)
        {
            _argc = AppArguments.Split(Constant.AppArgumentsSpliteChar);
            Main();
        }
        */

        public static void Main(string[] args)
        {
            _args = args;
            Main();
        }
    }
}