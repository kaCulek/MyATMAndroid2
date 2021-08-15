using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyATMAndroid
{
    public class Bank
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
        public List<AtmUserTransaction> BankTransactions { get; set; }
    }
}