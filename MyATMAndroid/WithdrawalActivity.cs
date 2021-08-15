using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "WithdrawalActivity")]
    public class WithdrawalActivity : Activity
    {
        private AtmUsers users;
        private Banks banks;
        private string cardNumber;
        private string bankId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            users = new AtmUsers();
            banks = new Banks();
            cardNumber = Intent.GetStringExtra("UserCard") ?? string.Empty;
            bankId = Intent.GetStringExtra("Banka") ?? string.Empty;
            SetContentView(Resource.Layout.withdrawal);

            var buttonWithdrawal10 = FindViewById<Button>(Resource.Id.button10);
            buttonWithdrawal10.Click += delegate
            {
                WithdrawAmount(10);
            };

            var buttonWithdrawal20 = FindViewById<Button>(Resource.Id.button20);
            buttonWithdrawal20.Click += delegate
            {
                WithdrawAmount(20);
            };

            var buttonWithdrawal30 = FindViewById<Button>(Resource.Id.button30);
            buttonWithdrawal30.Click += delegate
            {
                WithdrawAmount(30);
            };

            var buttonWithdrawal50 = FindViewById<Button>(Resource.Id.button50);
            buttonWithdrawal50.Click += delegate
            {
                WithdrawAmount(50);
            };

            var buttonWithdrawal100 = FindViewById<Button>(Resource.Id.button100);
            buttonWithdrawal100.Click += delegate
            {
                WithdrawAmount(100);
            };

            var buttonWithdrawal200 = FindViewById<Button>(Resource.Id.button200);
            buttonWithdrawal200.Click += delegate
            {
                WithdrawAmount(200);
            };

            var buttonWithdrawal300 = FindViewById<Button>(Resource.Id.button300);
            buttonWithdrawal300.Click += delegate
            {
                WithdrawAmount(300);
            };
        }

        private void WithdrawAmount(decimal amount)
        {
            try
            {
                var user = users.GetUser(cardNumber);
                var bank = banks.GetBank(bankId);
                users.AddTransaction(user.Id, amount, "withdrawal from mobile app");
                banks.AddTransaction(bank.Id, amount, "adding to the bank account");
                OpenDashboard();
            }
            catch (TransactionException e)
            {
                var activity = new Intent(this, typeof(ShowMessageActivity));
                activity.PutExtra("UserCard", cardNumber);
                activity.PutExtra("Message", e.Message);
                StartActivity(activity);
            }
        }

        private void OpenDashboard()
        {
            var activity = new Intent(this, typeof(DashboardActivity));
            activity.PutExtra("UserCard", cardNumber);
            activity.PutExtra("Banka", bankId);
            StartActivity(activity);
        }

        [Export("WithdrawaAmountOther")]
        public void WithdrawaAmountOther(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(WithdrawalOtherAmountActivity));
            activity.PutExtra("UserCard", cardNumber);
            activity.PutExtra("Banka", bankId);
            StartActivity(activity);
        }

        [Export("BackClick")]
        public void BackClick(Android.Views.View e)
        {
            OpenDashboard();
        }
    }
}