using KiteBrigade.Service.Interface;
using System.Threading.Tasks;
using System.Threading;
using System;
using KiteBrigade.Resx;

namespace KiteBrigade.ViewModel
{
    public class SessionCreationViewModel : SessionViewModel
    {

        public SessionCreationViewModel(ILogService logService, ISessionService sessionService, ISession session, IPhotoService photoService) : base(logService, sessionService, session, photoService)
        {

        }

        public override async Task SaveSession()
        {
            await base.SaveSession();
        }

        protected override void RaiseCanExecuteChanged()
        {
            //
        }
    }
}