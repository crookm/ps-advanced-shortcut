$ScriptRoot = $PSScriptRoot
if (-not $ScriptRoot) {
    # PSv2 does not have an automatic variable to its invocation directory, unlike v3+
    $ScriptRoot = Split-Path -Path $MyInvocation.MyCommand.Path -Parent
}

# Import the functions that are in active use here
#   - When naming your exported functions, please adhere to the approved verbs: https://docs.microsoft.com/en-us/powershell/scripting/developer/cmdlet/approved-verbs-for-windows-powershell-commands
#   - Functions that should not be exported (such as helpers) must be in PascalCase.

# Commands
. $ScriptRoot\src\commands\Find-Shortcut.ps1
. $ScriptRoot\src\commands\New-Shortcut.ps1
. $ScriptRoot\src\commands\Set-ShortcutProperty.ps1

Export-ModuleMember -Function *-*
