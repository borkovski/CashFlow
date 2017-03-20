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
        public List<TransferDTO> Get()
        {
            using (var context = new CashFlowContext())
            {
                List<TransferDTO> transfers = context.Transfer.AsEnumerable().Select<DataAccess.EF.Transfer, TransferDTO>(t => Convert(t)).ToList();
                return transfers;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TransferDTO Get(int id)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.Transfer efTransfer = context.Transfer.SingleOrDefault(t => t.Id == id);
                if(efTransfer != null)
                {
                    TransferDTO transfer = Convert(efTransfer);
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
        public TransferDTO Post([FromBody]TransferDTO transfer)
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
                            Title = transfer.Title,
                            AccountId = transfer.Account
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
                            Title = transfer.Title,
                            AccountId = transfer.Account
                        });
                        context.SaveChanges();
                    }
                }
                return transfer;
            }
            else
            {
                return new TransferDTO();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TransferDTO transfer)
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

        public static TransferDTO Convert(DataAccess.EF.Transfer t)
        {
            TransferDTO transferDTO = new TransferDTO();

            transferDTO.Id = t.Id;
            transferDTO.Amount = t.Amount;
            transferDTO.Date = t.Date;
            transferDTO.Direction = (Direction)t.DirectionId;
            transferDTO.FinishDate = t.FinishDate;
            transferDTO.IsContinuous = t.IsContinuous;
            transferDTO.IsRepeated = t.IsRepeated;
            if (t.RepeatPeriodId.HasValue)
            {
                transferDTO.RepeatPeriod = (RepeatPeriod)t.RepeatPeriodId;
            }
            transferDTO.Title = t.Title;
            transferDTO.Account = t.AccountId;
            return transferDTO;
        }
    }
}
