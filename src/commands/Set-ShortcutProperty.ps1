function Set-ShortcutProperty {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateScript( { Test-Path $_ } )]
        [string]
        $Path,
        [ValidateSet("Target", "Arguments", "WorkingDirectory", "WindowStyle", "Description", "Icon", "HotKey", "AUMID", "ClassId")]
        [Parameter(Mandatory = $true)]
        [string]
        $Property,
        [Parameter(Mandatory = $false)]
        [string]
        $Value
    )
}