using BusinessObjects.Enums;
using CashFlow.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects
{
    public class TransferSchemaDto
    {
        public long Id { get; set; }
        public long AccountFromId { get; set; }
        public long AccountToId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferStartDate { get; set; }
        public TransferPeriod TransferPeriod { get; set; }
        public DateTime? TransferEndDate { get; set; }

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
            transferSchemaDto.Name = transfer.Name;
            transferSchemaDto.TransferEndDate = transfer.TransferEndDate;
            if (Enum.IsDefined(typeof(TransferPeriod), transfer.TransferPeriod))
            {
                transferSchemaDto.TransferPeriod = (TransferPeriod)transfer.TransferPeriod;
            }
            transferSchemaDto.TransferStartDate = transfer.TransferStartDate;
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
            transferSchema.Id = transferSchemaDto.Id;
            transferSchema.Name = transferSchemaDto.Name;
            transferSchema.TransferEndDate = transferSchemaDto.TransferEndDate;
            transferSchema.TransferPeriod = (int)transferSchemaDto.TransferPeriod;
            transferSchema.TransferStartDate = transferSchemaDto.TransferStartDate;
            return transferSchema;
        }
    }
}
