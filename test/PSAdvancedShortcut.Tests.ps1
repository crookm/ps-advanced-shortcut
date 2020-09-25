Describe "Module manifest tests" {
    It "Passes module manifest test" {
        $moduleManifestPath = "$PSScriptRoot\..\PSAdvancedShortcut.psd1"
        Test-ModuleManifest -Path $moduleManifestPath
        $? | Should -Be $true
    }
}
