
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RulesEngine.Models;
using RulesEngine.WebApp.engine.factory;
using RulesEngine.WebApp.engine.factory.product;
using System.Linq.Expressions;

namespace RulesEgnine.WebApp.controller
{
    [Route("")]
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        IRuleRepository Rule { get; }
        IWorkflowRepository Workflow { get; }

        public HomeController(EngineFactory engineFactory)
        {
            Rule = engineFactory.DbFactory.RuleRepository;
            Workflow = engineFactory.DbFactory.WorkflowRepository;
        }

        public ActionResult<List<Rule>> GetAll()
        {
            return Rule.GetAllAsync().Result.ToList();
        }

        [HttpPost]
        public IActionResult CreateWorkflow()
        {
            var lstRule = new List<Rule> 
            {
                new Rule
                {
                    RuleName = "IsLoyal",
                    ErrorMessage = "One or more adjust rules failed.",
                    RuleExpressionType = RuleExpressionType.LambdaExpression,
                    Expression = "input1.loyaltyFactor > 3"
                },
            };
            var workflow = new Workflow
            {
                WorkflowName = "Discount",
                Rules = lstRule
            };
            Workflow.Add(workflow);
            return Ok();
        }
    }
}
