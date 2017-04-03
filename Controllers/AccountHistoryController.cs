using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CashFlow.DataAccess.EF;
using CashFlow.BusinessObjects;
using CashFlow.BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    public class AccountHistoryController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public AccountHistoryDto Get(long id)
        {
            return new AccountLogic().GetAccountHistory(id);
        }
    }
}