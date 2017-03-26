using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using CashFlow.DataAccess.EF;
using BusinessObjects.Enums;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        // GET: api/values
        [HttpGet]
        public List<TransferDto> Get()
        {
            using (var context = new CashFlowContext())
            {
                List<TransferDto> transfers = context.Transfer.AsEnumerable().Select<DataAccess.EF.Transfer, TransferDto>(t => TransferDto.Map(t)).ToList();
                return transfers;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TransferDto Get(int id)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.Transfer efTransfer = context.Transfer.SingleOrDefault(t => t.Id == id);
                if (efTransfer != null)
                {
                    TransferDto transfer = TransferDto.Map(efTransfer);
                    return transfer;
                }
                else
                {
                    return null;
                }
            }
        }

        // POST api/values
        [HttpPost]
        public long? Post([FromBody]TransferDto transfer)
        {
            if (transfer != null)
            {
                using (var context = new CashFlowContext())
                {
                    if (transfer.Id.HasValue)
                    { 
                        context.Transfer.Update(TransferDto.Map(transfer));
                    }
                    else
                    {
                        context.Transfer.Add(TransferDto.Map(transfer));
                    }
                    context.SaveChanges();
                }
                return transfer.Id;
            }
            else
            {
                return null;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TransferDto transfer)
        {
            if (transfer != null)
            {
                if (transfer.Id.HasValue)
                {
                    using (var context = new CashFlowContext())
                    {
                        context.Transfer.Update(TransferDto.Map(transfer));
                        context.SaveChanges();
                    }
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var context = new CashFlowContext())
            {
                var transfer = context.Transfer.FirstOrDefault(t => t.Id == id);
                if (transfer != null)
                {
                    context.Remove(transfer);
                    context.SaveChanges();
                }
            }
        }
    }
}
