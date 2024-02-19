using Avalonia.Controls;
using NavAvalonia.ViewModels;
using ReactiveUI;

namespace NavAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            
            if(e.Pointer.Type == Avalonia.Input.PointerType.Mouse)
            {
                var point = e.GetCurrentPoint(sender as Control);
                var x = point.Position.X;
                var y = point.Position.Y;
               
                if (point.Properties.IsLeftButtonPressed)
                {
                    Grid grid=sender as Grid;
                    if (grid!=null)
                    {
                       var menu= grid.DataContext as MenuItemModel;
                        if(menu!=null&&menu.OpenViewCommand!=null)
                        {
                            menu.OpenViewCommand.Execute(menu);
                        }
                       
                    }
                }
            }
           
           
            
        }
    }
}