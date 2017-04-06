using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class TransferSchema
    {
        public long Id { get; set; }
        public long AccountFromId { get; set; }
        public long AccountToId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferStartDate { get; set; }
        public int TransferPeriod { get; set; }
        public DateTime? TransferEndDate { get; set; }
        public DateTime? LastTransferDate { get; set; }

        public virtual Account AccountFrom { get; set; }
        public virtual Account AccountTo { get; set; }
        public virtual DictTransferPeriod TransferPeriodNavigation { get; set; }
    }
}
