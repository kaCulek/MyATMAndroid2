
using Android.App;
using Android.Content;
using Android.OS;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : Activity
    {
        private string userCard;
        private string bankId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.dashboard);
            userCard = Intent.GetStringExtra("UserCard") ?? string.Empty;
            bankId = Intent.GetStringExtra("Banka") ?? string.Empty;
        }

        [Export("WithdrawalClick")]
        public void WithdrawalClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(WithdrawalActivity));
            activity.PutExtra("UserCard", userCard);
            activity.PutExtra("Banka", bankId);
            StartActivity(activity);
        }

        [Export("ShowCurrentBalance")]
        public void ShowCurrentBalance(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(CurrentBalanceActivity));
            activity.PutExtra("UserCard", userCard);
            activity.PutExtra("Banka", bankId);
            StartActivity(activity);
        }

        [Export("LastTransactionsClick")]
        public void LastTransactionsClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(LastTransactionsActivity));
            activity.PutExtra("UserCard", userCard);
            activity.PutExtra("Banka", bankId);
            StartActivity(activity);
        }

        [Export("LogoutClick")]
        public void LogoutClick(Android.Views.View e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}