using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MyATMAndroid
{
    public class Banks
    {
        private List<Bank> banks;
        public Banks()
        {
            var internalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var banksFilePath = Path.Combine(internalPath, "banks.json");
            if (File.Exists(banksFilePath))
            {
                var banksJson = File.ReadAllText(banksFilePath);
                banks = JsonConvert.DeserializeObject<List<Bank>>(banksJson);
            }
            else
            {
                banks = new List<Bank>
            {
                new Bank
                {
                    Name = "Vodovod",
                    Account = "HR23000000000000000",
                    Address = "Drage Gervaisa 28",
                    Id = 0,
                    BankTransactions = new List<AtmUserTransaction>
                    {
                        new AtmUserTransaction
                        {
                            AccountBalance = 0,
                            Amount = 0,
                            Date = DateTime.Now,
                            Description = "Initial value"
                        }
                    }
                },
                new Bank
                {
                    Name = "Struja",
                    Account = "HR24000000000000000",
                    Address = "Drage Gervaisa 29",
                    Id = 1,
                    BankTransactions = new List<AtmUserTransaction>
                    {
                        new AtmUserTransaction
                        {
                            AccountBalance = 0,
                            Amount = 0,
                            Date = DateTime.Now,
                            Description = "Initial value"
                        }
                    }
                }
            };
            }
        }

        public Bank GetBank(string account)
        {
            return banks.First(b => b.Account == account);
        }
        public void AddTransaction(int id, decimal amount, string comment)
        {
            var bank = banks.First(i => i.Id == id);
            var lastTransaction = bank.BankTransactions.OrderByDescending(i => i.Date).First();
            var currentBalance = lastTransaction.AccountBalance + amount;
            bank.BankTransactions.Add(new AtmUserTransaction
            {
                Amount = amount,
                Date = DateTime.Now,
                Description = comment,
                AccountBalance = currentBalance
            });
            Save();
        }
        public void Save()
            {
                var internalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var banksFilePath = Path.Combine(internalPath, "banks.json");
                var banksJson = JsonConvert.SerializeObject(banks);
                File.WriteAllText(banksFilePath, banksJson);
            }
        }
}