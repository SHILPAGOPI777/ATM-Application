using System;

namespace ATM_Application
{
    // ATM APPLICATION SHILPA GOPI 8893284
    internal class Program
    {
        static Account[] accounts = new Account[100];
        static int accountCount = 0;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("=====Welcome To ATM Application=====\n");
                Console.WriteLine("Choose the following options by the number associated with it...!:\n");

                // Actions available, need user's input
                Console.WriteLine("1. Create Account      2. Select Account      3. Exit ");
                Console.WriteLine();

                Console.Write("==Please choose an option:== \n");

                string choice = Console.ReadLine();

                // actions for selecting each case

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

        static void CreateAccount() // Account details for new accounts
        {
            Console.Write("Enter Account Holder's Name: \n");
            string accountName = Console.ReadLine();

            Console.Write("Enter Account Number (Between 100 to 1000): \n");
            string AccntNumString = Console.ReadLine();

            if (!int.TryParse(AccntNumString, out int accountNumber))
            {
                Console.WriteLine("Invalid account number. Please try again.");
                return;
            }

            Console.Write("Enter Annual Interest Rate (should be less than 3.0%): \n");
            string IntRateString = Console.ReadLine();
            if (!double.TryParse(IntRateString, out double annualInterestRate) || annualInterestRate >= 3.0)
            {
                Console.WriteLine("Invalid interest rate. Please try again.");
                return;
            }

            Console.Write("Enter Initial Balance: \n");
            string InitBalString = Console.ReadLine();
            if (!double.TryParse(InitBalString, out double initialBalance))
            {
                Console.WriteLine("Invalid initial balance. Please try again.");
                return;
            }

            Account newAccount = new Account(accountName, accountNumber, annualInterestRate, initialBalance);
            accounts[accountCount++] = newAccount;

            Console.WriteLine("====Account created successfully!====\n");
        }

        static void SelectAccount()
        {
            Console.Write("Enter the account number to select: \n");
            string accountNumberInput = Console.ReadLine();

            if (int.TryParse(accountNumberInput, out int accountNumber))
            {
                Account selectedAccount = FindAccount(accountNumber);

                if (selectedAccount != null)
                {
                    Console.WriteLine($"Welcome, {selectedAccount.AccountName}!");
                    Console.WriteLine($"Account selected: {selectedAccount.AccountName}");
                    Console.WriteLine($"Initial Balance: {selectedAccount.InitialBalance}");
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

        static Account FindAccount(int accountNumber)
        {
            for (int i = 0; i < accountCount; i++)
            {
                if (accounts[i].AccountNumber == accountNumber)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        static void AccountMenu(Account account)
        {
            while (true)
            {
                Console.WriteLine("=====Account Menu=====");
                Console.WriteLine("a. Check Balance      b. Deposit      c. Withdraw      d. Display Transactions      e. Exit Account\n");

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
                AddTransaction(account, $"Deposited: {amount}");
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
                    AddTransaction(account, $"Withdrew: {amount}");
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
            Console.WriteLine($"Annual Interest Rate: {account.AnnualInterestRate}%");
            for (int i = 0; i < account.TransactionCount; i++)
            {
                Console.WriteLine(account.Transactions[i]);
            }
            Console.WriteLine();
        }

        static void AddTransaction(Account account, string transaction)
        {
            if (account.TransactionCount < account.Transactions.Length)
            {
                account.Transactions[account.TransactionCount++] = transaction;
            }
            else
            {
                Console.WriteLine("Transaction limit reached. Cannot add more transactions.");
            }
        }
    }

    public class Account
    {
        private const int MaxTransactions = 100;

        public int AccountNumber { get; private set; }
        public string AccountName { get; private set; }
        public double AnnualInterestRate { get; private set; }
        public double InitialBalance { get; set; }
        public string[] Transactions { get; private set; }
        public int TransactionCount { get; set; }

        public Account(string accountName, int accountNumber, double annualInterestRate, double initialBalance)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            AnnualInterestRate = annualInterestRate;
            InitialBalance = initialBalance;
            Transactions = new string[MaxTransactions];
            Transactions[0] = $"Account created with initial balance: {initialBalance}";
            TransactionCount = 1;
        }
    }
}
