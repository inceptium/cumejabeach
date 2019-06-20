using System;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using CumejaBeach.utility.controller;
using Xamarin.Forms;

namespace CumejaBeach.iOS
{
    public class iOSLongRunningTaskExample
    {
        nint _taskId;
        CancellationTokenSource _cts;

        public async Task Start()
        {
            _cts = new CancellationTokenSource();

            _taskId = UIApplication.SharedApplication.BeginBackgroundTask("CumejaRing", OnExpiration);

            try
            {
                //INVOKE THE SHARED CODE
                var counter = new ControllerRing(App.getInstance().inClient);
                await counter.LeggiRingAsync(_cts.Token);

            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    //var message = new CancelledMessage();
                    //Device.BeginInvokeOnMainThread(
                    //    () => MessagingCenter.Send(message, "CancelledMessage")
                    //);
                }
            }

            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        void OnExpiration()
        {
            _cts.Cancel();
        }
    }
}
