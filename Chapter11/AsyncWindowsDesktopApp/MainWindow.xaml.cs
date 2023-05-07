using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

using WinRT;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AsyncWindowsDesktopApp;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private async void OnStartAsync(object sender, RoutedEventArgs e)
    {
        text1.Text = $"UI thread: {GetThread()}";
        await Task.Delay(1000);
        text1.Text += $"\nafter awaitL {GetThread()}";
    }

    private static object GetThread() => $"thread: {Environment.CurrentManagedThreadId}";

    private async void OnStartAsyncConfigureAwait(object sender, RoutedEventArgs e)
    {
        text1.Text = $"UI thread: {GetThread()}";
        string s = await AsyncFunction().ConfigureAwait(continueOnCapturedContext: true);
        text1.Text += $"\n{s}\nafter await: {GetThread()}\n";

        async Task<string> AsyncFunction()
        {
            string result = $"async function: {GetThread()}";
            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
            result += $"\nasync function after await: {GetThread()}";

            try
            {
                text1.Text = "this is a call from the wrong thread";
                return "not reached";
            }
            catch (Exception ex) when (ex.HResult == -2147417842)
            {
                result += $"; exception: {ex.Message}";
                return result;
            }
        }
    }


    private async void OnStartAsyncWithThreadSwitch(object sender, RoutedEventArgs e)
    {
        text1.Text = $"UI thread: {GetThread()}";
        string s = await AsyncFunction();
        text1.Text += $"{s}\nafter await: {GetThread()}";

        async Task<string> AsyncFunction()
        {
            string result = $"\nasync function: {GetThread()}";
            await Task.Delay(1000).ConfigureAwait (continueOnCapturedContext: false);
            result += $"\nasync function after await: {GetThread()}";


            return text1.DispatcherQueue.TryEnqueue(()
                => text1.Text += $"\nasync function switch back to UI thread: {GetThread()}")
                   ? result
                   : throw new InvalidOperationException();
        }
    }

    private async void OnIAsyncOperation(object sender, RoutedEventArgs e)
    {
        MessageDialog dlg = new("Select One, Two, Or Three", "Sample");

        try
        {
            IntPtr windowHandle = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(dlg, windowHandle);

            dlg.Commands.Add(new UICommand("One", null, 1));
            dlg.Commands.Add(new UICommand("Two", null, 2));
            dlg.Commands.Add(new UICommand("Three", null, 3));

            IUICommand command = await dlg.ShowAsync();

            text1.Text = $"Command {command.Id} with the label {command.Label} invoked";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private void OnStartDeadlock(object sender, RoutedEventArgs e) => DelayAsync().Wait();

    private static async Task DelayAsync() => await Task.Delay(1000);
}
