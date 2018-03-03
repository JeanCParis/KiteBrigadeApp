using System;

using Android.OS;
using Microsoft.Practices.ServiceLocation;
using Android.Support.V7.App;
using KiteBrigade.ViewModel;

namespace KiteBrigade.Droid.UI.Activity
{
    public abstract class BaseActivity<T> : AppCompatActivity where T : BaseViewModel
    {
        public T ViewModel { get; protected set; }

        protected abstract int LayoutId { get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            ViewModel = ServiceLocator.Current.GetInstance<T>();
            base.OnCreate(savedInstanceState);
            SetContentView(LayoutId);
        }

        protected async override void OnStart()
        {
            base.OnStart();

            SubscribeToViewEvents();
            SubscribeToViewModelEvents();
            await ViewModel?.StartAsync();
        }

        protected override void OnStop()
        {
            base.OnStop();
            UnsubscribeFromViewEvents();
            UnsubscribeFromViewModelEvents();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ViewModel.Cleanup();
        }

        protected virtual void SubscribeToViewEvents()
        {
            UnsubscribeFromViewEvents();
        }

        protected virtual void UnsubscribeFromViewEvents()
        {
        }

        protected void GoBack(object sender, EventArgs args)
        {
            OnBackPressed();
        }

        protected virtual void OpenChat(object sender, EventArgs args)
        {

        }

        protected virtual void SubscribeToViewModelEvents()
        {
            UnsubscribeFromViewModelEvents();
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        protected virtual void UnsubscribeFromViewModelEvents()
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnViewModelPropertyChanged(e.PropertyName);
        }

        protected virtual void OnViewModelPropertyChanged(string propertyName)
        {
        }
    }
}