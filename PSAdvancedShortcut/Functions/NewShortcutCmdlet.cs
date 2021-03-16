using System;
using System.IO;
using System.Management.Automation;
using PSAdvancedShortcut.Contracts;
using PSAdvancedShortcut.Interop;
using PSAdvancedShortcut.PropertySystem;
using File = System.IO.File;

namespace PSAdvancedShortcut.Functions
{
    [Cmdlet(VerbsCommon.New, "Shortcut")]
    public class NewShortcutCmdlet : PSCmdlet
    {
        [Parameter(
            Mandatory = false,
            HelpMessage = "File path to where the shortcut should be created under, defaults to current directory")]
        public string Path { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "Name for the shortcut, extension will be added if not included")]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "File path to the target")]
        public string Target { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Specify this switch to overwrite existing files")]
        public SwitchParameter Force { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Arguments to pass to the target on execution")]
        public string Arguments { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Working directory the target will be executed in")]
        public string WorkingDirectory { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Window style the target will open in")]
        public WindowStyle? WindowStyle { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Description of the shortcut")]
        public string Description { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Path to a file with icons embedded in it, this is usually a .ico file - but could also be a .icl, .exe, or .dll")]
        public string IconPath { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "If the file specified in IconPath has multiple icons available, you may specify the index here")]
        public int IconIndex { get; set; }

        // [Parameter(
        //     Mandatory = false,
        //     HelpMessage = "HotKey that will open the shortcut")]
        // public string HotKey { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "The Application User Model Id of the target, used by Windows to associate disperate processes as a single application")]
        public string AppUserModelId { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "The Toast Activator Class Id of the target, used by Windows to find an application to execute when a person taps a notification in the Action Center")]
        public Guid ToastActivatorClassId { get; set; }

        protected override void ProcessRecord()
        {
            if (!Name.EndsWith(".lnk")) Name += ".lnk";
            if (string.IsNullOrEmpty(Path))
            {
                Path = SessionState.Path.CurrentFileSystemLocation.Path;
                WriteVerbose("Path parameter was not set, defaulting to current PS session filesystem directory");
            }

            var fullPath = System.IO.Path.Combine(Path, Name);
            WriteVerbose($"Shortcut will be saved as: {fullPath}");

            if (File.Exists(fullPath) && !Force.IsPresent)
                throw new ArgumentException("File already exists at destination, and 'Force' parameter was not specified");

            var shortcut = (IShellLinkW)new CShellLink();
            shortcut.SetPath(Target);

            if (!string.IsNullOrEmpty(Arguments)) shortcut.SetArguments(Arguments);
            if (!string.IsNullOrEmpty(WorkingDirectory)) shortcut.SetWorkingDirectory(WorkingDirectory);
            if (WindowStyle.HasValue) shortcut.SetShowCmd((uint)WindowStyle.Value);
            if (!string.IsNullOrEmpty(Description)) shortcut.SetDescription(Description);
            if (!string.IsNullOrEmpty(IconPath)) shortcut.SetIconLocation(IconPath, IconIndex);
            //if (!string.IsNullOrEmpty(HotKey)) shortcut.SetHotKey(HotKey);

            var shortcutExtended = (IPropertyStore)shortcut;
            if (!string.IsNullOrEmpty(AppUserModelId))
            {
                using (var appIdVariant = new PropVariant(AppUserModelId))
                {
                    if (shortcutExtended.SetValue(AppUserModel.ID, appIdVariant) > 1)
                        throw new Exception("Failed to set App User Model Id property");
                    if (shortcutExtended.Commit() > 1)
                        throw new Exception("Failed to commit App User Model Id property");
                }
            }

            if (ToastActivatorClassId != null)
            {
                using (var toastActivatorIdVariant = new PropVariant(ToastActivatorClassId.ToString("B")))
                {
                    if (shortcutExtended.SetValue(AppUserModel.ToastActivatorCLSID, toastActivatorIdVariant) > 1)
                        throw new Exception("Failed to set Toast Activator Class Id property");
                    if (shortcutExtended.Commit() > 1)
                        throw new Exception("Failed to commit Toast Activator Class Id property");
                }
            }

            var shortcutPersist = (IPersistFile)shortcut;
            if (shortcutPersist.Save(fullPath, true) > 1)
                throw new Exception("Failed to save shortcut to path specified");
        }
    }
}
