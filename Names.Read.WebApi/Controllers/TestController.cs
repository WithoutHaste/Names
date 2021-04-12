using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Names.DataAccess.EntityFramework;
using Names.Domain;
using Names.Read.SoapService.Contracts;
using Names.Read.UseCases;

namespace Names.Read.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly INamesGateway _gateway;

        public TestController(INamesGateway gateway)
        {
            _gateway = gateway;
        }

        [HttpGet("service-connection")]
        public ActionResult<bool> TestServiceConnection()
        {
            return true;
        }

        [HttpGet("database-connection")]
        public ActionResult<string> TestDatabaseConnection()
        {
            return _gateway.TestDataStoreConnection();
        }
    }
}
