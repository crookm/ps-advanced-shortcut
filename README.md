# PSAdvancedShortcut

[![PowerShell Gallery Downloads](https://img.shields.io/powershellgallery/v/PSAdvancedShortcut?logo=powershell&logoColor=aaa&style=flat-square) ![PowerShell Gallery Downloads](https://img.shields.io/powershellgallery/dt/PSAdvancedShortcut?logo=powershell&logoColor=aaa&style=flat-square) ![PowerShell Gallery Platform](https://img.shields.io/powershellgallery/p/PSAdvancedShortcut?logo=powershell&logoColor=aaa&style=flat-square)](https://www.powershellgallery.com/packages/PSAdvancedShortcut)

## Background

PSAdvancedShortcut is a tool to allow the creation of shortcut files on Windows machines. Its purpose is to make the powerful hidden properties of shortcuts easy to set, in a programmatic way.

Some of the properties that can be set include:

- Target
- Arguments
- Working directory
- Description
- Window launch style
- Icon (including index offset)
- Application user model ID (AUMID)
- Toast activator class ID

## Installation

You can install [PSAdvancedShortcut](https://www.powershellgallery.com/packages/PSAdvancedShortcut) via the Powershell Gallery:

```powershell
Install-Module -Name PSAdvancedShortcut
```

## Documentation

### New-Shortcut

Creates a new shortcut with the desired properties.

```powershell
New-Shortcut
    -Name <string>
    -Target <string>
    [-Arguments <string>]
    [-Path <string>]
    [-Description <string>]
    [-WorkingDirectory <string>]
    [-WindowStyle <WindowStyle>]
    [-IconPath <string>]
    [-IconIndex <int>]
    [-AppUserModelId <string>]
    [-ToastActivatorClassId <Guid>]
    [-Force]
    [<CommonParameters>]
```

#### Examples

Create a simple shortcut named `MyShortcut` in the current directory, targeting the calculator:
```powershell
New-Shortcut -Name MyShortcut -Target calc.exe
```

Creates a shortcut on the desktop called `MyDesktopShortcut`, targeting the calculator:
```powershell
New-Shortcut -Name MyDesktopShortcut -Path ~\Desktop -Target calc.exe
```

Creates a shortcut in the current directory named `TreeCalc` with a tree icon, targeting the calculator:
```powershell
New-Shortcut -Name TreeCalc -Target calc.exe -IconPath C:\Windows\System32\SHELL32.dll -IconIndex 41
```

#### Parameters

##### `-Name`
The name of the shortcut. Will automatically append `.lnk` if not included - but note that the file extension is always hidden by the OS. If a shortcut by this name already exists in the directory, you must specify `-Force` to replace it.

##### `-Target`
The executable or shell destination of the shortcut.

##### `-Arguments`
Arguments to pass into the target.

##### `-Path`
The directory to create the shortcut under. The directory must exist and be writable.

##### `-Description`
A string that describes the shortcut. This will be displayed when the user hovers over the shortcut icon.

##### `-WorkingDirectory`
Specifies the directory that the target should be launched under.

##### `-WindowStyle`
The style of the window that the target should open in. The acceptable values are `Normal`, `Maximized`, and `Minimized`. Note that this does not work for all applications.

##### `-IconPath`
The full path to a file which contains an icon. This is usually a .ico file, but may also include .icl, .exe, and .dll files.

##### `-IconIndex`
Icon files, as well as binaries, may contain multiple icons. You can specify the offset with this parameter. The index starts at zero.

##### `-AppUserModelId`
The AUMID to set for this shortcut. The format typically follows Java's [package name rules](https://docs.oracle.com/javase/specs/jls/se6/html/packages.html#7.7), but can be any string. This only applies to Windows 8 and above.

##### `-ToastActivatorClassId`
Specifies the GUID for the registered COM class which will be activated when a user clicks on a notification in the Action Center from your application. This only applies to Windows 8 and above.

##### `-Force`
Specifies that an existing shortcut should be overwritten.

#### Inputs

None

#### Outputs

None

---

### Find-Shortcut

Finds a shortcut that links to the specified target, within a specified directory.

```powershell
Find-Shortcut
    -Target <string>
    [-Path <string>]
    [-Recurse]
    [<CommonParameters>]
```

#### Examples

Find the shortcut files which target the calculator:
```powershell
Find-Shortcut -Target C:\Windows\System32\calc.exe
```

#### Parameters

##### `-Target`
The absolute path of the target you wish to find shortcuts for.

Note that if you created a shortcut with a non-absolute path to a target, the OS will fill-in the rest of the path to become absolute at creation time. You will not, for example, be able to find any shortcuts which target `calc.exe`.

##### `-Path`
The directory which should be searched to find shortcuts. Defaults to the current directory. The directory must exist.

##### `-Recurse`
Recursively search into subdirectories, including network drives and symbolic links.

#### Inputs
`string`

A string representing the full path to the target may be supplied from the pipeline, instead of using the parameter `-Target`.

#### Outputs
`string[]`

String array of absolute file paths of the shortcuts which match the target specified.