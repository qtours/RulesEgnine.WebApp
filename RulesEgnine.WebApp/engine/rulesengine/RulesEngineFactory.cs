// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RulesEngine.Models;
using RulesEngine.WebApp.engine.factory;
using RulesEngine.WebApp.engine.factory.product;
using RulesEngine.WebApp.engine.rulesengine.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        public RulesEngineFactory(DbType dbType) : base(dbType)
        {
            RuleRepository = new RuleRepositoryImpl(this);

            WorkflowRepository = new WorkflowRepositoryImpl(this);

            if (Database.EnsureCreated())
            {
                var workflow = JsonConvert.DeserializeObject<List<Workflow>>(@"
                [
  {
    ""WorkflowName"": ""Discount"",
    ""Rules"": [
      {
        ""RuleName"": ""GiveDiscount10"",
        ""SuccessEvent"": ""10"",
        ""ErrorMessage"": ""One or more adjust rules failed."",
        ""ErrorType"": ""Error"",
        ""RuleExpressionType"": ""LambdaExpression"",
        ""Expression"": ""input1.country == \""india\"" AND input1.loyaltyFactor <= 2 AND input1.totalPurchasesToDate >= 5000 AND input2.totalOrders > 2 AND input3.noOfVisitsPerMonth > 2""
      },
      {
        ""RuleName"": ""GiveDiscount20"",
        ""SuccessEvent"": ""20"",
        ""ErrorMessage"": ""One or more adjust rules failed."",
        ""ErrorType"": ""Error"",
        ""RuleExpressionType"": ""LambdaExpression"",
        ""Expression"": ""input1.country == \""india\"" AND input1.loyaltyFactor == 3 AND input1.totalPurchasesToDate >= 10000 AND input2.totalOrders > 2 AND input3.noOfVisitsPerMonth > 2""
      },
      {
        ""RuleName"": ""GiveDiscount25"",
        ""SuccessEvent"": ""25"",
        ""ErrorMessage"": ""One or more adjust rules failed."",
        ""ErrorType"": ""Error"",
        ""RuleExpressionType"": ""LambdaExpression"",
        ""Expression"": ""input1.country != \""india\"" AND input1.loyaltyFactor >= 2 AND input1.totalPurchasesToDate >= 10000 AND input2.totalOrders > 2 AND input3.noOfVisitsPerMonth > 5""
      },
      {
        ""RuleName"": ""GiveDiscount30"",
        ""SuccessEvent"": ""30"",
        ""ErrorMessage"": ""One or more adjust rules failed."",
        ""ErrorType"": ""Error"",
        ""RuleExpressionType"": ""LambdaExpression"",
        ""Expression"": ""input1.loyaltyFactor > 3 AND input1.totalPurchasesToDate >= 50000 AND input1.totalPurchasesToDate <= 100000 AND input2.totalOrders > 5 AND input3.noOfVisitsPerMonth > 15""
      },
      {
        ""RuleName"": ""GiveDiscount30NestedOrExample"",
        ""SuccessEvent"": ""30"",
        ""ErrorMessage"": ""One or more adjust rules failed."",
        ""ErrorType"": ""Error"",
        ""Operator"": ""OrElse"",
        ""Rules"":[
          {
            ""RuleName"": ""IsLoyalAndHasGoodSpend"",
            ""ErrorMessage"": ""One or more adjust rules failed."",
            ""ErrorType"": ""Error"",
            ""RuleExpressionType"": ""LambdaExpression"",
            ""Expression"": ""input1.loyaltyFactor > 3 AND input1.totalPurchasesToDate >= 50000 AND input1.totalPurchasesToDate <= 100000""
          },
          {
            ""RuleName"": ""OrHasHighNumberOfTotalOrders"",
            ""ErrorMessage"": ""One or more adjust rules failed."",
            ""ErrorType"": ""Error"",
            ""RuleExpressionType"": ""LambdaExpression"",
            ""Expression"": ""input2.totalOrders > 15""
          }
        ]
      },
      {
        ""RuleName"": ""GiveDiscount35NestedAndExample"",
        ""SuccessEvent"": ""35"",
        ""ErrorMessage"": ""One or more adjust rules failed."",
        ""ErrorType"": ""Error"",
        ""Operator"": ""AndAlso"",
        ""Rules"": [
          {
            ""RuleName"": ""IsLoyal"",
            ""ErrorMessage"": ""One or more adjust rules failed."",
            ""ErrorType"": ""Error"",
            ""RuleExpressionType"": ""LambdaExpression"",
            ""Expression"": ""input1.loyaltyFactor > 3""
          },
          {
            ""RuleName"": ""AndHasTotalPurchased100000"",
            ""ErrorMessage"": ""One or more adjust rules failed."",
            ""ErrorType"": ""Error"",
            ""RuleExpressionType"": ""LambdaExpression"",
            ""Expression"": ""input1.totalPurchasesToDate >= 100000""
          },
          {
            ""RuleName"": ""AndOtherConditions"",
            ""ErrorMessage"": ""One or more adjust rules failed."",
            ""ErrorType"": ""Error"",
            ""RuleExpressionType"": ""LambdaExpression"",
            ""Expression"": ""input2.totalOrders > 15 AND input3.noOfVisitsPerMonth > 25""
          }
        ]
      }
    ]
  }
]");
                foreach (var item in workflow)
                {
                    WorkflowRepository.Add(item);
                }
                SaveChanges();
            }
        }
    }
}
