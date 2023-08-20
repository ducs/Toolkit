using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace Toolkit.Core.Contracts.Models
{
    public interface IViewInfo : IInfo
    {
        IconElement? Icon { get; set; }
        bool IsActive { get; }
        bool IsExpanded { get; internal set; }
        object Content { get; set; }
        string TargetPageTag { get; set; }

        Type? TargetPageType { get; set; }

        internal bool IsMenuElement { get; set; }
        //public Assembly Assembly { get; set; }
        //bool IsAvailable { get; set; }
    }
}