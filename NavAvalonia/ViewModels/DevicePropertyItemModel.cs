using ReactiveUI;

namespace NavAvalonia.ViewModels
{
    public class DevicePropertyItemModel:ViewModelBase
    {
        public string PropName { get; set; }
       

        private string _propValue;
        public string PropValue
        {
            get { return _propValue; }
            set { this.RaiseAndSetIfChanged(ref _propValue, value); }
        }
    }
}