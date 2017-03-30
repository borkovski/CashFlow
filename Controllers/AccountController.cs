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
    public class AccountController : Controller
    {
        // GET: api/values
        [HttpGet]
        public List<AccountDto> Get()
        {
            return new AccountLogic().GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public AccountDto Get(long id)
        {
            return new AccountLogic().GetById(id);
        }

        // POST api/values
        [HttpPost]
        public long? Post([FromBody]AccountDto account)
        {
            return new AccountLogic().Save(account);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            new AccountLogic().Delete(id);
        }
    }
}