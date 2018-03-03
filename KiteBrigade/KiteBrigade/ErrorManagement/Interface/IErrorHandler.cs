using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.ErrorManagement.Interface
{
    public interface IErrorHandler
    {
        Task HandleErrorAsync(Exception e, CancellationToken ct);
    }
}
