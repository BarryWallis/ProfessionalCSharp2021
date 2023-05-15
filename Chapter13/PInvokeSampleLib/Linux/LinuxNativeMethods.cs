using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace PInvokeSample;

[SupportedOSPlatform("Linux")]
internal static class LinuxNativeMethods
{
    internal enum LinkErrors
    {
        EPERM = 1,
        ENOENT = 2,
        EIO = 5,
        EACCES = 13,
        EEXIST = 17,
        EXDEV = 18,
        ENOSPC = 28,
        EROFS = 30,
        EMLINK = 31,
    }

    private static readonly Dictionary<LinkErrors, string> _errorMessages = new()
    {
        {LinkErrors.EPERM, "OnGNU/Linux and GNU/Hurd systems and some others you cannot make links to " +
            "directories. Many systems allow only privileged users to do so." },
        {LinkErrors.ENOENT, "The file named oldname doesn't exist. You can't make a link to a file that " +
            "doesn't exist." },
        {LinkErrors.EIO, "A hardware error occurred while trying to read or write to the filesystem." },
        { LinkErrors.EACCES, "You are not allowed to write to the directory in which the new link is to " +
            "be written." },
        { LinkErrors.EEXIST, "There is already a file named newname. If you want to replace this link with a " +
            "new link, you must remove the old link explicitly first." },
        { LinkErrors.EXDEV, "The directory specified in newname is on a different file system than the " +
            "existing file." },
        { LinkErrors.ENOSPC, "The directory or file system that would contain the new link is full and " +
            "cannot be extended." },
        { LinkErrors.EROFS, "The directory containing the new link can’t be modified because it’s on a " +
            "read-only file system." },
        { LinkErrors.EMLINK, "There are already too many links to the file named by oldname. (The maximum " +
            "number of links to a file is LINK_MAX; see Section 32.6 [Limits on File System Capacity], " +
            "page 904.)" }

    };

    [DllImport("libc", 
               EntryPoint = "Link", 
               CallingConvention = CallingConvention.Cdecl, 
               SetLastError = true, 
               CharSet = CharSet.Unicode)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
    private static extern int Link(string oldPath, string newpath);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

    internal static void CreateHardLink(string newFileName, string oldFileName)
    {
        int result = Link(newFileName, oldFileName);
        if (result != 0)
        {
            int errorCode = Marshal.GetLastWin32Error();
            if (!_errorMessages.TryGetValue((LinkErrors)errorCode, out string? errorText))
            {
                errorText = "No error message defined";
            }
            
            throw new IOException(errorText, errorCode);
        }
    }
}
