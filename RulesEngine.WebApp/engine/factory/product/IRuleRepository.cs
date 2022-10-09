// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.Models;

namespace RulesEngine.WebApp.engine.factory.product
{
    /// <summary>
    /// 规则仓储接口定义
    /// </summary>
    public interface IRuleRepository : IBaseRepository<Rule>
    {
        //基于通用接口之外的自定义扩展接口
        IEnumerable<Rule> GetRulesByFilter(string filter);
    }
}