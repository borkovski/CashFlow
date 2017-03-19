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
        public List<BusinessObjects.Transfer> Get()
        {
            using (var context = new CashFlowContext())
            {
                List<BusinessObjects.Transfer> transfers = context.Transfer.AsEnumerable().Select<DataAccess.EF.Transfer, BusinessObjects.Transfer>(t => Convert(t)).ToList();
                return transfers;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public BusinessObjects.Transfer Get(int id)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.Transfer efTransfer = context.Transfer.SingleOrDefault(t => t.Id == id);
                if(efTransfer != null)
                {
                    BusinessObjects.Transfer transfer = Convert(efTransfer);
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
        public BusinessObjects.Transfer Post([FromBody]BusinessObjects.Transfer transfer)
        {
            if(transfer != null)
            {
                if (transfer.Id.HasValue)
                {
                    using (var context = new CashFlowContext())
                    {
                        context.Transfer.Update(new DataAccess.EF.Transfer
                        {
                            Id = transfer.Id.Value,
                            Amount = transfer.Amount,
                            Date = transfer.Date,
                            DirectionId = (int)transfer.Direction,
                            FinishDate = transfer.FinishDate,
                            IsContinuous = transfer.IsContinuous,
                            IsRepeated = transfer.IsRepeated,
                            RepeatPeriodId = (int)transfer.RepeatPeriod,
                            Title = transfer.Title
                        });
                        context.SaveChanges();
                    }
                }
                else
                {
                    using(var context = new CashFlowContext())
                    {
                        context.Transfer.Add(new DataAccess.EF.Transfer
                        {
                            Amount = transfer.Amount,
                            Date = transfer.Date,
                            DirectionId = (int)transfer.Direction,
                            FinishDate = transfer.FinishDate,
                            IsContinuous = transfer.IsContinuous,
                            IsRepeated = transfer.IsRepeated,
                            RepeatPeriodId = (int)transfer.RepeatPeriod,
                            Title = transfer.Title
                        });
                        context.SaveChanges();
                    }
                }
                return transfer;
            }
            else
            {
                return new BusinessObjects.Transfer();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]BusinessObjects.Transfer transfer)
        {
            if (transfer != null)
            {
                if (transfer.Id.HasValue)
                {
                    using (var context = new CashFlowContext())
                    {
                        context.Transfer.Update(new DataAccess.EF.Transfer
                        {
                            Id = transfer.Id.Value,
                            Amount = transfer.Amount,
                            Date = transfer.Date,
                            DirectionId = (int)transfer.Direction,
                            FinishDate = transfer.FinishDate,
                            IsContinuous = transfer.IsContinuous,
                            IsRepeated = transfer.IsRepeated,
                            RepeatPeriodId = (int)transfer.RepeatPeriod,
                            Title = transfer.Title
                        });
                        context.SaveChanges();
                    }
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using(var context = new CashFlowContext())
            {
                var transfer = context.Transfer.FirstOrDefault(t => t.Id == id);
                if(transfer != null)
                {
                    context.Remove(transfer);
                    context.SaveChanges();
                }
            }
        }

        public static BusinessObjects.Transfer Convert(DataAccess.EF.Transfer t)
        {
            return new BusinessObjects.Transfer
            {
                Id = t.Id,
                Amount = t.Amount,
                Date = t.Date,
                Direction = (Direction)t.DirectionId,
                FinishDate = t.FinishDate,
                IsContinuous = t.IsContinuous,
                IsRepeated = t.IsRepeated,
                RepeatPeriod = (RepeatPeriod)t.RepeatPeriodId,
                Title = t.Title
            };
        }
    }
}
