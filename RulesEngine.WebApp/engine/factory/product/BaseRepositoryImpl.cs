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
    /// 通用数据仓储接口实现，泛型约束
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    internal class BaseRepositoryImpl<T> : IBaseRepository<T> where T : class
    {
        public DbContext dbContext { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext">数据操作上下文</param>
        public BaseRepositoryImpl(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(dbContext.Set<T>().AsEnumerable());
        }
    }
}
