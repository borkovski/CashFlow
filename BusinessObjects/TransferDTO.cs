using BusinessObjects.Enums;
using CashFlow.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class TransferDto
    {
        public long? Id { get; set; }
        public long AccountFromId { get; set; }
        public long AccountToId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
