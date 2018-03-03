using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Views;
using Android.Widget;
using KiteBrigade.ViewModel;
using KiteBrigade.Resx;
using Com.Bumptech.Glide;

namespace KiteBrigade.Droid.UI.Activity
{
    [Activity(Label = "PictureActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PictureActivity : LoggedActivity<PictureViewModel>
    {
        private View _backButton;
        private ImageView _picture;

        protected override int LayoutId => Resource.Layout.activity_picture;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _backButton = toolbar.FindViewById(Resource.Id.button_back);
            _picture = toolbar.FindViewById<ImageView>(Resource.Id.image_picture);
        }

        protected override void SubscribeToViewEvents()
        {
            base.SubscribeToViewEvents();

            _backButton.Click += Back;
        }

        protected override void UnsubscribeFromViewEvents()
        {
            base.UnsubscribeFromViewEvents();

            _backButton.Click -= Back;
        }

        private void Back(object sender, EventArgs args)
        {
            OnBackPressed();
        }

        protected override void OnViewModelPropertyChanged(string propertyName)
        {
            base.OnViewModelPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(ViewModel.PictureUri):
                    Glide.With(this).Load(ViewModel.PictureUri).Apply(new Com.Bumptech.Glide.Request.RequestOptions().Override(1000, 1000)).Into(_picture);
                    break;
            }
        }
    }
}