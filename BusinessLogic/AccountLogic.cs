using CashFlow.BusinessObjects;
using CashFlow.DataAccess.EF;
using CashFlow.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.BusinessLogic
{
    public class AccountLogic
    {
        public List<AccountDto> GetAll()
        {
            using (var context = new CashFlowContext())
            {
                IEnumerable<Account> accounts = context.Account
                    .Include(a => a.AccountBalance)
                    .Include(a => a.TransferAccountFrom)
                    .Include(a => a.TransferAccountTo)
                    .AsEnumerable();
                List<AccountDto> accountDtos = accounts.Select<DataAccess.EF.Account, AccountDto>(a => 
                AccountMapper.Map(a, 
                    a.AccountBalance.OrderByDescending(b => b.StartDate).First().Balance 
                    - a.TransferAccountFrom.Sum(t => t.Amount) 
                    + a.TransferAccountTo.Sum(t => t.Amount)))
                    .ToList();
                return accountDtos;
            }
        }

        public AccountDto GetById(long accountId)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.Account efAccount = context.Account
                    .Include(a => a.AccountBalance)
                    .Include(a => a.TransferAccountFrom)
                    .Include(a => a.TransferAccountTo)
                    .SingleOrDefault(t => t.Id == accountId);
                if (efAccount != null)
                {
                    AccountDto Account = AccountMapper.Map(efAccount, efAccount.AccountBalance.OrderByDescending(b => b.StartDate).First().Balance
                    - efAccount.TransferAccountFrom.Sum(t => t.Amount)
                    + efAccount.TransferAccountTo.Sum(t => t.Amount));
                    return Account;
                }
                else
                {
                    return null;
                }
            }
        }

        public AccountHistoryDto GetAccountHistory(long accountId)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.Account efAccount = context.Account
                    .Include(a => a.AccountBalance)
                    .Include(a => a.TransferAccountFrom)
                    .Include(a => a.TransferAccountTo)
                    .SingleOrDefault(t => t.Id == accountId);
                if (efAccount != null)
                {
                    AccountBalance lastAccountBalance = efAccount.AccountBalance.OrderByDescending(b => b.StartDate).First();
                    AccountHistoryDto account = AccountHistoryMapper.Map(
                        efAccount,
                        lastAccountBalance,
                        lastAccountBalance.Balance - efAccount.TransferAccountFrom.Sum(t => t.Amount) + efAccount.TransferAccountTo.Sum(t => t.Amount),
                        efAccount.TransferAccountTo.OrderByDescending(t => t.TransferDate),
                        efAccount.TransferAccountFrom.OrderByDescending(t => t.TransferDate));
                    return account;
                }
                else
                {
                    return null;
                }
            }
        }

        public long? Save(AccountDto account)
        {
            if (account != null)
            {
                using (var context = new CashFlowContext())
                {
                    if (account.Id.HasValue)
                    {
                        context.Account.Update(AccountMapper.Map(account));
                    }
                    else
                    {
                        Account accountMapped = AccountMapper.Map(account);
                        context.Account.Add(accountMapped);
                        account.Id = accountMapped.Id;
                        AccountBalance accountBalance = new AccountBalance();
                        accountBalance.AccountId = account.Id.Value;
                        accountBalance.Balance = 0;
                        accountBalance.StartDate = DateTime.Now;
                        context.AccountBalance.Add(accountBalance);
                    }
                    context.SaveChanges();
                }
                return account.Id;
            }
            else
            {
                return null;
            }
        }

        public void Delete(long id)
        {
            using (var context = new CashFlowContext())
            {
                var Account = context.Account.FirstOrDefault(t => t.Id == id);
                if (Account != null)
                {
                    context.Remove(Account);
                    context.SaveChanges();
                }
            }
        }
    }
}
