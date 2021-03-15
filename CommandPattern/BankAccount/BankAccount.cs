using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern.BankAccount
{
    public class BankAccount
    {
        public int Amount { get; private set; }
        public void Deposit(int amount) => Amount += amount;
        public void Withdraw(int amount) => Amount -= amount;
    }
}
