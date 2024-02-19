using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NavAvalonia.ViewModels;

namespace NavAvalonia.Views.Pages;

public partial class DevicePage : UserControl
{
    public DevicePage()
    {
        InitializeComponent();
        this.DataContext = new DeviceViewModel();
    }
}