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
            Console.Write("Enter Account Holder's Name: ");
            string accountName = Console.ReadLine();

            Console.Write("Enter Account Number: ");
            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("Invalid account number. Please try again.");
                return;
            }

            Console.Write("Enter Annual Interest Rate (as a percentage): ");
            if (!double.TryParse(Console.ReadLine(), out double annualInterestRate))
            {
                Console.WriteLine("Invalid interest rate. Please try again.");
                return;
            }

            Account newAccount = new Account(accountName, accountNumber, annualInterestRate);
            accounts.Add(newAccount);

            Console.WriteLine("====Account created successfully!\n");
        }

        static void SelectAccount()
        {
            Console.WriteLine("Available accounts:");

            foreach (var account in accounts)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}, Account Name: {account.AccountName}");
            }

            Console.Write("Enter the account number to select: ");
            string accountNumberInput = Console.ReadLine();

            if (int.TryParse(accountNumberInput, out int accountNumber))
            {
                Account selectedAccount = accounts.Find(a => a.AccountNumber == accountNumber);

                if (selectedAccount != null)
                {
                    Console.WriteLine($"Account selected: {selectedAccount.AccountName}");
                   

                    /// ENter e=initail balance to be implemented
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
    }

    public class Account
    {
        public int AccountNumber { get; private set; }
        public string AccountName { get; private set; }
        public double AnnualInterestRate { get; private set; }

        public Account(string accountName, int accountNumber, double annualInterestRate)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            AnnualInterestRate = annualInterestRate;
        }
    }
}
