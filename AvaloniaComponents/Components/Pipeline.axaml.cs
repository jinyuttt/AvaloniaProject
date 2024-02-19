using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaComponents.Components
{
    public partial class Pipeline : UserControl
    {
        /// <summary>
        /// 液体流向，接受1和2两个值
        /// </summary>
        public int Direction
        {
            get { return (int)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        public static readonly StyledProperty<int> DirectionProperty =
            AvaloniaProperty.Register<Pipeline,int>(nameof(Direction), 0,false,Avalonia.Data.BindingMode.OneWay,null, OnDirectionChanged);

        private static int OnDirectionChanged(AvaloniaObject @object, int arg2)
        {
            //  int value = int.Parse(e.NewValue.ToString());
            //  VisualStateManager.GoToState(d as Pipeline, value == 1 ? "WEFlowState" : "EWFlowState", false);
            return arg2;

        }

      

        public IBrush LiquidColor
        {
            get { return (Brush)GetValue(LiguidColorProperty); }
            set { SetValue(LiguidColorProperty, value); }
        }
        public static readonly StyledProperty<IBrush> LiguidColorProperty =
            AvaloniaProperty.Register<Pipeline,IBrush>(nameof(LiquidColor), Brushes.Orange);

        public int CapRadius
        {
            get { return (int)GetValue(CapRadiusProperty); }
            set { SetValue(CapRadiusProperty, value); }
        }
        public static readonly StyledProperty<int> CapRadiusProperty =
            AvaloniaProperty.Register<Pipeline,int>(nameof(CapRadius),0);

        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(CapRadiusProperty, value); }
        }
        public static readonly StyledProperty<Point> EndPointProperty =
           AvaloniaProperty.Register<Pipeline, Point>(nameof(CapRadius),new Point(100,0));
        public Pipeline()
        {
            InitializeComponent();
            EndPoint = new Point(this.Width-10,0);
         
        }
    }
}
