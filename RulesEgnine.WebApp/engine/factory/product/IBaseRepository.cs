// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.WebApp.engine.factory.product
{
    /// <summary>
    /// 通用数据仓储接口定义
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// 数据操作上下文
        /// </summary>
        DbContext dbContext { get; }

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="entity">泛型实体</param>
        void Add(T entity);

        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="entity">泛型实体</param>
        void Update(T entity);

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="entity"><泛型实体/param>
        void Delete(T entity);

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <returns>泛型数据集</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
