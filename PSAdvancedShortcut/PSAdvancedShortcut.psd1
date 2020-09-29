#
# Module manifest for module 'PSAdvancedShortcut'
#
# Generated by: Matt Crook
# Generated on: 2020-09-25
#

@{
    ModuleVersion     = '0.1.0'

    # Supported PSEditions
    # CompatiblePSEditions = @()

    GUID              = 'd65f8fae-52e0-4688-a7f7-474364f7ab64'

    Author            = 'Matt Crook'
    # CompanyName       = ''
    Copyright         = '(c) Matt Crook 2020. All rights reserved.'
    Description       = 'Advanced shortcut appliance to create and modify shortcut file properties that are not easily accessible'

    PowerShellVersion = '2.0'

    # Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
    FunctionsToExport = @()

    # Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
    CmdletsToExport   = @(
        'New-Shortcut',
        'Find-Shortcut'
    )

    # Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
    AliasesToExport   = @()

    NestedModules     = @('PSAdvancedShortcut.dll')

    # Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
    PrivateData       = @{
        PSData = @{
            # Tags applied to this module. These help with module discovery in online galleries.
            # Tags = @()

            # A URL to the license for this module.
            # LicenseUri = ''

            # A URL to the main website for this project.
            # ProjectUri = ''

            # A URL to an icon representing this module.
            # IconUri = ''

            # ReleaseNotes of this module
            # ReleaseNotes = ''
        }
    }

    # HelpInfo URI of this module
    # HelpInfoURI = ''
}