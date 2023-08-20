using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Dto;

namespace Toolkit.Core.Contracts.Services
{
    //加载和保存views实体数据
    public interface INavigationViewModelEntityService
    {
        Task<IEnumerable<INavigationViewDto>> Load();

        Task<bool> Save(IEnumerable<INavigationViewDto> navigationViewEntities);

        Task<bool> Delete(IEnumerable<INavigationViewDto> navigationViewEntities);
    }
}