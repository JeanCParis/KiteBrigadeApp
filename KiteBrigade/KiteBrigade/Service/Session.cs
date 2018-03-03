using System.Collections.Generic;
using KiteBrigade.Model;
using KiteBrigade.Service.Interface;

namespace KiteBrigade.Service
{
    public class Session : ISession
    {
        public User User { get; set; }
        public IList<KiteSession> Sessions { get; set; }
        public KiteSession CurrentSession { get; set; }
        public int CurrentSessionIndex { get; set; }
    }
}