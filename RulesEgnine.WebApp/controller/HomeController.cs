// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using RulesEngine.WebApp.engine.factory;
using RulesEngine.WebApp.engine.factory.product;

namespace RulesEgnine.WebApp.controller
{
    [Route("")]
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public ActionResult<List<Rule>> Index()
        {
            return Rule.GetAllAsync().Result.ToList();
        }

        IRuleRepository Rule { get; }

        public HomeController(EngineFactory engineFactory)
        {
            Rule = engineFactory.DbFactory.RuleRepository;
        }
    }
}
