using CommandPattern.BankAccount;
using System;
using Xunit;

namespace PatternTests
{
    public class CommandTestCase
    {
        [Fact]
        public void Test_Passes()
        {
            Assert.True(true);
        }

        [Fact]
        public void BankAccountCommand_Deposit()
        {
            var cashToDeposit = 100;
            var account = new BankAccount();
            var bankCommand = new BankAccountCommand(account, TransactionType.DEPOSIT, cashToDeposit);
            var expectedBankBalance = account.Amount + cashToDeposit;

            bankCommand.Execute();

            Assert.Equal(expectedBankBalance, account.Amount);
        }

        [Fact]
        public void BankAccountCommand_Withdraw()
        {
            var cashToWithdraw = 100;
            var account = new BankAccount();
            var bankCommand = new BankAccountCommand(account, TransactionType.WITHDRAW, cashToWithdraw);
            var expectedBankBalance = account.Amount - cashToWithdraw;

            bankCommand.Execute();

            Assert.Equal(expectedBankBalance, account.Amount);
        }


        [Theory]
        [InlineData(TransactionType.DEPOSIT)]
        [InlineData(TransactionType.WITHDRAW)]
        public void BankAccountCommand_Undo(TransactionType transactionType)
        {
            var cashToWithdraw = 100;
            var account = new BankAccount();
            var bankCommand = new BankAccountCommand(account, transactionType, cashToWithdraw);
            var expectedBankBalance = account.Amount;

            bankCommand.Execute();
            bankCommand.Undo();

            Assert.Equal(expectedBankBalance, account.Amount);
        }

        [Fact]
        public void BankAccountCommand_Deposit_Twice_Undo_Once()
        {
            var cashToDeposit = 100;
            var account = new BankAccount();
            var bankCommand = new BankAccountCommand(account, TransactionType.DEPOSIT, cashToDeposit);
            var expectedBankBalance = account.Amount + cashToDeposit;

            bankCommand.Execute();
            bankCommand.Execute();
            bankCommand.Undo();

            Assert.Equal(expectedBankBalance, account.Amount);
        }

        [Fact]
        public void BankAccountCommand_Withdraw_Twice_Undo_Once()
        {
            var cashToDeposit = 100;
            var account = new BankAccount();
            var bankCommand = new BankAccountCommand(account, TransactionType.WITHDRAW, cashToDeposit);
            var expectedBankBalance = account.Amount - cashToDeposit;

            bankCommand.Execute();
            bankCommand.Execute();
            bankCommand.Undo();

            Assert.Equal(expectedBankBalance, account.Amount);
        }

        [Fact]
        public void BankAccountCommand_Withdraw_Once_Undo_Twice()
        {
            var cashToDeposit = 100;
            var account = new BankAccount();
            var bankCommand = new BankAccountCommand(account, TransactionType.WITHDRAW, cashToDeposit);
            var expectedBankBalance = account.Amount;

            bankCommand.Execute();
            bankCommand.Undo();
            bankCommand.Undo();

            Assert.Equal(expectedBankBalance, account.Amount);
        }

        [Fact]
        public void BankAccountCommand_Undo_Many_Transactions()
        {
            var cashToDeposit = 100;
            var cashToWithdraw = 50;
            var account = new BankAccount();
            var depositCommand = new BankAccountCommand(account, TransactionType.DEPOSIT, cashToDeposit);
            var withdrawCommand = new BankAccountCommand(account, TransactionType.WITHDRAW, cashToWithdraw);
            var expectedBankBalance = account.Amount;

            depositCommand.Execute();
            withdrawCommand.Execute();
            depositCommand.Undo();
            withdrawCommand.Undo();

            Assert.Equal(expectedBankBalance, account.Amount);
        }
    }
}
