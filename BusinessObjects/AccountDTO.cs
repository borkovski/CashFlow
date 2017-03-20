using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects
{
    public class AccountDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
