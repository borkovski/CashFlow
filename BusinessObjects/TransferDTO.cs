using System;

namespace CashFlow.BusinessObjects
{
    public class TransferDto
    {
        public long? Id { get; set; }
        public long AccountFromId { get; set; }
        public string AccountFrom { get; set; }
        public long AccountToId { get; set; }
        public string AccountTo { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
