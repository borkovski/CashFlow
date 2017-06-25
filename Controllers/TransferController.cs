using Microsoft.AspNetCore.Mvc;
using CashFlow.BusinessObjects;
using CashFlow.BusinessLogic;
using CashFlow.BusinessObjects.Utilities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using CashFlow.BusinessLogic.Utilities;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        // GET: api/values
        [HttpGet]
        public PagedList<TransferDto> Get()
        {
            DataFilter dataFilter = new DataFilterUtilities().GetDataFilter(Request.QueryString.ToString(), "transferDate");
            return new TransferLogic().Get(dataFilter);
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public TransferDto Get(long id)
        {
            return new TransferLogic().GetById(id);
        }

        // POST api/values
        [HttpPost]
        public long? Post([FromBody]TransferDto transfer)
        {
            return new TransferLogic().Save(transfer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            new TransferLogic().Delete(id);
        }
    }
}
