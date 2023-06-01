using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;

using WinRT;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIAppEditor;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    [ComImport, Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitializeWithWindow
    {
        void Initialize([In] IntPtr hwnd);
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto, PreserveSig = true, SetLastError = false)]
    internal static extern IntPtr GetActiveWindow();

    public async void OnSaveDotNet()
    {
        try
        {
            FileSavePicker picker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "New Document",
            };
            picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

            InitializeActiveWindow(picker);
            StorageFile file = await picker.PickSaveFileAsync();
            if (file is not null)
            {
                StorageStreamTransaction transaction = await file.OpenTransactedWriteAsync();
                Stream stream = transaction.Stream.AsStreamForWrite();
                using StreamWriter writer = new(transaction.Stream.AsStreamForWrite());
                byte[] preamble = Encoding.UTF8.GetPreamble();
                await stream.WriteAsync(preamble);
                await writer.WriteAsync(text1.Text);
                await writer.FlushAsync();
                transaction.Stream.Size = (ulong)stream.Length;
                await transaction.CommitAsync();
            }
        }
        catch (Exception ex)
        {
            MessageDialog dialog = new(ex.Message, "Error");
            _ = await dialog.ShowAsync();
        }
    }

    public async void OnOpenDotNet()
    {
        try
        {
            FileOpenPicker picker = new()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".md");

            InitializeActiveWindow(picker);
            StorageFile file = await picker.PickSingleFileAsync();
            if (file is not null)
            {
                IRandomAccessStreamWithContentType randomAccessStream = await file.OpenReadAsync();
                Stream stream = randomAccessStream.AsStreamForRead();
                using StreamReader reader = new(stream);
                text1.Text = await reader.ReadToEndAsync();
            }
        }
        catch (Exception ex)
        {
            MessageDialog dialog = new(ex.Message, "Error");
            _ = await dialog.ShowAsync();
        }
    }

    private static void InitializeActiveWindow(ICustomQueryInterface picker)
    {
        IInitializeWithWindow initializeWithWindowWrapper = picker.As<IInitializeWithWindow>();
        IntPtr hwnd = GetActiveWindow();
        initializeWithWindowWrapper.Initialize(hwnd);
    }

    public async void OnOpen()
    {
        try
        {
            FileOpenPicker picker = new()
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".md");

            InitializeActiveWindow(picker);

            StorageFile file = await picker.PickSingleFileAsync();

            if (file is not null)
            {
                IRandomAccessStreamWithContentType stream = await file.OpenReadAsync();
                using DataReader reader = new(stream);
                _ = await reader.LoadAsync((uint)stream.Size);
                text1.Text = reader.ReadString((uint)stream.Size);
            }
        }
        catch (Exception ex)
        {
            MessageDialog dialog = new(ex.Message, "Error");
            _ = await dialog.ShowAsync();
        }
    }

    public async void OnSave()
    {
        try
        {
            FileSavePicker picker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "New Document",
            };
            picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

            InitializeActiveWindow(picker);
            StorageFile file = await picker.PickSaveFileAsync();
            if (file is not null)
            {
                using StorageStreamTransaction transaction = await file.OpenTransactedWriteAsync();
                IRandomAccessStream stream = transaction.Stream;
                stream.Seek(0);
                using DataWriter writer = new(stream);
                _ = writer.WriteString(text1.Text);
                transaction.Stream.Size = await writer.StoreAsync();
                await transaction.CommitAsync();
            }
        }
        catch (Exception ex)
        {
            MessageDialog dialog = new(ex.Message, "Error");
            _ = await dialog.ShowAsync();
        }
    }
}
