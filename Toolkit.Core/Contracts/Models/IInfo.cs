using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Core.Contracts.Models
{
    /// <summary>
    /// 基础信息接口
    /// </summary>
    public interface IInfo
    {
        string Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}