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
        public long Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }

        public static AccountDto Map(Account account)
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
            account.Id = accountDto.Id;
            account.Name = accountDto.Name;
            return account;
        }
    }
}
