using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Microsoft.AppCenter.Analytics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppAnalytics;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Analytics.TrackEvent(EventNames.PageNavigation,
                             new Dictionary<string, string> { ["Page"] = nameof(MainWindow) });
    }

    private async void OnAnalyticsChanged(object sender, RoutedEventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            bool isChecked = checkBox?.IsChecked ?? true;
            await Analytics.SetEnabledAsync(isChecked);
        }
    }

    private void OnButton_Click(object sender, RoutedEventArgs e) 
        => Analytics.TrackEvent(EventNames.ButtonClicked,
                                new Dictionary<string, string>() { ["State"] = TextState.Text });
}
