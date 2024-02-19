using Avalonia.Controls;
using Avalonia.Input;

namespace ProAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {

        }

        private void Border_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if (e.Pointer.Type == PointerType.Mouse)
            {
                this.BeginMoveDrag(e);
            }
        }
    }
}