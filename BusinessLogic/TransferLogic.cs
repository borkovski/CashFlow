using CashFlow.BusinessObjects;
using CashFlow.DataAccess.EF;
using CashFlow.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CashFlow.BusinessLogic
{
    public class TransferLogic
    {
        public List<TransferDto> GetAll()
        {
            using (var context = new CashFlowContext())
            {
                List<TransferDto> transfers = context.Transfer
                    .Include(t => t.AccountFrom)
                    .Include(t => t.AccountTo)
                    .AsEnumerable()
                    .Select<DataAccess.EF.Transfer, TransferDto>(t => TransferMapper.Map(t))
                    .OrderByDescending(t => t.TransferDate)
                    .ToList();
                return transfers;
            }
        }

        public TransferDto GetById(long id)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.Transfer efTransfer = context.Transfer
                    .Include(t => t.AccountFrom)
                    .Include(t => t.AccountTo)
                    .SingleOrDefault(t => t.Id == id);
                if (efTransfer != null)
                {
                    TransferDto transfer = TransferMapper.Map(efTransfer);
                    return transfer;
                }
                else
                {
                    return null;
                }
            }
        }

        public long? Save(TransferDto transfer)
        {
            if (transfer != null)
            {
                using (var context = new CashFlowContext())
                {
                    if (transfer.Id.HasValue)
                    {
                        context.Transfer.Update(TransferMapper.Map(transfer));
                    }
                    else
                    {
                        Transfer transferMapped = TransferMapper.Map(transfer);
                        context.Transfer.Add(transferMapped);
                        transfer.Id = transferMapped.Id;
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

        public void Delete(long id)
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
