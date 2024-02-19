using Avalonia.Media;
using GisAvalonia.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using Zhaoxi.RemoteMonitor.Base;

namespace GisAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ColorModel> WHColors { get; set; } = new ObservableCollection<ColorModel>();



        private double value = 10;

        public double Value1
        {
            get => value;
            set => this.RaiseAndSetIfChanged(ref value, value);
        }


        private object _contentView;

        public object ContentView
        {
            get => _contentView;
            set => this.RaiseAndSetIfChanged(ref _contentView, value);
        }
       
        private bool isCheck=false;

        public bool IsCheck
        {
            get { return isCheck; }
            set { this.RaiseAndSetIfChanged(ref isCheck, value); }

        }

        public ICommand MenuCommand { get; set; }

        public MainWindowViewModel()
        {
            MenuCommand = new Command(Menu);


            for (int i = 0; i < 5; i++)
            {
                WHColors.Add(new ColorModel { Color = Brushes.OrangeRed });
            }
        }

        private void Menu(object obj)
        {
            // 菜单数据库维护
            // 每次创建的新？
            if (ContentView != null && ContentView.GetType().Name == obj.ToString()) return;

            Type type = Assembly.GetExecutingAssembly().GetType("GisAvalonia.Views." + obj.ToString())!;
            this.ContentView = Activator.CreateInstance(type)!;
        }
    }
}
