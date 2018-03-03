using System;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using KiteBrigade.Droid.UI.CustomView;
using KiteBrigade.Droid.Utils;
using KiteBrigade.ViewModel;
using static Android.Widget.SeekBar;

namespace KiteBrigade.Droid.UI.Activity
{
    public abstract class SessionActivity<T> : LoggedActivity<T>, IOnSeekBarChangeListener where T : SessionViewModel
    {
        protected SessionView _sessionView;

        protected override void SubscribeToViewEvents()
        {
            base.SubscribeToViewEvents();

            _sessionView.PhotoButton.Click += SetPhoto;
            _sessionView.PhotoView.Click += ViewPhoto;
            _sessionView.LocationButton.Click += SetLocation;

            _sessionView.WindSeekbar.SetOnSeekBarChangeListener(this);
            _sessionView.WaterSeekbar.SetOnSeekBarChangeListener(this);
            _sessionView.FunSeekbar.SetOnSeekBarChangeListener(this);
        }

        protected override void UnsubscribeFromViewEvents()
        {
            base.UnsubscribeFromViewEvents();

            _sessionView.PhotoButton.Click -= SetPhoto;
            _sessionView.PhotoView.Click -= ViewPhoto;
            _sessionView.LocationButton.Click -= SetLocation;

            _sessionView.WindSeekbar.SetOnSeekBarChangeListener(null);
            _sessionView.WaterSeekbar.SetOnSeekBarChangeListener(null);
            _sessionView.FunSeekbar.SetOnSeekBarChangeListener(null);
        }

        private void SetPhoto(object sender, EventArgs args)
        {
            ViewModel.SetPhoto();
        }

        private void ViewPhoto(object sender, EventArgs args)
        {
            StartActivity(new Intent(this, typeof(PictureActivity)));
        }

        private void SetLocation(object sender, EventArgs args)
        {
            StartActivity(new Intent(this, typeof(MapActivity)));
        }

        protected async void SaveSession(object sender, EventArgs args)
        {
            await ViewModel.SaveSession();
            Finish();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case RequestCode.SelectPicture:
                    if (resultCode == Result.Ok)
                    {
                        ViewModel.SetSessionPhotoUri(data.Data.ToString());
                    }
                    break;
            }
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            if (fromUser)
            {
                switch (seekBar.Id)
                {
                    case Resource.Id.seekbar_wind:
                        ViewModel.WindValue = progress;
                        break;
                    case Resource.Id.seekbar_water:
                        ViewModel.WaterValue = progress;
                        break;
                    case Resource.Id.seekbar_fun:
                        ViewModel.FunValue = progress;
                        break;
                }
            }
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
            //
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
            //
        }

        protected override void OnViewModelPropertyChanged(string propertyName)
        {
            base.OnViewModelPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(ViewModel.WindValue):
                    _sessionView.WindSeekbar.Progress = ViewModel.WindValue;
                    break;
                case nameof(ViewModel.WaterValue):
                    _sessionView.WaterSeekbar.Progress = ViewModel.WaterValue;
                    break;
                case nameof(ViewModel.FunValue):
                    _sessionView.FunSeekbar.Progress = ViewModel.FunValue;
                    break;
                case nameof(ViewModel.Score):
                    _sessionView.Score.Text = Math.Round(ViewModel.Score, 1).ToString();
                    break;
                case nameof(ViewModel.PhotoUri):
                    if (ViewModel.PhotoUri == null)
                    {
                        _sessionView.PhotoView.SetImageBitmap(null);
                        _sessionView.PhotoButton.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        Glide.With(this).Load(ViewModel.PhotoUri).Apply(new Com.Bumptech.Glide.Request.RequestOptions().Override(1000, 1000)).Into(_sessionView.PhotoView);
                        _sessionView.PhotoButton.Visibility = ViewStates.Gone;
                    }
                    break;
                case nameof(ViewModel.Location):
                    if (ViewModel.Location == null)
                    {
                        _sessionView.LocationButton.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        _sessionView.Location.Text = ViewModel.Location;
                        _sessionView.LocationButton.Visibility = ViewStates.Gone;
                    }
                    break;
                case nameof(ViewModel.Duration):
                    if (ViewModel.Duration == 0)
                    {
                        _sessionView.DurationButton.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        _sessionView.Duration.Text = ViewModel.Duration.ToString();
                        _sessionView.DurationButton.Visibility = ViewStates.Gone;
                    }
                    break;
            }
        }
    }
}