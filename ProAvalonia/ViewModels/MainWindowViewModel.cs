using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using ProAvalonia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.VisualElements;


namespace ProAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
      

       public IEnumerable<ISeries> StateSeries { get; set; } = new ISeries[]
       {
             new PieSeries<int> { Values = new int[] { 2 }, Name = "1" ,InnerRadius=20, Fill=  new SolidColorPaint(SKColor.Parse("#008000"))},
             new PieSeries<int> { Values = new int[] { 4 }, Name = "2" ,InnerRadius=20, Fill=  new SolidColorPaint(SKColor.Parse("#D2C86B")) },
             new PieSeries<int> { Values = new int[] { 1 }, Name = "3" ,InnerRadius=20, Fill=  new SolidColorPaint(SKColor.Parse("#03AEDE")) },
             new PieSeries<int> { Values = new int[] { 4 }, Name = "4" ,InnerRadius=20, Fill=  new SolidColorPaint(SKColor.Parse("#C13530")) },
             new PieSeries<int> { Values = new int[] { 3 }, Name = "5" ,InnerRadius=20, Fill=  new SolidColorPaint(SKColor.Parse("#696969")) }
       };

        public LabelVisual Title { get; set; } =
       new LabelVisual
       {
           //Text = "My chart title",
          // TextSize = 25,
           Padding = new LiveChartsCore.Drawing.Padding(15),
           Paint = new SolidColorPaint() { Color= SKColors.DarkSlateGray,
               SKTypeface = SKFontManager.Default.MatchCharacter('汉')
           },
       };
       
        public ISeries[] Series { get; set; }
          = new ISeries[]
          {
                new LineSeries<double>
                {
                    Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                },
                 new LineSeries<double>
                {
                    Values = new double[] { 1, 1, 4, 6, 7, 8, 3 },
                    Fill = null
                }
          };
        public ISeries[] BarSeries { get; set; } =
  {
             new ColumnSeries<double>
             {
                 Name = "Mary",

                 Values = new double[] { 2, 5, 4 }
             },
             new ColumnSeries<double>
             {
                 Name = "Ana",
                 Values = new double[] { 3, 1, 6 }
             }
         };

        public List<CompareItemModel> WorkerCompareList { get; set; }

        public List<CompareItemModel> QualityList { get; set; }

        public ObservableCollection<string> Alarms { get; set; }

        public int FinishRate { get; set; } = 80;
       
        public ObservableCollection<string> CurrentYeild { get; set; }

        public List<BadItemModel> BadScatter { get; set; }
        Random random = new Random();
        CancellationTokenSource cts = new CancellationTokenSource();
        Task task = null;

        public MainWindowViewModel()
        {
            //Init();
            InitBad();
            InitEmpolys();
            Initquality();
            InitWarn();

        }
        //private void Init()
        //{
        //    #region 环状图数据初始化
        //    StateSeries=new ObservableCollection<PieSeries<double>>();
        //    StateSeries.Add(new PieSeries<double>()
        //    {
        //       // Title = "普货",
        //       // Values = new ChartValues<double>(new double[] { 0.533 }),
        //        Values = new double[] {   0.533 },
        //      //  Fill = new SolidColorBrush(Color.FromArgb(255, 43, 182, 254))
        //    });
        //    StateSeries.Add(new PieSeries<double>()
        //    {
        //        //  Title = "普货",

        //        Values = new double[] { 0.2 }

        //    }); ;
        //    StateSeries.Add(new PieSeries<double>()
        //    {
        //      //  Title = "普货",
        //       // Values = new ChartValues<double>(new double[] { 0.167 }),
        //      //  Fill = new SolidColorBrush(Color.FromArgb(255, 144, 150, 191))
        //         Values = new double[] {  0.167  },
        //    });
        //    StateSeries.Add(new PieSeries<double>()
        //    {
        //        //   Title = "普货",
        //        //  Values = new ChartValues<double>(new double[] { 0.1 }),
        //        //  Fill = Brushes.DimGray
        //        Values = new double[] {  0.1 },
        //    });
        //    StateSeries.AsPieSeries();
        //    #endregion
        //}
   
        private void InitEmpolys()
        {
            #region 人员绩效初始化
            string[] Empolys = new string[] { "赵XX", "钱XX", "孙XX", "李XX", "周XX" };
            WorkerCompareList = new List<CompareItemModel>();
            foreach (var e in Empolys)
            {
                WorkerCompareList.Add(new CompareItemModel()
                {
                    Name = e,
                    PlanValue = random.Next(100, 200),
                    FinishedValue = random.Next(10, 150),
                });
            }

            #endregion
        }

        private void InitWarn()
        {
            #region 报警数据初始化
            Alarms = new ObservableCollection<string>();
            Alarms.Add("【H338->厂务冷却水入水温度[℃]】 34->10:00");
            Alarms.Add("【H338->厂务冷却水入水温度[℃]】 34->10:00");
            Alarms.Add("【H338->厂务冷却水入水温度[℃]】 34->10:00");
            Alarms.Add("【H338->厂务冷却水入水温度[℃]】 34->10:00");
            #endregion

        }

        private void InitBad()
        {
            #region 不良分布初始化
            BadScatter = new List<BadItemModel>();
            string[] BadNames = new string[] { "缺角A", "缺角B", "缺角C", "缺角D", "缺角E", "缺角E", "缺角F", "缺角G" };
            for (int i = 0; i < BadNames.Length; i++)
            {
                BadScatter.Add(new BadItemModel() { Title = BadNames[i], Size = 180 - 20 * i, Value = 0.9 - 0.1 * i });
            }
            #endregion
        }

        private void Initquality()
        {
            #region 质量控制
            string[] quality = new string[] { "机床-1", "机床-2", "机床-3", "机床-4",
                "机床-5", "机床-6", "机床-7", "机床-8", "机床-9", "机床-10" };
            QualityList = new List<CompareItemModel>();
            foreach (var q in quality)
            {
                QualityList.Add(new CompareItemModel()
                {
                    Name = q,
                    PlanValue = random.Next(100, 200),
                    FinishedValue = random.Next(10, 150),
                });
            }
            #endregion

            task = Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                    // 持续获取现场数据
                   // ushort[] values =
                     //  CurrentYeild = values[0].ToString("000000");
                     CurrentYeild = new ObservableCollection<string>(new string[] { "3", "5", "6" });
                    // 添加报警
                     Alarms.Insert(0, "【H338->厂务冷却水入水温度[℃]】 34->20:00");
                        if (Alarms.Count > 6)
                            Alarms.RemoveAt(Alarms.Count - 1);// 滚动   停留
                 
                }
            }, cts.Token);
        }

    }
}
