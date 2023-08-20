using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Modularity;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ModuleAttribute : Attribute
{
    public string ModuleName { get; set; }
    public bool OnDemand { get; set; }
}

