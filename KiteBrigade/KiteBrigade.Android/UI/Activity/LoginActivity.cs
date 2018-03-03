using Android.App;
using Android.Content;
using Android.Content.PM;
using KiteBrigade.ViewModel;

namespace KiteBrigade.Droid.UI.Activity
{
    [Activity(Label = "KiteBrigade", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : BaseActivity<LoginViewModel>
    {
        protected override int LayoutId => Resource.Layout.activity_login;

        protected override void OnResume()
        {
            base.OnResume();

            if (ViewModel.IsLogedIn)
            {
                LaunchMainActivity();
            }
        }

        protected override void OnViewModelPropertyChanged(string propertyName)
        {
            base.OnViewModelPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(ViewModel.IsLogedIn):
                    if (ViewModel.IsLogedIn)
                    {
                        LaunchMainActivity();
                    }
                    break;
            }
        }

        private void LaunchMainActivity()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
            Finish();
        }
    }
}