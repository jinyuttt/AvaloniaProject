using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.RemoteMonitor.Base
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Set<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (field != null && field.Equals(value)) return;
            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
