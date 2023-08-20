using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Models;

namespace Toolkit.Core.Contracts.Dto
{
    public interface INavigationViewDto : IViewInfo
    {
        /// <summary>
        /// Gets parent if in <see cref="MenuItems"/> collection
        /// </summary>
        string ParentId { get; set; }
        /// <summary>
        /// 数据所指向的类
        /// </summary>
        Assembly Assembly { get; set; }
        bool IsAvailable { get; set; }
    }
}