using KiteBrigade.Service.Interface;
using KiteBrigade.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiteBrigade.ViewModel
{
    public abstract class BaseViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private int _busyCounter;
        protected readonly ILogService _logService;

        public int BusyCounter => _busyCounter;
        public bool IsBusy => _busyCounter != 0;

        public BaseViewModel(ILogService logService)
        {
            _logService = logService;
        }

        public void Start()
        {
            if (IsBusy)
            {
                return;
            }

            StartAsync().FireAndForgetSafeAsync();
        }

        public async Task StartAsync()
        {
            if (IsBusy)
            {
                return;
            }

            SynchronizationContext context = SynchronizationContext.Current;
            try
            {
                IncrementBusyCounter();
                using (CancellationTokenSource cts = new CancellationTokenSource(10000))
                {
                    await StartAsync(cts.Token);
                }
            }
            catch (Exception ex)
            {
                _logService.Error(new Exception("Unable to start async under the fixed timeout"));
            }
            finally
            {
                DecrementBusyCounter();
            }
        }
        protected abstract Task StartAsync(CancellationToken ct);

        protected void IncrementBusyCounter()
        {
            Interlocked.Increment(ref _busyCounter);
            RaisePropertyChanged(nameof(BusyCounter));
            RaisePropertyChanged(nameof(IsBusy));
            RaiseCanExecuteChanged();
        }
        protected void DecrementBusyCounter()
        {
            Interlocked.Decrement(ref _busyCounter);
            RaisePropertyChanged(nameof(BusyCounter));
            RaisePropertyChanged(nameof(IsBusy));
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Use it as second parameter to create commands.
        /// </summary>
        /// <returns>!IsBusy by default</returns>
        public virtual bool DefaultCanExecute()
        {
            return !IsBusy;
        }

        /// <summary>
        /// Call RaiseCanExecuteChanged on the ViewModel commands
        /// </summary>
        protected abstract void RaiseCanExecuteChanged();
    }
}