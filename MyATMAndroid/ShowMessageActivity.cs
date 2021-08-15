
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "ShowMessageActivity")]
    public class ShowMessageActivity : Activity
    {
        private string userCard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.show_message);

            userCard = Intent.GetStringExtra("UserCard") ?? string.Empty;
            var message = Intent.GetStringExtra("Message") ?? string.Empty;
            var mesageTextLabel = FindViewById<TextView>(Resource.Id.showMessageLabel);
            mesageTextLabel.Text = message;
        }

        [Export("BackClick")]
        public void BackClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(WithdrawalActivity));
            activity.PutExtra("UserCard", userCard);
            StartActivity(activity);
        }
    }
}