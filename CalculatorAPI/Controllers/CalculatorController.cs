using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorModels;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("calculate")]
    public class CalculatorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

      

        [HttpPost]
        [Route("Calc")]
        public JsonResult Calc(CalcBody body)
        {
            double res = 0;
            if (body!=null)
            {
                switch (body.Operator)
                {
                    case '/':
                        res = body.left / body.right;
                        break;
                    case '+':
                        res = body.left + body.right;
                        break;
                    case '-':
                        res = body.left - body.right;
                        break;
                    case '*':
                        res = body.left * body.right;
                        break;
                    
                    default:
                        break;
                }
            }

            return new JsonResult(res);

        }
        [HttpPost]
        [Route("CalcString")]
        public JsonResult CalcString(CalcBodyString body)
        {
            string res = "";
            if (body != null)
            {
                switch (body.Operator)
                {
                    case '/':
                        res = body.left + " divide "+ body.right;
                        break;
                    case '+':
                        res = body.left + " plus " + body.right;
                        break;
                    case '-':
                        res = body.left + " minus " + body.right;
                        break;
                    case '*':
                        res = body.left + " multiple " + body.right;
                        break;

                    default:
                        break;
                }
            }

            return new JsonResult(res);

        }
    }
}
