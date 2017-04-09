using CashFlow.BusinessObjects;
using CashFlow.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.Mappers
{
    public class TransferPendingMapper
    {
        public static TransferDto Map(TransferSchema transferSchemaDto)
        {
            if (transferSchemaDto == null)
            {
                return null;
            }
            TransferDto transfer = new TransferDto();
            transfer.Id = transferSchemaDto.Id;
            transfer.AccountFrom = transferSchemaDto.AccountFrom.Name;
            transfer.AccountFromId = transferSchemaDto.AccountFromId;
            transfer.AccountTo = transferSchemaDto.AccountTo.Name;
            transfer.AccountToId = transferSchemaDto.AccountToId;
            transfer.Amount = transferSchemaDto.Amount;
            transfer.Title = transferSchemaDto.Name;
            transfer.TransferDate = transferSchemaDto.TransferStartDate.ToUniversalTime();
            return transfer;
        }

        public static Transfer Map(TransferDto transferDto)
        {
            if (transferDto == null)
            {
                return null;
            }
            Transfer transfer = new Transfer();
            transfer.AccountFromId = transferDto.AccountFromId;
            transfer.AccountToId = transferDto.AccountToId;
            transfer.Amount = transferDto.Amount;
            transfer.Name = transferDto.Title;
            transfer.TransferDate = transferDto.TransferDate.ToLocalTime();
            return transfer;
        }
    }
}