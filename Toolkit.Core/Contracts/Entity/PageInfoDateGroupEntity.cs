using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Models;

namespace Toolkit.Core.Contracts.Entity
{
    public class PageInfoDateGroupEntity
    {
        public string UniqueId { get; init; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string GroupID { get; set; }
        public IconElement Icon { get; set; }
        public bool IsExpanded { get; set; } = false;
    }
}
