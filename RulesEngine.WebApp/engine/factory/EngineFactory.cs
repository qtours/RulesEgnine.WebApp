// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.WebApp.engine.rulesengine;

namespace RulesEngine.WebApp.engine.factory
{
    /// <summary>
    /// 抽象工厂入口
    /// </summary>
    public class EngineFactory
    {
        /// <summary>
        /// 工厂全局访问接口
        /// </summary>
        public AbstractEngineFactory DbFactory { get; }

        /// <summary>
        /// 无参构造方法
        /// </summary>
        public EngineFactory()
        {
            DbFactory = new RulesEngineFactory(DbType.Sqlite);
        }

        /// <summary>
        /// 带参数构造方法，用于依赖注入时，指定数据库类型
        /// </summary>
        public EngineFactory(DbType dbType)
        {
            DbFactory = new RulesEngineFactory(dbType);
        }
    }
}
