using KiteBrigade.Model;
using KiteBrigade.Service.Interface;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.ViewModel
{
    public class SessionEditViewModel : SessionViewModel
    {
        public SessionEditViewModel(ILogService logService, ISessionService sessionService, ISession session, IPhotoService photoService) : base(logService, sessionService, session, photoService)
        {

        }

        protected override void RaiseCanExecuteChanged()
        {
            //
        }
    }
}