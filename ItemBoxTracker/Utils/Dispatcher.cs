using System;

namespace MHWItemBoxTracker.Utils
{
    public class Dispatcher
    {
        public static void Dispatch(System.Action function) =>
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, function);
    }
}
