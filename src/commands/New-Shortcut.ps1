function New-Shortcut {
    [CmdletBinding()]
    param (
        # The path to the shortcut, will add the .lnk file extension if not supplied
        [Parameter(Mandatory = $true)]
        [string]
        $Path,
        # File path to a file that will be executed when opening the shortcut
        [Parameter(Mandatory = $true)]
        [string]
        $Target,
        # Overwrite existing files
        [Parameter(Mandatory = $false)]
        [switch]
        $Force,

        # Arguments to pass to target
        [Parameter(Mandatory = $false)]
        [string]
        $Arguments,
        # Working directory the target will be opened in
        [Parameter(Mandatory = $false)]
        [string]
        $WorkingDirectory,
        # Window style of the target
        [Parameter(Mandatory = $false)]
        [string]
        $WindowStyle,
        # Description of the shortcut
        [Parameter(Mandatory = $false)]
        [string]
        $Description,
        # Icon of the shortcut
        [Parameter(Mandatory = $false)]
        [string]
        $Icon,
        # HotKey that will open the shortcut
        [Parameter(Mandatory = $false)]
        [string]
        $HotKey,

        # Extended attribute; the Application User Model ID
        [Parameter(Mandatory = $false)]
        [string]
        $AUMID,
        # Extended attribute; the Class Id of the target
        [Parameter(Mandatory = $false)]
        [string]
        $ClassId
    )
}