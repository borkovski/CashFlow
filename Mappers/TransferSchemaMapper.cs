using CashFlow.BusinessObjects.Enums;
using CashFlow.BusinessObjects;
using CashFlow.DataAccess.EF;
using System;

namespace CashFlow.Mappers
{
    public static class TransferSchemaMapper
    {
        public static TransferSchemaDto Map(TransferSchema transfer)
        {
            if (transfer == null)
            {
                return null;
            }
            TransferSchemaDto transferSchemaDto = new TransferSchemaDto();
            transferSchemaDto.AccountFromId = transfer.AccountFromId;
            transferSchemaDto.AccountToId = transfer.AccountToId;
            transferSchemaDto.Amount = transfer.Amount;
            transferSchemaDto.Id = transfer.Id;
            transferSchemaDto.Title = transfer.Name;
            if (transfer.TransferEndDate.HasValue)
            {
                transferSchemaDto.TransferEndDate = transfer.TransferEndDate.Value.ToUniversalTime();
            }
            if (Enum.IsDefined(typeof(TransferPeriod), transfer.TransferPeriod))
            {
                transferSchemaDto.TransferPeriod = (TransferPeriod)transfer.TransferPeriod;
            }
            transferSchemaDto.TransferStartDate = transfer.TransferStartDate.Add(transfer.TransferTime).ToUniversalTime();
            return transferSchemaDto;
        }

        public static TransferSchema Map(TransferSchemaDto transferSchemaDto)
        {
            if (transferSchemaDto == null)
            {
                return null;
            }
            TransferSchema transferSchema = new TransferSchema();
            transferSchema.AccountFromId = transferSchemaDto.AccountFromId;
            transferSchema.AccountToId = transferSchemaDto.AccountToId;
            transferSchema.Amount = transferSchemaDto.Amount;
            if (transferSchemaDto.Id.HasValue)
            {
                transferSchema.Id = transferSchemaDto.Id.Value;
            }
            transferSchema.Name = transferSchemaDto.Title;
            if (transferSchemaDto.TransferEndDate.HasValue)
            {
                transferSchema.TransferEndDate = transferSchemaDto.TransferEndDate.Value.ToLocalTime();
            }
            transferSchema.TransferPeriod = (int)transferSchemaDto.TransferPeriod;
            transferSchema.TransferStartDate = transferSchemaDto.TransferStartDate.ToLocalTime().Date;
            transferSchema.TransferTime = transferSchemaDto.TransferStartDate.ToLocalTime().TimeOfDay;
            return transferSchema;
        }
    }
}
