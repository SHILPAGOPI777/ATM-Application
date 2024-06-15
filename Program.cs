using System;
using System.Collections.Generic;

namespace ATM_Application
{
    // ATM APPLICATION SHILPA GOPI 8893284
    internal class Program
    {
        static List<Account> accounts = new List<Account>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("=====Welcome To ATM Application=====");
                Console.WriteLine("Choose the following options by the number associated with it...!:");

                Console.WriteLine("1. Create Account");
                Console.WriteLine();

                Console.WriteLine("2. Select Account");
                Console.WriteLine();

                Console.WriteLine("3. Exit");
                Console.WriteLine();

                Console.Write("==Please choose an option:== ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        SelectAccount();
                        break;
                    case "3":
                        Console.WriteLine("Exiting application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void CreateAccount()
        {
            Console.Write("Enter Account Holder's Name: /n");
            string accountName = Console.ReadLine();

            Console.Write("Enter Account Number (Between 100 to 1000): \n");
            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("Invalid account number. Please try again.");
                return;
            }

            Console.Write("Enter Annual Interest Rate ( should be bess than 3.0%): \n");
            if (!double.TryParse(Console.ReadLine(), out double annualInterestRate))
            {
                Console.WriteLine("Invalid interest rate. Please try again.");
                return;
            }

            Console.Write("Enter Initial Balance: /n");
            if (!double.TryParse(Console.ReadLine(), out double initialBalance))
            {
                Console.WriteLine("Invalid initial balance. Please try again.");
                return;
            }

            Account newAccount = new Account(accountName, accountNumber, annualInterestRate, initialBalance);
            accounts.Add(newAccount);

            Console.WriteLine("====Account created successfully!====\n");
        }

        static void SelectAccount()
        {
            Console.Write("Enter the account number to select: /n");
            string accountNumberInput = Console.ReadLine();

            if (int.TryParse(accountNumberInput, out int accountNumber))
            {
                Account selectedAccount = accounts.Find(a => a.AccountNumber == accountNumber);

                if (selectedAccount != null)
                {
                    Console.WriteLine($"Account selected: {selectedAccount.AccountName} $\"Initial Balance: {{selectedAccount.InitialBalance}}");
                    //Console.WriteLine($"Initial Balance: {selectedAccount.InitialBalance}");
                    AccountMenu(selectedAccount);
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number.");
            }
        }

        static void AccountMenu(Account account)
        {
            while (true)
            {
                Console.WriteLine("=====Account Menu=====");
                Console.WriteLine("a. Check Balance b. Deposi c.Withdraw d.Display Transactions e.Exit Account\n");
               
                Console.Write("==Please choose an option:== \n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "a":
                        CheckBalance(account);
                        break;
                    case "b":
                        Deposit(account);
                        break;
                    case "c":
                        Withdraw(account);
                        break;
                    case "d":
                        DisplayTransactions(account);
                        break;
                    case "e":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void CheckBalance(Account account)
        {
            Console.WriteLine($"Current Balance: {account.InitialBalance}\n");
        }

        static void Deposit(Account account)
        {
            Console.Write("Enter amount to deposit: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                account.InitialBalance += amount;
                account.Transactions.Add($"Deposited: {amount}");
                Console.WriteLine("Deposit successful!\n");
            }
            else
            {
                Console.WriteLine("Invalid amount. Please try again.");
            }
        }

        static void Withdraw(Account account)
        {
            Console.Write("Enter amount to withdraw: \n");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (amount <= account.InitialBalance)
                {
                    account.InitialBalance -= amount;
                    account.Transactions.Add($"Withdrew: {amount}");
                    Console.WriteLine("Withdrawal successful!\n");
                }
                else
                {
                    Console.WriteLine("Insufficient balance. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please try again.");
            }
        }

        static void DisplayTransactions(Account account)
        {
            Console.WriteLine("=====Transaction History=====");
            foreach (var transaction in account.Transactions)
            {
                Console.WriteLine(transaction);
            }
            Console.WriteLine();
        }
    }

    public class Account
    {
        public int AccountNumber { get; private set; }
        public string AccountName { get; private set; }
        public double AnnualInterestRate { get; private set; }
        public double InitialBalance { get; set; }
        public List<string> Transactions { get; private set; }

        public Account(string accountName, int accountNumber, double annualInterestRate, double initialBalance)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            AnnualInterestRate = annualInterestRate;
            InitialBalance = initialBalance;
            Transactions = new List<string> { $"Account created with initial balance: {initialBalance}" };
        }
    }
}
