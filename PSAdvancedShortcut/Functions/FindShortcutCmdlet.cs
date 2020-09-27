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
            Mandatory = true,
            HelpMessage = "Path to look for shortcuts")]
        public string Path { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "Target to match within the shortcuts")]
        public string Target { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Recurse the search into subdirectories, including network drives and symbolic links")]
        public bool Recurse { get; set; }

        protected override void ProcessRecord()
        {
            var files = Directory.GetFiles(Path, "*.lnk", Recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
            {
                WriteObject(null);
                return;
            }

            var shell = new WshShell();
            var matches = files
                .Where(file => ((IWshShortcut)shell.CreateShortcut(file)).TargetPath == Target)
                .ToArray();
            WriteObject(matches);
        }
    }
}
