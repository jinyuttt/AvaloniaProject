using ReactiveUI;
using System.Windows.Input;

namespace NavAvalonia.ViewModels
{
    public class PageItemModel:ViewModelBase
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { this.RaiseAndSetIfChanged(ref _isSelected, value); }
        }
        public string? Header { get; set; }
        public object? PageView { get; set; }

        public ICommand? CloseTabCommand { get; set; }
    }
}