using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.ReactiveUI;
using System;

namespace NavAvalonia
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }
        public static void ChangeLanguage(string language)
        {
            var file = $"avares://TestLanguages/Assets/Languages/{language}.axaml";
            var data = new ResourceInclude(new Uri(file, UriKind.Absolute));
            data.Source = new Uri(file, UriKind.Absolute);
            Avalonia.Application.Current!.Resources.MergedDictionaries[0] = data;
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI();
    }
}
