using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NavAvalonia.ViewModels
{
    public class MenuItemModel:ViewModelBase
    {
        public bool IsExpanded { get; set; }
        public string? IconCode { get; set; }
        public string? Header { get; set; } 
        public string? TargetView { get; set; }

        public ICommand? OpenViewCommand { get; set; }

        public ObservableCollection<MenuItemModel> Children { get; set; }

        public bool Empty { get { return Children.Count == 0; } }

        public MenuItemModel()
        {
            this.Header = "工艺设计";
            Children = new ObservableCollection<MenuItemModel>();
        }
    }
}