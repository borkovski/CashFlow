using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CashFlow.BusinessLogic;
using CashFlow.BusinessObjects;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    public class TransferPendingController : Controller
    {
        // GET: api/values
        [HttpGet]
        public List<TransferDto> Get()
        {
            return new TransferPendingLogic().GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TransferDto Get(long id)
        {
            return new TransferPendingLogic().GetById(id);
        }

        // POST api/values
        [HttpPost]
        public long? Post([FromBody]TransferDto transfer)
        {
            return new TransferPendingLogic().Save(transfer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            new TransferPendingLogic().Delete(id);
        }
    }
}
