using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Models;

namespace Toolkit.Core.Models
{
    public abstract class PageInfoDate : IInfo
    {
        public string UniqueId { get; init; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public PageInfoDateGroup Group { get; set; } = null;
        public IconElement Icon { get; set; }
    }
}