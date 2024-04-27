using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Entity;
using Toolkit.Core.Models;

namespace Toolkit.Core.Contracts.Services;

    //加载和保存views实体数据
    public interface IPageInfoDateEntityService
    {
        Task<IEnumerable<PageInfoDateGroupEntity>> GetPageInfoDateGroupEntityAsync();

        Task<IEnumerable<PageInfoDateItemEntity>> GetPageInfoDateItemEntityAsync();

        //Task<bool> Save(IEnumerable<PageInfoDateItemEntity> pageInfoDateItemEntities);

        Task<IEnumerable<PageInfoDateGroup>> PackagePageInfoDateAsync(
            IEnumerable<PageInfoDateGroupEntity> groupEntities,
            IEnumerable<PageInfoDateItemEntity> ItemEntities
        );/*
        {
            return new Task<IEnumerable<PageInfoDateGroup>>(groupEntities, ItemEntities) => {
                ICollection<PageInfoDateGroup> DateGroups = new Collection<PageInfoDateGroup>();
                foreach (var entity in groupEntities)
                {
                    DateGroups.Add(
                        new PageInfoDateGroup(
                            entity.UniqueId,
                            entity.Title,
                            entity.Subtitle,
                            entity.Description,
                            null,
                            entity.Icon,
                            entity.IsExpanded
                        )
                    );
                }
            };
        }*/
    }
