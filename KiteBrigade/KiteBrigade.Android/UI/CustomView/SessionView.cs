using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Constraints;
using Android.Util;
using KiteBrigade.Resx;

namespace KiteBrigade.Droid.UI.CustomView
{
    [Register("com.kitebrigade.SessionView")]
    public class SessionView : ConstraintLayout
    {
        public TextView Score { get; set; }

        public SeekBar WindSeekbar { get; private set; }
        public SeekBar WaterSeekbar { get; private set; }
        public SeekBar FunSeekbar { get; private set; }

        public TextView LocationButton { get; private set; }
        public TextView Location { get; private set; }
        public TextView DurationButton { get; private set; }
        public TextView Duration { get; private set; }

        public ImageView PhotoView { get; private set; }
        public TextView PhotoButton { get; private set; }

        public SessionView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            LayoutInflater.From(context).Inflate(Resource.Layout.view_session, this, true);

            Score = FindViewById<TextView>(Resource.Id.score);
            WindSeekbar = FindViewById<SeekBar>(Resource.Id.seekbar_wind);
            WaterSeekbar = FindViewById<SeekBar>(Resource.Id.seekbar_water);
            FunSeekbar = FindViewById<SeekBar>(Resource.Id.seekbar_fun);
            LocationButton = FindViewById<TextView>(Resource.Id.button_location);
            Location = FindViewById<TextView>(Resource.Id.location);
            DurationButton = FindViewById<TextView>(Resource.Id.button_duration);
            Duration = FindViewById<TextView>(Resource.Id.duration);
            PhotoView = FindViewById<ImageView>(Resource.Id.image_photo);
            PhotoButton = FindViewById<TextView>(Resource.Id.button_photo);

            FindViewById<TextView>(Resource.Id.score_title).Text = AppResources.SessionScore;
            FindViewById<TextView>(Resource.Id.seekbar_wind_title).Text = AppResources.SessionWind;
            FindViewById<TextView>(Resource.Id.seekbar_water_title).Text = AppResources.SessionWater;
            FindViewById<TextView>(Resource.Id.seekbar_fun_title).Text = AppResources.SessionFun;

            PhotoButton.Text = AppResources.PhotoButtonText;
            LocationButton.Text = AppResources.Location;
            DurationButton.Text = AppResources.Duration;
        }
    }
}