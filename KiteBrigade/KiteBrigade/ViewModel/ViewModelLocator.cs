using GalaSoft.MvvmLight.Ioc;
using KiteBrigade.Service;
using KiteBrigade.Service.Interface;
using Microsoft.Practices.ServiceLocation;

namespace KiteBrigade.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public static void Init()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ILogService, LogService>();
            SimpleIoc.Default.Register<ISession, Session>();
            SimpleIoc.Default.Register<ISessionService, SessionService>();

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SessionCreationViewModel>();
            SimpleIoc.Default.Register<SessionEditViewModel>();
            SimpleIoc.Default.Register<MapViewModel>();
            SimpleIoc.Default.Register<PictureViewModel>();
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}