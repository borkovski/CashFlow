using CashFlow.BusinessObjects.Enums;

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
