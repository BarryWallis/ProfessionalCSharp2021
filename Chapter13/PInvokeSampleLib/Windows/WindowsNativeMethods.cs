using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace PInvokeSample;

[SupportedOSPlatform("windows")]
internal static class WindowsNativeMethods
{
    [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "CreateHardLink", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
    private static extern bool CreateHardLink(
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
        [In, MarshalAs(UnmanagedType.LPWStr)] string newFileName,
        [In, MarshalAs(UnmanagedType.LPWStr)] string existingFileName,
        nint securityAttributes);

    internal static void CreateHardLink(string oldFileName, string newFileName)
    {
        if (!CreateHardLink(newFileName, oldFileName, IntPtr.Zero))
        {
            int errorCode = Marshal.GetLastWin32Error();
            throw new IOException($"CreateHardLink error {errorCode}", errorCode);
        }
    }
}
