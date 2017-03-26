using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class AccountBalance
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Account Account { get; set; }
    }
}
