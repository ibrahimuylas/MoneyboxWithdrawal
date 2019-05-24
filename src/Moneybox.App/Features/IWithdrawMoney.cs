using System;
namespace Moneybox.App.Features
{
    public interface IWithdrawMoney
    {
        bool Execute(Guid accountId, decimal amount);
    }
}
