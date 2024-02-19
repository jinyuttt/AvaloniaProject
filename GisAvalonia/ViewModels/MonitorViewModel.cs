using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using ReactiveUI;
using System.Collections.Generic;
using LiveChartsCore.SkiaSharpView.VisualElements;
using System.Collections.ObjectModel;
using GisAvalonia.Models;
using Avalonia.Media;
using Avalonia.Skia;
using LiveChartsCore.Drawing;
using System.Threading.Tasks;
using System;

namespace GisAvalonia.ViewModels
{
    internal class MonitorViewModel : ViewModelBase
    {
        public ObservableCollection<ColorModel> WHColors { get; set; } = new ObservableCollection<ColorModel>();

        private IPaint<LiveChartsCore.SkiaSharpView.Drawing.SkiaSharpDrawingContext> textPaint = new SolidColorPaint()
        {
            Color = SKColors.DarkSlateGray,
            SKTypeface = SKFontManager.Default.MatchCharacter('汉')
        };
        public IPaint<LiveChartsCore.SkiaSharpView.Drawing.SkiaSharpDrawingContext> TextPaint
        {
            get { return textPaint; }
            set
            {
                this.RaiseAndSetIfChanged(ref textPaint, value);
            }
        }
    

    
        public IEnumerable<ISeries> StateSeries { get; set; } = new ISeries[]
      {
             new PieSeries<int> { Values = new int[] { 48 }, Name = "正常设备" ,  Stroke=  new SolidColorPaint(SKColor.Parse("#CC3B7BE7"),1){ SKTypeface = SKFontManager.Default.MatchCharacter('汉')} },
             new PieSeries<int> { Values = new int[] { 43 }, Name = "离线设备" , Stroke=  new SolidColorPaint(SKColor.Parse("#AA05A3E4"),1){ SKTypeface = SKFontManager.Default.MatchCharacter('汉')} },
             new PieSeries<int> { Values = new int[] {7}, Name = "故障设备" , Stroke=  new SolidColorPaint(SKColor.Parse("#99E44D4E"),1) { SKTypeface = SKFontManager.Default.MatchCharacter('汉') } },
             new PieSeries<int> { Values = new int[] {2}, Name = "空闲设备" , Stroke=  new SolidColorPaint(Brushes.LightGray.Color.ToSKColor(),1){SKTypeface = SKFontManager.Default.MatchCharacter('汉') } },
            
      };

        private double value1 = 10;

        public double Value1
        {
            get => value1;
            set => this.RaiseAndSetIfChanged(ref value1, value);
        }
        public LabelVisual Title { get; set; } =
     new LabelVisual
     {
        Text = "综合",
         Padding = new LiveChartsCore.Drawing.Padding(15),
         Paint = new SolidColorPaint()
         {
             Color = SKColors.DarkSlateGray,
             SKTypeface = SKFontManager.Default.MatchCharacter('汉')
         },
     };
        public ISeries[] Series { get; set; }
       = new ISeries[]
       {
                new LineSeries<double>
                {
                    Values = new double[] { 3,5,34,53,23,53,534,5,3,5,634},
                  //  Fill=new SolidColorPaint(SKColor.Parse("#008000"),2),
                   // Stroke=new SolidColorPaint(SKColor.Parse("#008000"),2),
                }
               
       };
        public Axis[] XAxes { get; set; } = new Axis[]
        {
              new Axis
                {
                   Labels=new string[]{"1", "2","3","4","5",  "6", "7", "8", "9", "10", "11" },
                    SeparatorsPaint=new SolidColorPaint(){ StrokeThickness=0},
                    UnitWidth=1,
                    MinStep=1
        }
        };
        public Axis[] YAxes { get; set; } = new Axis[]
       {
              new Axis
                {
                   MinLimit=0, MaxLimit=700,
                   SeparatorsPaint=new SolidColorPaint(){ StrokeThickness=0, Color=SKColor.Parse("#1888")},
        }
       };

        public MonitorViewModel()
        {
            for (int i = 0; i < 5; i++)
            {
                WHColors.Add(new ColorModel { Color = Brushes.OrangeRed });
            }

            Task.Run(async () =>
            {
                //Mqtt.Mqtt.MqttServer();
                // var mqttClient= Mqtt.Mqtt.Client();
                //Mqtt.Mqtt.SubscribeAsync(mqttClient, "test", (t, p) =>
                //{
                //    double value = double.Parse(p.Trim());
                //    Value1 = value;
                //});
                //var client = Mqtt.Mqtt.Client();
                while (true)
                {
                    await Task.Delay(500);
                    // 从设备中获取的值 
                    var value = new Random().Next(10, 80);

                 //   Mqtt.Mqtt.PublishStringAsync(client,"test",value.ToString());
                    // 存到集合中   缓存       
                    // 显示到页面
                     Value1 = value;


                    // 数据存库   // 需要做缓存
                }
            });

           // this.Menu("MonitorPage");
        }
    }
}
