using KiteBrigade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiteBrigade.Service.Interface
{
    public interface ISession
    {
        User User { get; set; }
        IList<KiteSession> Sessions { get; set; }
        KiteSession CurrentSession { get; set; }
        int CurrentSessionIndex { get; set; }
    }
}
