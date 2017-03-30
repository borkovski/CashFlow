using CashFlow.BusinessObjects.Enums;
using CashFlow.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects
{
    public class AccountDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
