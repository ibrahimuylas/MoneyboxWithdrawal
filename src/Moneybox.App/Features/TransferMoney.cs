using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney : MoneyBase, ITransferMoney
    {
        private readonly IWithdrawMoney withdrawMoney;

        public TransferMoney(IWithdrawMoney withdrawMoney, IAccountRepository accountRepository) : base(accountRepository)
        {
            this.withdrawMoney = withdrawMoney;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var to = this.accountRepository.GetAccountById(toAccountId);

            if (to.PayIn(amount)
                && withdrawMoney.Execute(fromAccountId, amount))
            {
                base.accountRepository.Update(to);
            }
        }
    }
}
