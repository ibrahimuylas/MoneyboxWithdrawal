using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney : MoneyBase, IWithdrawMoney
    {

        public WithdrawMoney(IAccountRepository accountRepository) : base(accountRepository)
        {
        }

        public bool Execute(Guid accountId, decimal amount)
        {
            var account = this.accountRepository.GetAccountById(accountId);

            if (account.Withdraw(amount))
            {
                base.accountRepository.Update(account);
            }

            return true;
        }
    }
}
