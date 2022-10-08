// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.DependencyInjection;

namespace RulesEngine.WebApp.engine.factory
{
    /// <summary>
    /// 抽象工厂依赖注入扩展方法
    /// </summary>
    public static class EngineExtensions
    {
        /// <summary>
        /// 注册数据访问工厂
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        /// <exception cref="ArgumentNullException">services为空时，抛出异常，终止运行</exception>
        public static IServiceCollection AddDataCore(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(IServiceCollection));

            services.AddSingleton<EngineFactory>();

            return services;
        }
    }
}
