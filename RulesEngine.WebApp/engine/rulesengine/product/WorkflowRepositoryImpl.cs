// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using RulesEngine.WebApp.engine.factory.product;

namespace RulesEngine.WebApp.engine.rulesengine.product
{
    /// <summary>
    /// 工作流仓储接口实现，继承通用数据仓储接口实现、工作流仓储接口定义
    /// </summary>
    internal class WorkflowRepositoryImpl : BaseRepositoryImpl<Workflow>, IWorkflowRepository
    {
        /// <summary>
        /// 带参构造方法
        /// </summary>
        /// <param name="dbContext">数据操作上下文</param>
        public WorkflowRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}