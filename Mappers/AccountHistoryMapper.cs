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
        public static AccountHistoryDto Map(Account account, AccountBalance startBalance, decimal currentBalance, IEnumerable<Transfer> incomingTrasnfers, IEnumerable<Transfer> outgoingTransfers)
        {
            if (account == null)
            {
                return null;
            }
            AccountHistoryDto accountDto = new AccountHistoryDto();
            accountDto.Id = account.Id;
            accountDto.Name = account.Name;
            accountDto.AccountStartBalance = startBalance.Balance;
            accountDto.AccountCurrentBalance = currentBalance;
            foreach (var transfer in incomingTrasnfers)
            {
                accountDto.IncomingTransfers.Add(TransferMapper.Map(transfer));
            }
            foreach (var transfer in outgoingTransfers)
            {
                accountDto.OutgoingTransfers.Add(TransferMapper.Map(transfer));
            }
            int numberOfDays = (int)Math.Ceiling((DateTime.Now - startBalance.StartDate).TotalDays);
            DateTime accountBalanceHistoryDate = startBalance.StartDate;
            decimal historyBalance = startBalance.Balance;
            for (int i = 0; i < numberOfDays; i++)
            {
                historyBalance = historyBalance
                    + incomingTrasnfers.Where(t => t.TransferDate.Date == accountBalanceHistoryDate.Date).Sum(t => t.Amount)
                    - outgoingTransfers.Where(t => t.TransferDate.Date == accountBalanceHistoryDate.Date).Sum(t => t.Amount);
                accountDto.AccountBalanceHistory.Add(new AccountBalanceHistoryDto
                {
                    BalanceDate = accountBalanceHistoryDate,
                    Balance = historyBalance
                });
                accountBalanceHistoryDate = accountBalanceHistoryDate.AddDays(1);
            }
            return accountDto;
        }
    }
}
