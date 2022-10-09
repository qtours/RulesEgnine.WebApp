// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.WebApp.engine.factory;
using RulesEngine.WebApp.engine.factory.product;
using RulesEngine.WebApp.engine.rulesengine.product;

namespace RulesEngine.WebApp.engine.rulesengine
{
    /// <summary>
    /// Rules抽象工厂实现
    /// </summary>
    internal class RulesEngineFactory : AbstractEngineFactory
    {
        // 仅重写与Rules相关的仓储接口定义，用于全局访问

        public override IRuleRepository RuleRepository { get; }

        public override IWorkflowRepository WorkflowRepository { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        public RulesEngineFactory()
        {
            RuleRepository = new RuleRepositoryImpl(this);
            WorkflowRepository = new WorkflowRepositoryImpl(this);

            Database.EnsureCreated();
        }
    }
}