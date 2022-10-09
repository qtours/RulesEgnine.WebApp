// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using RulesEngine.Models;
using RulesEngine.WebApp.engine.factory.product;
using System;
using System.Text.Json;

namespace RulesEngine.WebApp.engine.factory
{
    /// <summary>
    /// EF抽象工厂基类
    /// </summary>
    public abstract class AbstractEngineFactory : DbContext
    {
        #region 抽象接口定义

        /// <summary>
        /// 抽象Rule仓储接口
        /// </summary>
        public virtual IRuleRepository RuleRepository { get; }

        /// <summary>
        /// 抽象Workflow仓储接口
        /// </summary>
        public virtual IWorkflowRepository WorkflowRepository { get; }

        #endregion 抽象接口定义

        public DbSet<Workflow> Workflows { get; set; }

        public DbSet<Rule> Rules { get; set; }

        /// <summary>
        /// 抽象工厂构造方法
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        public AbstractEngineFactory()
        {
        }

        #region 重写DbContext方法

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScopedParam>()
              .HasKey(k => k.Name);

            modelBuilder.Entity<Workflow>(entity => {
                entity.HasKey(k => k.WorkflowName);
                entity.Ignore(b => b.WorkflowsToInject);
            });

            modelBuilder.Entity<Rule>().HasOne<Rule>().WithMany(r => r.Rules).HasForeignKey("RuleNameFK");

            var serializationOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);

            modelBuilder.Entity<Rule>(entity => {
                entity.HasKey(k => k.RuleName);

                var valueComparer = new ValueComparer<Dictionary<string, object>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c);

                entity.Property(b => b.Properties)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, serializationOptions),
                    v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, serializationOptions))
                    .Metadata
                    .SetValueComparer(valueComparer);

                entity.Property(p => p.Actions)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, serializationOptions),
                   v => JsonSerializer.Deserialize<RuleActions>(v, serializationOptions));

                entity.Ignore(b => b.WorkflowsToInject);
                //entity.Ignore(b => b.WorkflowRulesToInject);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Demo.db");

        #endregion 重写DbContext方法
    }
}
