using System;
using System.Collections.Generic;

namespace CashFlow.DataAccess.EF
{
    public partial class DictTransferPeriod
    {
        public DictTransferPeriod()
        {
            TransferSchema = new HashSet<TransferSchema>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TransferSchema> TransferSchema { get; set; }
    }
}
