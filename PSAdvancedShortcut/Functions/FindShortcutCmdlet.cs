using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using IWshRuntimeLibrary;

namespace PSAdvancedShortcut.Functions
{
    [Cmdlet(VerbsCommon.Find, "Shortcut")]
    public class FindShortcutCmdlet : PSCmdlet
    {
        [Parameter(
            Mandatory = false,
            HelpMessage = "Path to look for shortcuts, defaults to current directory")]
        public string Path { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            HelpMessage = "Target to match within the shortcuts")]
        public string Target { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Recursively search into subdirectories, including network drives and symbolic links")]
        public SwitchParameter Recurse { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(Path))
            {
                Path = SessionState.Path.CurrentFileSystemLocation.Path;
                WriteVerbose("Path parameter was not set, defaulting to current PS session filesystem directory");
            }

            WriteVerbose($"Looking for shortcut files in the path: {Path}");
            var files = Directory.GetFiles(Path, "*.lnk", Recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
            {
                WriteVerbose("Could not find any shortcut files in the specified path");
                WriteObject(null);
                return;
            }

            WriteVerbose($"Checking {files.Count()} shortcut files for the target: {Target}");
            var shell = new WshShell();
            var matches = files
                .Where(file => string.Equals(((IWshShortcut)shell.CreateShortcut(file)).TargetPath, Target, StringComparison.OrdinalIgnoreCase))
                .ToArray();
            WriteVerbose($"Found {matches.Count()} shortcut matches");
            WriteObject(matches);
        }
    }
}
