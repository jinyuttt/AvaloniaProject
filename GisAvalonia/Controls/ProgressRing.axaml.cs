using Avalonia;
using Avalonia.Controls;
using System.Collections.Generic;
using System;
using Avalonia.Media;
using System.ComponentModel;
using ReactiveUI;
using Mapsui.UI.Avalonia;

namespace GisAvalonia.Controls
{
  
    public partial class ProgressRing : UserControl
    {
        private IBrush _forecolor = Brushes.Orange;

        public IBrush ForeColor
        {
            get { return GetValue(ForeColorProperty); }
            set
            {
                // _forecolor = value;
                //SetAndRaise()
                SetValue(ForeColorProperty, value);
                this.Refresh();
                
            }
        }

        public static readonly StyledProperty<IBrush> ForeColorProperty =
    AvaloniaProperty.Register<ProgressRing, IBrush>(nameof(ForeColor), Brushes.Orange);
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly StyledProperty<double> ValueProperty =
            AvaloniaProperty.Register<ProgressRing, double>(nameof(Value), 0,false,Avalonia.Data.BindingMode.TwoWay,null,
                OnValueChanged);

        private static double OnValueChanged(AvaloniaObject @object, double arg2)
        {
            var obj = @object as ProgressRing;
            obj.Refresh();
            return arg2;
        }

     

       


        public void Refresh()
        {
            var count = Math.Ceiling(Value / 100 * 360 / 8);
            for (int i = 0; i < MarkList.Count; i++)
            {
                if (i <= count)
                    this.MarkList[i].Color = this.ForeColor;
                else
                    this.MarkList[i].Color = Brushes.LightGray;
            }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<ProgressRing, string>(nameof(Title),string.Empty);

       
        public List<MarkInfo> MarkList
        {
            get { return GetValue(MarkProperty); }
            set { SetValue(MarkProperty, value); }
        }
        public static readonly StyledProperty<List<MarkInfo>> MarkProperty =
           AvaloniaProperty.Register<ProgressRing, List<MarkInfo>>(nameof(MarkList), null);
        public ProgressRing()
        {
            MarkList = new List<MarkInfo>();
            Value = 80;
            Title = "×ÛºÏÉè±¸";
            InitializeComponent();
          
           
            int count = 360 / 8;
            for (int i = 0; i < count; i++)
            {
                MarkList.Add(new MarkInfo() { Angle = i * 8 });
            }
           
        }

      
    }

    public class MarkInfo : INotifyPropertyChanged
    {
        public double Angle { get; set; }
        private IBrush _color = Brushes.Red;
        public IBrush Color
        {
            get => _color; set
            {
                _color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
