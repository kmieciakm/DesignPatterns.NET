using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern.BankAccount
{
    /// <summary>
    /// Bank account use case.
    /// Command pattern is used to add possibility to undo transactions.
    /// </summary>
    public class BankAccountCommand : IReversibleCommand
    {
        private BankAccount Account { get; }
        private TransactionType Action { get; }
        private int Amount { get; }
        private TransationManager TransactionManager { get; }
        public BankAccountCommand(BankAccount account, TransactionType action, int amount)
        {
            Account = account;
            TransactionManager = new TransationManager(Account);
            Action = action;
            Amount = amount;
        }

        public void Execute()
        {
            TransactionManager.MakeTransaction(new Transaction(Action, Amount));
        }
        public void Undo()
        {
            var transaction = TransactionManager.UndoTransaction();
        }

        private class Transaction
        {
            public TransactionType Type { get; set; }
            public int Amount { get; set; }
            public DateTime TransactionTime { get; set; }
            public Transaction(TransactionType type, int amount)
            {
                Type = type;
                Amount = amount;
                TransactionTime = DateTime.Now;
            }
        }

        private class TransationManager
        {
            public BankAccount BankAccount { get; set; }
            public TransationManager(BankAccount bankAccount)
            {
                BankAccount = bankAccount;
            }

            private Stack<Transaction> Operations { get; set; } = new Stack<Transaction>();
            public void MakeTransaction(Transaction transaction)
            {
                switch (transaction.Type)
                {
                    case TransactionType.DEPOSIT:
                        BankAccount.Deposit(transaction.Amount);
                        break;
                    case TransactionType.WITHDRAW:
                        BankAccount.Withdraw(transaction.Amount);
                        break;
                    default: throw new NotSupportedException($"Bank does not support {transaction.Type}");
                }
                Operations.Push(transaction);
            }
            public Transaction UndoTransaction()
            {
                Transaction lastTransaction;
                if (Operations.TryPop(out lastTransaction))
                {
                    switch (lastTransaction.Type)
                    {
                        case TransactionType.DEPOSIT:
                            BankAccount.Withdraw(lastTransaction.Amount);
                            break;
                        case TransactionType.WITHDRAW:
                            BankAccount.Deposit(lastTransaction.Amount);
                            break;
                        default: throw new NotSupportedException($"Bank does not support {lastTransaction.Type}");
                    }
                }

                return lastTransaction;
            }
        }
    }
}
