    using System;
using Moneybox.App.DataAccess;

namespace Moneybox.App.Features
{
    public abstract class MoneyBase
    {
        internal IAccountRepository accountRepository;

        public MoneyBase(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
    }
}
