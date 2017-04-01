using CashFlow.BusinessObjects.Enums;
using System;

namespace CashFlow.BusinessObjects
{
    public class TransferSchemaDto
    {
        public long? Id { get; set; }
        public long AccountFromId { get; set; }
        public long AccountToId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferStartDate { get; set; }
        public TransferPeriod TransferPeriod { get; set; }
        public DateTime? TransferEndDate { get; set; }
    }
}
