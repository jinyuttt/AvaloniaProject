using Avalonia;
using Avalonia.Controls;

namespace AvaloniaComponents.Components
{
    public partial class WarningLight : UserControl
    {
        // µ∆π‚◊¥Ã¨µƒ“¿¿µ Ù–‘
        public LightState LightState
        {
            get { return (LightState)GetValue(LightStateProperty); }
            set { SetValue(LightStateProperty, value); }
        }
        public static readonly StyledProperty<LightState> LightStateProperty =
            AvaloniaProperty.Register<WarningLight, LightState>(nameof(LightState),LightState.None,false,Avalonia.Data.BindingMode.OneWay,null, OnLightStateChanged);

        private static LightState OnLightStateChanged(AvaloniaObject @object, LightState state)
        {
            return state;
        }

      
        public WarningLight()
        {
            InitializeComponent();
        }
    }
    public enum LightState
    {
        None, Fault, Warning, Run
    }
}
