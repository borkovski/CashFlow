using Microsoft.AspNetCore.Mvc;
using CashFlow.BusinessObjects;
using CashFlow.BusinessLogic;
using CashFlow.BusinessObjects.Utilities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

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
            Dictionary<string, StringValues> queryStringDictionary = QueryHelpers.ParseQuery(Request.QueryString.ToString());
            foreach (var queryStringKey in queryStringDictionary)
            {
                foreach (var item in queryStringKey.Value)
                {
                    dataFilter.FilterProperties.Add(new KeyValuePair<string, string>(queryStringKey.Key, item));
                }
            }
            dataFilter.SortPropertyName = sortPropertyName;
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
