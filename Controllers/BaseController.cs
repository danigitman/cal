using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apical.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// data service interface
        /// </summary>
        protected IDataService DataService;

        public BaseController(IDataService dataService)
        {
            DataService = dataService;
        }
    }
}