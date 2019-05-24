using System;
using Moneybox.App.Domain.Services;

namespace Moneybox.App
{
    public class Account
    {
        private INotificationService notificationService;

        #region Constructors
        public Account(Guid id, User user, decimal balance, decimal withdrawn, decimal paidIn, INotificationService notificationService)
        {
            Id = id;
            User = user;
            Balance = balance;
            Withdrawn = withdrawn;
            PaidIn = paidIn;

            this.notificationService = notificationService;
        }
        #endregion

        #region Properties
        public const decimal InsufficientLimit = 0m;

        public const decimal FundsLowLimit = 500m;

        public const decimal PayInLimit = 4000m;

        public Guid Id { get; private set; }

        public User User { get; private set; }

        public decimal Balance { get; private set; }

        public decimal Withdrawn { get; private set; }

        public decimal PaidIn { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Calculates and updates new Withraw and Balance value.
        /// </summary>
        /// <returns>Returns true if success.</returns>
        /// <param name="amount">Amount.</param>
        public bool Withdraw(in decimal amount)
        {
            var newBalance = Balance - amount;
            if (newBalance < InsufficientLimit)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (newBalance < FundsLowLimit)
            {
                notificationService.NotifyFundsLow(User?.Email);
            }

            Balance = newBalance;
            Withdrawn -= amount;

            return true;
        }

        /// <summary>
        /// Calculates and updates new PaidIn and Balance value.
        /// </summary>
        /// <returns>Returns true If success</returns>
        /// <param name="amount">Amount.</param>
        public bool PayIn(in decimal amount)
        {
            var paidIn = PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < FundsLowLimit)
            {
                notificationService.NotifyApproachingPayInLimit(User?.Email);
            }

            Balance += amount;
            PaidIn = paidIn;

            return true;
        }
        #endregion

    }
}
