using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zhaoxi.RemoteMonitor.Base
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            DoExecute?.Invoke(parameter);
        }
        private Action<object> DoExecute { get; set; }

        public Command(Action<object> doExecute)
        {
            DoExecute = doExecute;
        }
    }
}
