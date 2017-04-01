using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CashFlow.BusinessLogic;
using CashFlow.BusinessObjects;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    public class TransferSchemaController : Controller
    {
        // GET: api/values
        [HttpGet]
        public List<TransferSchemaDto> Get()
        {
            return new TransferSchemaLogic().GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TransferSchemaDto Get(long id)
        {
            return new TransferSchemaLogic().GetById(id);
        }

        // POST api/values
        [HttpPost]
        public long? Post([FromBody]TransferSchemaDto transfer)
        {
            return new TransferSchemaLogic().Save(transfer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            new TransferSchemaLogic().Delete(id);
        }
    }
}
