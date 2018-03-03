using Android.Content;
using KiteBrigade.ViewModel;

namespace KiteBrigade.Droid.UI.Activity
{
    public abstract class LoggedActivity<T> : BaseActivity<T> where T : LoggedViewModel
    {
        protected override void OnStart()
        {
            base.OnStart();

            if (!ViewModel.IsLoggedIn)
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
                FinishAffinity();
            }
        }
    }
}