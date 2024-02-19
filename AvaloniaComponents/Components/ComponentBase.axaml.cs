using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System.Windows.Input;

namespace AvaloniaComponents.Components
{
    public partial class ComponentBase : UserControl
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                if (value)
                {
                    var parent = this.Parent as Grid;
                    if (parent != null)
                    {
                        foreach (var item in parent.Children)
                        {
                            if (item is ComponentBase)
                                (item as ComponentBase).IsSelected = false;
                        }
                    }
                }

              //  VisualStateManager.GoToState(this, value ? "SelectedState" : "UnselectedState", false);
            }
        }

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }
        public static readonly StyledProperty<bool> IsRunningProperty =
            AvaloniaProperty.Register<ComponentBase,bool>(nameof(IsRunning), false,false, BindingMode.OneWay, null, OnRunningStateChanged);

        private static bool OnRunningStateChanged(AvaloniaObject @object, bool arg2)
        {
            //  bool state = (bool)e.NewValue;
            return arg2;
        }

     

        public bool IsFault
        {
            get { return (bool)GetValue(IsFaultProperty); }
            set { SetValue(IsFaultProperty, value); }
        }
        public static readonly StyledProperty<bool> IsFaultProperty =
            AvaloniaProperty.Register<ComponentBase, bool>(nameof(IsFault), false, false, BindingMode.OneWay,null, OnFaultStateChanged);

        private static bool OnFaultStateChanged(AvaloniaObject @object, bool arg2)
        {
            // VisualStateManager.GoToState(d as ComponentBase, (bool)e.NewValue ? "FaultState" : "NormalState", false);
            //throw new NotImplementedException();
            return arg2;
        }

       

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly StyledProperty<ICommand> CommandProperty =
            AvaloniaProperty.Register<ComponentBase, ICommand>(nameof(Command),null,false, BindingMode.OneWay,null
                );

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly StyledProperty<object> CommandParameterProperty =
             AvaloniaProperty.Register<ComponentBase, object>(nameof(CommandParameter));


       

       
        public ComponentBase()
        {
            InitializeComponent();
            this.PointerPressed += ComponentBase_PointerPressed;
        }

        private void ComponentBase_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            var point = e.GetCurrentPoint(sender as Control);
            if(!point.Properties.IsLeftButtonPressed)
            {
                return;
            }
           
            this.IsSelected = !this.IsSelected;

            this.Command?.Execute(this.CommandParameter);

            e.Handled = true;
        }
    }
}
