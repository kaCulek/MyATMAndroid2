
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "WithdrawalOtherAmountActivity")]
    public class WithdrawalOtherAmountActivity : Activity
    {
        public AtmUsers users { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            users = new AtmUsers();
            SetContentView(Resource.Layout.withdrawal_other);
        }

        [Export("WithdrawClick")]
        public void WithdrawClick(Android.Views.View e)
        {
            var userCard = Intent.GetStringExtra("UserCard") ?? string.Empty;
            try
            {
                var amountInput = (EditText)FindViewById(Resource.Id.withdrawalAmount);
                var amount = Decimal.Parse(amountInput.Text.ToString());
                var user = users.GetUser(userCard);
                users.AddTransaction(user.Id, amount, "withdrawal other amount from mobile app");
                var activity = new Intent(this, typeof(DashboardActivity));
                activity.PutExtra("UserCard", userCard);
                StartActivity(activity);
            }
            catch (TransactionException ex)
            {
                var activity = new Intent(this, typeof(ShowMessageActivity));
                activity.PutExtra("UserCard", userCard);
                activity.PutExtra("Message", ex.Message);
                StartActivity(activity);
            }
        }
    }
}