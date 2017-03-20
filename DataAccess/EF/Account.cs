using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class Account
    {
        public Account()
        {
            Transfer = new HashSet<Transfer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public virtual ICollection<Transfer> Transfer { get; set; }
    }
}
