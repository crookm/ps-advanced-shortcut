function Find-Shortcut {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]
        $Path,
        [Parameter(Mandatory = $true)]
        [string]
        $Target
    )

    $shortcuts = Get-ChildItem -Path $Path -Filter *.lnk
    if (-not $shortcuts) {
        return $null
    }

    $shell = New-Object -ComObject WScript.Shell
    return $shortcuts | Where-Object { $shell.CreateShortcut($_.FullName).TargetPath -eq $Target }
}