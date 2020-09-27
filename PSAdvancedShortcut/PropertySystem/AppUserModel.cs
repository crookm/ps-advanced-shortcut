using System;

namespace PSAdvancedShortcut.PropertySystem
{
	// Find explanations of properties, and the expected types here: https://docs.microsoft.com/en-us/windows/win32/properties/software-bumper
	public static class AppUserModel
    {
		public static PropertyKey RelaunchCommand => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 2);
		public static PropertyKey RelaunchIconResource => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 3);
		public static PropertyKey RelaunchDisplayNameResource => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 4);
		public static PropertyKey ID => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 5);
		public static PropertyKey IsDestinationListSeparator => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 6);
		public static PropertyKey ExcludeFromShowInNewInstall  => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 8);
		public static PropertyKey PreventPinning => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 9);
		public static PropertyKey IsDualMode => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 11);
		public static PropertyKey ToastActivatorCLSID => new PropertyKey(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 26);
	}
}
