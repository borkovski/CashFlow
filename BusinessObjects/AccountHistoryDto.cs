using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.BusinessObjects
{
    public class AccountHistoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal AccountStartBalance { get; set; }
        public decimal AccountCurrentBalance { get; set; }
        public List<TransferDto> IncomingTransfers { get; set; }
        public List<TransferDto> OutgoingTransfers { get; set; }

        public AccountHistoryDto()
        {
            IncomingTransfers = new List<TransferDto>();
            OutgoingTransfers = new List<TransferDto>();
        }
    }
}
