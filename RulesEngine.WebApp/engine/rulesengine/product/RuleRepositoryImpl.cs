// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using RulesEngine.WebApp.engine.factory.product;

namespace RulesEngine.WebApp.engine.rulesengine.product
{
    /// <summary>
    /// 规则仓储接口实现，继承通用数据仓储接口实现、规则仓储接口定义
    /// </summary>
    internal class RuleRepositoryImpl : BaseRepositoryImpl<Rule>, IRuleRepository
    {
        /// <summary>
        /// 带参构造方法
        /// </summary>
        /// <param name="dbContext">数据操作上下文</param>
        public RuleRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }

        //仅用于体现接口扩展、多态，该处具体实现无意义（通用数据仓储可定义)
        public IEnumerable<Rule> GetRulesByFilter(string filter)
        {
            throw new NotImplementedException();
        }
    }
}