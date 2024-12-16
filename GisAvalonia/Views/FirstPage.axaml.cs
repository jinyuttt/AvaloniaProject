using Avalonia.Controls;
using Xilium.CefGlue.Avalonia;

namespace GisAvalonia.Views
{
    public partial class FirstPage : UserControl
    {
        public FirstPage()
        {
            InitializeComponent();
           // WebView webview = new() { Focusable = true, Name= "webview"  };
           //  web.Children.Add(webview); 
           //  webview.BrowserCreated += (s, e) => webview.Navigate("https://www.bydauto.com.cn/pc/?ad_id=7");

            AvaloniaCefBrowser browser=new AvaloniaCefBrowser();
            browser.Address = "https://www.bydauto.com.cn/pc/?ad_id=7";
           
            web.Children.Add(browser);
            //    browser.TitleChanged += (s, e) => this. = e;
            this.Unloaded += FirstPage_Unloaded;
          //  webview.DocumentTitleChanged += (s, e) => Title = e.Title;

           
          //  Closing += (s, e) => Program.app?.Shutdown();
        }

        private void FirstPage_Unloaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
          
           if(web.Children.Count>0 )
            {
                foreach( var child in web.Children )
                {
                    //var view= child as WebView;
                    // if( view != null )
                    // {
                    //     view.Close();
                    //
                    // 
                    var view = child as AvaloniaCefBrowser;
                    if (view != null)
                    {
                        view.Dispose();
                    }
                }
            }
           
        }
    }
}
