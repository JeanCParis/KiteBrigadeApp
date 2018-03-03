using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiteBrigade.Service.Interface
{
    public interface ILogService
    {
        void Error(Exception e);
    }
}
