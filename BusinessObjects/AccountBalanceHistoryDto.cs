using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects
{
    public class AccountBalanceHistoryDto
    {
        public DateTime BalanceDate { get; set; }
        public decimal Balance { get; set; }
    }
}
