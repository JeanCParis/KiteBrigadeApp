using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using KiteBrigade.ViewModel;
using KiteBrigade.Service.Interface;
using KiteBrigade.Android.Service;
using GalaSoft.MvvmLight.Ioc;
using KiteBrigade.Droid.Service;

namespace KiteBrigade.Droid
{
	//You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            ViewModelLocator.Init();

            SimpleIoc.Default.Register<IPhotoService, PhotoService>();
            SimpleIoc.Default.Register<IGeocodingService, GeocodingService>();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}