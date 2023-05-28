using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

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

namespace WindowsAppTimer;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window, INotifyPropertyChanged
{
    private readonly DispatcherTimer _timer = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    private double _timerAngle;
    public double TimerAngle
    {
        get => _timerAngle;
        set
        {
            if (!EqualityComparer<double>.Default.Equals(_timerAngle, value))
            {
                _timerAngle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimerAngle)));
            }
        }
    }

    public MainWindow()
    {
        Title = "WinUI Dispatcher Timer App";
        InitializeComponent();
        _timer.Tick += OnTick;
        _timer.Interval = TimeSpan.FromSeconds(1);
    }

    private void OnTick(object? sender, object e) => TimerAngle = (TimerAngle + 6) % 360;

    private void OnStartTimer() => _timer.Start();

    private void OnStopTimer() => _timer.Stop();


}
