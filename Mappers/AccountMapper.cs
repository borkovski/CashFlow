using CashFlow.BusinessObjects;
using CashFlow.BusinessObjects.Enums;
using CashFlow.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.Mappers
{
    public static class AccountMapper
    {
        public static AccountDto Map(Account account, decimal balance)
        {
            if (account == null)
            {
                return null;
            }
            AccountDto accountDto = new AccountDto();
            if (Enum.IsDefined(typeof(AccountType), account.AccountTypeId))
            {
                accountDto.AccountType = (AccountType)account.AccountTypeId;
            }
            accountDto.Id = account.Id;
            accountDto.Name = account.Name;
            accountDto.CurrentBalance = balance;
            return accountDto;
        }

        public static Account Map(AccountDto accountDto)
        {
            if (accountDto == null)
            {
                return null;
            }
            Account account = new Account();
            account.AccountTypeId = (int)accountDto.AccountType;
            if (accountDto.Id.HasValue)
            {
                account.Id = accountDto.Id.Value;
            }
            account.Name = accountDto.Name;
            return account;
        }
    }
}
