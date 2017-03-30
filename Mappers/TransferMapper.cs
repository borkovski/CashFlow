using BusinessObjects;
using CashFlow.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.Mappers
{
    public static class TransferMapper
    {
        public static TransferDto Map(Transfer transfer)
        {
            if (transfer == null)
            {
                return null;
            }
            TransferDto transferDto = new TransferDto();
            transferDto.AccountFromId = transfer.AccountFromId;
            transferDto.AccountToId = transfer.AccountToId;
            transferDto.Amount = transfer.Amount;
            transferDto.Id = transfer.Id;
            transferDto.Title = transfer.Name;
            transferDto.TransferDate = transfer.TransferDate;
            return transferDto;
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
            if (transferDto.Id.HasValue)
            {
                transfer.Id = transferDto.Id.Value;
            }
            transfer.Name = transferDto.Title;
            transfer.TransferDate = transferDto.TransferDate;
            return transfer;
        }
    }
}
