using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class DictRepeatPeriod
    {
        public DictRepeatPeriod()
        {
            Transfer = new HashSet<Transfer>();
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Transfer> Transfer { get; set; }
    }
}
