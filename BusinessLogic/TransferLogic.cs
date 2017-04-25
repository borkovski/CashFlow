using CashFlow.BusinessLogic.Utilities;
using CashFlow.BusinessObjects;
using CashFlow.BusinessObjects.Utilities;
using CashFlow.DataAccess.EF;
using CashFlow.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CashFlow.BusinessLogic
{
    public class TransferLogic
    {
        public PagedList<TransferDto> Get(DataFilter dataFilter)
        {
            using (var context = new CashFlowContext())
            {
                PagedList<TransferDto> transfers = new PagedList<TransferDto>(context.Transfer
                    .Include(t => t.AccountFrom)
                    .Include(t => t.AccountTo)
                    .Filter<DataAccess.EF.Transfer, TransferDto>(dataFilter)
                    .AsEnumerable()
                    .Select<DataAccess.EF.Transfer, TransferDto>(t => TransferMapper.Map(t))
                    .ToList());
                transfers.TotalCount = context.Transfer
                    .Include(t => t.AccountFrom)
                    .Include(t => t.AccountTo).Filter<Transfer, TransferDto>(dataFilter.FilterProperties).Count();
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
