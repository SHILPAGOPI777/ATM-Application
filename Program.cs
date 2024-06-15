using System;

namespace ATM_Application
{
    // ATM APPLICATION SHILPA GOPI 8893284
    internal class Program
    {
      static Bank bank = new Bank();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("============= Welcome To ATM Application ============\n");
                Console.WriteLine("        Choose An Option From The Following.!         \n");
                Console.WriteLine("1. Create Account      2. Select Account      3. Exit ");
                Console.WriteLine("\n===================================================== \n");
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
            Console.Write("Enter Account Holder's Name: \n");
            string accountName = Console.ReadLine();

            Console.Write("Enter Account Number (Between 100 to 1000): \n");
            string accountNumberStr = Console.ReadLine();
            if (!int.TryParse(accountNumberStr, out int accountNumber) || accountNumber < 100 || accountNumber > 1000)
            {
                Console.WriteLine("Invalid account number. Please try again.");
                return;
            }

            Console.Write("Enter Annual Interest Rate (should be less than 3.0%): \n");
            string annualInterestRateStr = Console.ReadLine();
            if (!double.TryParse(annualInterestRateStr, out double annualInterestRate) || annualInterestRate >= 3.0)
            {
                Console.WriteLine("Invalid interest rate. Please try again.");
                return;
            }

            Console.Write("Enter Initial Balance: \n");
            string initialBalanceStr = Console.ReadLine();
            if (!double.TryParse(initialBalanceStr, out double initialBalance))
            {
                Console.WriteLine("Invalid initial balance. Please try again.");
                return;
            }

            Account newAccount = new Account(accountNumber, initialBalance, annualInterestRate, accountName);
            bank.AddAccount(newAccount);
            Console.WriteLine("============= Account created successfully! =============\n");
        }

        static void SelectAccount()
        {
            Console.Write("Enter the account number to select: \n");
            string accountNumberStr = Console.ReadLine();
            if (int.TryParse(accountNumberStr, out int accountNumber))
            {
                Account selectedAccount = bank.RetrieveAccount(accountNumber);
                if (selectedAccount != null)
                {
                    Console.WriteLine($"Welcome, {selectedAccount.AccountName}");
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
                Console.WriteLine("============= Account Menu =============");
                Console.WriteLine("1. Check Balance      2. Deposit      3. Withdraw      4. Display Transactions      5. Exit Account\n");

                Console.Write("============= Please choose an option: ============= \n");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckBalance(account);
                        break;
                    case "2":
                        Deposit(account);
                        break;
                    case "3":
                        Withdraw(account);
                        break;
                    case "4":
                        DisplayTransactions(account);
                        break;
                    case "5":
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
                account.AddTransaction($"Deposited: {amount}");
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
                    account.AddTransaction($"Withdrew: {amount}");
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
            Console.WriteLine("============= Transaction History =============");
            Console.WriteLine($"Annual Interest Rate: {account.AnnualInterestRate}%");
            for (int i = 0; i < account.TransactionCount; i++)
            {
                Console.WriteLine(account.Transactions[i]);
            }
            Console.WriteLine();
        }
    }

    public class Account // created class and adding objects
    {
        public const int MaxTransactions = 100;
        public int TransactionCount { get; set; }
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        public double AnnualInterestRate { get; set; }
        public double InitialBalance { get; set; }
        public string[] Transactions { get; set; }
      

        public Account(int accountNumber, double initialBalance, double annualInterestRate, string accountName = "My Account")
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            AnnualInterestRate = annualInterestRate;
            InitialBalance = initialBalance;
            Transactions = new string[MaxTransactions];
            Transactions[0] = $"Account created with initial balance: {initialBalance}"; // Creating array of transactions
            TransactionCount = 1;
        }

        public void AddTransaction(string transaction)    //ADDCOMMENT
        {
            if (TransactionCount <= MaxTransactions)
            {
                Transactions[TransactionCount] = transaction;
                TransactionCount++;
            }
            else
            {
                Console.WriteLine("Transaction limit reached. Cannot add more transactions.");
            }
        }
    }// Class Account created
    public class Bank
    {
        private const int MaxAccounts = 100; // Maximum account limit set to 100
        private int accountCount;
        private Account[] accounts;
      

        public Bank() // Class bank created
        {
            accounts = new Account[MaxAccounts];
            accountCount = 0;
            CreateDefaultAccounts(); // Default account created and count to be set
        }

        private void CreateDefaultAccounts()
        {
            for (int i = 0; i < 10; i++) // 10 default account is created 
            {
                accounts[accountCount] = new Account(100 + i, 100, 2.5); // account number will start from 100 and increment to 10 accounts 
                accountCount++;
            }
        }

        public void AddAccount(Account newAccount) // To limit maximum account limit to 100
        {
            if (accountCount < MaxAccounts)
            {
                accounts[accountCount++] = newAccount;
            }
            else
            {
                Console.WriteLine("Account limit reached. Cannot add more accounts."); 
            }
        }

        public Account RetrieveAccount(int accountNumber)
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
    }
}
