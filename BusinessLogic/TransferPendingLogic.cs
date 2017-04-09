using CashFlow.BusinessObjects;
using CashFlow.BusinessObjects.Enums;
using CashFlow.DataAccess.EF;
using CashFlow.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashFlow.BusinessLogic
{
    public class TransferPendingLogic
    {
        public List<TransferDto> GetAll()
        {
            DateTime pendingDate = DateTime.Now.AddDays(1); 
            using (var context = new CashFlowContext())
            {
                List<TransferDto> transfers = context.TransferSchema
                    .Include(t => t.AccountFrom)
                    .Include(t => t.AccountTo)
                    .Where(s => pendingDate > s.TransferStartDate
                        && (pendingDate < s.TransferEndDate || s.TransferEndDate == null)
                        && ((s.TransferPeriod == (int)TransferPeriod.Daily && s.LastTransferDate.HasValue && s.LastTransferDate.Value.Date < pendingDate.AddDays(-1).Date)
                            || (s.TransferPeriod == (int)TransferPeriod.Monthly && s.LastTransferDate.HasValue && s.LastTransferDate.Value.Date < pendingDate.AddMonths(-1).Date)
                            || (s.TransferPeriod == (int)TransferPeriod.Quarterly && s.LastTransferDate.HasValue && s.LastTransferDate.Value.Date < pendingDate.AddMonths(-3).Date)
                            || !s.LastTransferDate.HasValue))
                    .AsEnumerable().Select<DataAccess.EF.TransferSchema, TransferDto>(t => TransferPendingMapper.Map(t)).ToList();
                return transfers;
            }
        }

        public TransferDto GetById(long id)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.TransferSchema efTransferSchema = context.TransferSchema
                    .Include(t => t.AccountFrom)
                    .Include(t => t.AccountTo)
                    .SingleOrDefault(t => t.Id == id);
                if (efTransferSchema != null)
                {
                    TransferDto transfer = TransferPendingMapper.Map(efTransferSchema);
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
                        TransferSchema transferSchema = context.TransferSchema.SingleOrDefault(t => t.Id == transfer.Id);
                        if (transferSchema != null)
                        {
                            Transfer transferMapped = TransferPendingMapper.Map(transfer);
                            context.Transfer.Add(transferMapped);
                            transfer.Id = transferMapped.Id;
                            transferSchema.LastTransferDate = transferMapped.TransferDate;
                            context.TransferSchema.Update(transferSchema);
                        }
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
                var transferSchema = context.TransferSchema.FirstOrDefault(t => t.Id == id);
                if (transferSchema != null)
                {
                    context.Remove(transferSchema);
                    context.SaveChanges();
                }
            }
        }
    }
}
