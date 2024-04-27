using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Models;

namespace Toolkit.Core.Contracts.Models
{
    /// <summary>
    /// 基础信息接口
    /// </summary>
    public interface IInfo
    {
        public string UniqueId { get; init; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public PageInfoDateGroup Group { get; set; }
    }
}