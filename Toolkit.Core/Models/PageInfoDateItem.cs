using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Models;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

//页运行时间信息
namespace Toolkit.Core.Models
{
    public class PageInfoDateItem : PageInfoDate
    {
        public Type PageType { get; set; }

        public PageInfoDateItem(
            string uniqueId,
            string title,
            string subtitle,
            string description,
            PageInfoDateGroup group,
            IconElement icon,
            string pageType
        )
        {
            UniqueId = uniqueId;
            Title = title;
            Subtitle = subtitle;
            Description = description;
            Group = group;
            Icon = icon;
            Debug.Assert(Type.GetType(pageType, false, true) is not null);
            PageType = Type.GetType(pageType, false, true);
        }
    }
}