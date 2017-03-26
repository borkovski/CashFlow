using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class Account
    {
        public Account()
        {
            AccountBalance = new HashSet<AccountBalance>();
            TransferAccountFrom = new HashSet<Transfer>();
            TransferAccountTo = new HashSet<Transfer>();
            TransferSchemaAccountFrom = new HashSet<TransferSchema>();
            TransferSchemaAccountTo = new HashSet<TransferSchema>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int AccountTypeId { get; set; }

        public virtual ICollection<AccountBalance> AccountBalance { get; set; }
        public virtual ICollection<Transfer> TransferAccountFrom { get; set; }
        public virtual ICollection<Transfer> TransferAccountTo { get; set; }
        public virtual ICollection<TransferSchema> TransferSchemaAccountFrom { get; set; }
        public virtual ICollection<TransferSchema> TransferSchemaAccountTo { get; set; }
        public virtual DictAccountType AccountType { get; set; }
    }
}
