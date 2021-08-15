using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MyATMAndroid
{
    public class AtmUsers
    {
        private List<AtmUser> users;
        public AtmUsers()
        {
            var internalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var usersFilePath = Path.Combine(internalPath, "users.json");
            if (File.Exists(usersFilePath))
            {
                var usersJson = File.ReadAllText(usersFilePath);
                users = JsonConvert.DeserializeObject<List<AtmUser>>(usersJson);
            }
            else
            {
                users = new List<AtmUser>
                {
                    new AtmUser
                    {
                        CardNumber = "1234-1234-1234-1234",
                        Pin = "1234",
                        Name = "Ivan",
                        LastName = "Horvat",
                        Id = 0,
                        Transactions = new List<AtmUserTransaction>
                        {
                            new AtmUserTransaction
                            {
                                AccountBalance = 1000,
                                Amount = 100,
                                Date = DateTime.Now,
                                Description = "Inital deposit"
                            }
                        }
                    },
                    new AtmUser
                    {
                        CardNumber = "5678-5678-5678-5678",
                        Pin = "5678",
                        Name = "Karlo",
                        LastName = "Čulek",
                        Id = 1,
                        Transactions = new List<AtmUserTransaction>
                        {
                            new AtmUserTransaction
                            {
                                AccountBalance = 2000,
                                Amount = 100,
                                Date = DateTime.Now,
                                Description = "Inital deposit"
                            }
                        }
                    }
                };
            }
        }
        public AtmUser GetUser(string cardNumber)
        {
            return users.First(u => u.CardNumber == cardNumber);
        }
        public void AddTransaction(int id, decimal amount, string comment)
        {
            var user = users.First(i => i.Id == id);
            var lastTransaction = user.Transactions.OrderByDescending(i => i.Date).First();
            var currentBalance = lastTransaction.AccountBalance - amount;
            if (currentBalance < 0)
            {
                throw new TransactionException("Not enough money left on the account try a lower amount.");
            }
            else if (user.Transactions.Count(i => i.Date > DateTime.Today) >= 10)
            {
                throw new TransactionException("You have reached the maximum number of transactions today.");
            }
            user.Transactions.Add(new AtmUserTransaction
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
            var usersFilePath = Path.Combine(internalPath, "users.json");
            var usersJson = JsonConvert.SerializeObject(users);
            File.WriteAllText(usersFilePath, usersJson);
        }
    }
}