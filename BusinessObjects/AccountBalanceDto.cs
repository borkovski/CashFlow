using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.DataAccess.EF;

namespace CashFlow.BusinessObjects
{
    public class AccountBalanceDto
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime StartDate { get; set; }

        public static AccountBalanceDto Map(AccountBalance accountBalance)
        {
            if (accountBalance == null)
            {
                return null;
            }
            AccountBalanceDto accountBalanceDto = new AccountBalanceDto();
            accountBalanceDto.AccountId = accountBalance.AccountId;
            accountBalanceDto.Balance = accountBalance.Balance;
            accountBalanceDto.Id = accountBalance.Id;
            accountBalanceDto.StartDate = accountBalance.StartDate;
            return accountBalanceDto;
        }

        public static AccountBalance Map(AccountBalanceDto accountBalanceDto)
        {
            if (accountBalanceDto == null)
            {
                return null;
            }
            AccountBalance accountBalance = new AccountBalance();
            accountBalance.AccountId = accountBalanceDto.AccountId;
            accountBalance.Balance = accountBalanceDto.Balance;
            accountBalance.Id = accountBalanceDto.Id;
            accountBalance.StartDate = accountBalanceDto.StartDate;
            return accountBalance;
        }
    }
}
