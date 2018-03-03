using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using KiteBrigade.Resx;
using KiteBrigade.ViewModel;
using System;

using Android.Views;
using KiteBrigade.Droid.UI.CustomView;
using TextView = Android.Widget.TextView;
using IOnSeekBarChangeListener = Android.Widget.SeekBar.IOnSeekBarChangeListener;
using SeekBar = Android.Widget.SeekBar;
using ImageView = Android.Widget.ImageView;
using Uri = Android.Net.Uri;
using Com.Bumptech.Glide;

namespace KiteBrigade.Droid.UI.Activity
{
    [Activity(Label = "SessionEditActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SessionEditActivity : SessionActivity<SessionEditViewModel>
    {
        private View _backButton, _confirmButton;

        protected override int LayoutId => Resource.Layout.activity_session_edit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _sessionView = FindViewById<SessionView>(Resource.Id.session);
            _backButton = toolbar.FindViewById(Resource.Id.button_back);
            _confirmButton = toolbar.FindViewById(Resource.Id.button_confirm);

            (_confirmButton as TextView).Text = AppResources.Ok;
            toolbar.FindViewById<TextView>(Resource.Id.title).Text = AppResources.EditSession;
        }

        protected override void SubscribeToViewEvents()
        {
            base.SubscribeToViewEvents();

            _backButton.Click += Back;
            _confirmButton.Click += SaveSession;
        }

        protected override void UnsubscribeFromViewEvents()
        {
            base.UnsubscribeFromViewEvents();

            _backButton.Click -= Back;
            _confirmButton.Click -= SaveSession;
        }

        private void Back(object sender, EventArgs args)
        {
            OnBackPressed();
        }
    }
}