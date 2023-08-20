using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Toolkit.Core.Contracts.Models
{
    /// <summary>
    /// 这是修改表记录历史接口，使用此接口并配置启用该表历史记录可写历史表,
    /// 只可设置为修改基础数据表,不可作为业务表接口
    /// </summary>
    public interface IDataHistory
    {
        string DataHistoryID { get; set; }
        DateTime ModifyDateTime { get; set; }
        string Machine { get; set; }
        string IPAddress { get; set; }
        string Author { get; set; }

        string GetDataHash();

        //bool IsAvailable();
    }
}