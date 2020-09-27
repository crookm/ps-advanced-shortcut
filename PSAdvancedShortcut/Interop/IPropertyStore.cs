using System;
using System.Runtime.InteropServices;
using PSAdvancedShortcut.Interop.Contracts;
using PSAdvancedShortcut.PropertySystem;

namespace PSAdvancedShortcut.Interop
{
    [ComImport,
        Guid(ShellCOMGuid.IPropertyStore),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IPropertyStore
    {
        UInt32 GetCount([Out] out uint propertyCount);
        UInt32 GetAt([In] uint propertyIndex, out PropertyKey key);
        UInt32 GetValue([In] ref PropertyKey key, [Out] PropVariant pv);
        UInt32 SetValue([In] ref PropertyKey key, [In] PropVariant pv);
        UInt32 Commit();
    }
}
