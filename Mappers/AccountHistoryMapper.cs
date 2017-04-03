using CashFlow.BusinessObjects;
using CashFlow.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.Mappers
{
    public class AccountHistoryMapper
    {
        public static AccountHistoryDto Map(Account account, decimal startBalance, decimal currentBalance, IEnumerable<Transfer> incomingTrasnfers, IEnumerable<Transfer> outgoingTransfers)
        {
            if (account == null)
            {
                return null;
            }
            AccountHistoryDto accountDto = new AccountHistoryDto();
            accountDto.Id = account.Id;
            accountDto.Name = account.Name;
            accountDto.AccountStartBalance = startBalance;
            accountDto.AccountCurrentBalance = currentBalance;
            foreach (var transfer in incomingTrasnfers)
            {
                accountDto.IncomingTransfers.Add(TransferMapper.Map(transfer));
            }
            foreach (var transfer in outgoingTransfers)
            {
                accountDto.OutgoingTransfers.Add(TransferMapper.Map(transfer));
            }
            return accountDto;
        }
    }
}
