using KiteBrigade.ErrorManagement.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.Utils
{
    public static class TaskUtils
    {
        public static async void FireAndForgetSafeAsync(this Func<Task> func, IErrorHandler handler = null, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var invoke = func?.Invoke();
                if (invoke != null)
                {
                    await invoke;
                }
            }
            catch (Exception ex)
            {
                handler?.HandleErrorAsync(ex, ct).FireAndForgetSafeAsync(null, ct);
            }
        }
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleErrorAsync(ex, ct).FireAndForgetSafeAsync(null, ct);
            }
        }
    }
}
