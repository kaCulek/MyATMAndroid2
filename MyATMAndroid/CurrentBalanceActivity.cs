
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "CurrentBalanceActivity")]
    public class CurrentBalanceActivity : Activity
    {
        private string cardNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.current_balance);

            cardNumber = Intent.GetStringExtra("UserCard") ?? string.Empty;
            var users = new AtmUsers();
            var user = users.GetUser(cardNumber);

            var balanceLabel = FindViewById<TextView>(Resource.Id.currentBalanceLabel);
            var lastTransaction = user.Transactions.OrderByDescending(i => i.Date).First();
            balanceLabel.Text = $"Current balance: {lastTransaction.AccountBalance}";
        }

        [Export("BackClick")]
        public void BackClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(DashboardActivity));
            activity.PutExtra("UserCard", cardNumber);
            StartActivity(activity);
        }
    }
}