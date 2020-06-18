using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apical.Interfaces;
using apical.Models.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace apical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly IDataService _dataService;
        private readonly AppSettings _settings;

        public TransactionsController(IDataService dataService, IOptions<AppSettings> settings) : base(dataService)
        {
            _settings = settings.Value;
            _dataService = dataService;
        }

        [HttpGet]
        [Route("~/api/transactions")]
        public IActionResult Get()
        {
            var token = _dataService.Authenticate().Result;

            
            var result = _dataService.GetTransactions(token).Result;
            List<string> fieldsList = typeof(TransactionModel).GetProperties().Select(x => x.Name).ToList();
            return new ObjectResult(_dataService.Response(result, fieldsList));

        }
    }
}