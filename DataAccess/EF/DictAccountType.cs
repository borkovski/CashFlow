using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class DictAccountType
    {
        public DictAccountType()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
