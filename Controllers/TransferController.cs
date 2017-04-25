using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CashFlow.BusinessObjects;
using CashFlow.BusinessLogic;
using CashFlow.BusinessObjects.Utilities;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        // GET: api/values
        [HttpGet]
        public PagedList<TransferDto> Get(string sortPropertyName = "transferdate", bool isDescending = false, int? skip = null, int? take = null)
        {
            DataFilter dataFilter = new DataFilter();
            dataFilter.SortPropertyName = sortPropertyName;
            //dataFilter.FilterProperties.Add(new KeyValuePair<string, string>("Name", "Zakupy"));
            dataFilter.IsDescending = isDescending;
            dataFilter.Skip = skip;
            dataFilter.Take = take;
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
