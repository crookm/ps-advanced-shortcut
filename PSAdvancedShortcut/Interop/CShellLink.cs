using System;
using System.Runtime.InteropServices;
using PSAdvancedShortcut.Interop.Contracts;

namespace PSAdvancedShortcut.Interop
{
    [ComImport,
        Guid(ShellCOMGuid.CShellLink),
        ClassInterface(ClassInterfaceType.None)]
    internal class CShellLink { }
}
