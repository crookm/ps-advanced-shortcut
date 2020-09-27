using System;
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
            Mandatory = true,
            HelpMessage = "File path to where the shortcut should be created")]
        public string Path { get; set; }

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
            HelpMessage = "Arguments to pass to the target")]
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
            HelpMessage = "Icon path of a binary which contains an icon")]
        public string IconPath { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Index of the icon in the binary specified in IconPath")]
        public int IconIndex { get; set; }

        // [Parameter(
        //     Mandatory = false,
        //     HelpMessage = "HotKey that will open the shortcut")]
        // public string HotKey { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "The AUMID of the target")]
        public string AppUserModelId { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "The toast activator class id of the target")]
        public Guid ToastActivatorClassId { get; set; }

        protected override void ProcessRecord()
        {
            if (!Path.EndsWith(".lnk")) Path += ".lnk";
            if (File.Exists(Path) && !Force.IsPresent)
                throw new ArgumentException("File already exists at destination, and 'Force' parameter was not specified");

            var shortcut = (IShellLinkW)new CShellLink();
            shortcut.SetPath(Target);

            if (!string.IsNullOrEmpty(Arguments)) shortcut.SetArguments(Arguments);
            if (!string.IsNullOrEmpty(WorkingDirectory)) shortcut.SetWorkingDirectory(WorkingDirectory);
            if (WindowStyle.HasValue) shortcut.SetShowCmd((uint)WindowStyle.Value);
            if (!string.IsNullOrEmpty(Description)) shortcut.SetDescription(Description);
            if (!string.IsNullOrEmpty(IconPath)) shortcut.SetIconLocation(IconPath, IconIndex);
            //if (!string.IsNullOrEmpty(Hotkey)) shortcut.SetHotKey(Hotkey);

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
                    if (shortcutExtended.SetValue(AppUserModel.ID, toastActivatorIdVariant) > 1)
                        throw new Exception("Failed to set Toast Activator Class Id property");
                    if (shortcutExtended.Commit() > 1)
                        throw new Exception("Failed to commit Toast Activator Class Id property");
                }
            }

            var shortcutPersist = (IPersistFile)shortcut;
            if (shortcutPersist.Save(Path, true) > 1)
                throw new Exception("Failed to save shortcut to path specified");
        }
    }
}
