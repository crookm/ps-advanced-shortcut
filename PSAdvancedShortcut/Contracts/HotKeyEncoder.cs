using System;

namespace PSAdvancedShortcut.Contracts
{
    internal static class HotKeyEncoder
    {
        private const byte HOTKEYF_SHIFT   = 0x01;
        private const byte HOTKEYF_CONTROL = 0x02;
        private const byte HOTKEYF_ALT     = 0x04;

        /// <summary>
        /// Parses a hotkey string such as "Ctrl+Alt+T" and encodes it into the 16-bit
        /// value expected by IShellLinkW::SetHotKey. The low byte holds the virtual key
        /// code and the high byte holds the modifier flags.
        /// </summary>
        /// <param name="hotkey">Case-insensitive, '+'-delimited string. Recognised modifiers:
        /// Ctrl, Alt, Shift. Recognised keys: A-Z, 0-9, F1-F24.</param>
        /// <returns>Encoded 16-bit hotkey value.</returns>
        /// <exception cref="ArgumentException">Thrown for unrecognized tokens or missing key.</exception>
        public static short Encode(string hotkey)
        {
            var parts = hotkey.Split('+');
            byte modifiers = 0;
            byte vkCode = 0;

            foreach (var part in parts)
            {
                var token = part.Trim();
                if (string.IsNullOrEmpty(token))
                    throw new ArgumentException($"Invalid hotkey string: '{hotkey}'");

                switch (token.ToUpperInvariant())
                {
                    case "CTRL":
                        modifiers |= HOTKEYF_CONTROL;
                        break;
                    case "ALT":
                        modifiers |= HOTKEYF_ALT;
                        break;
                    case "SHIFT":
                        modifiers |= HOTKEYF_SHIFT;
                        break;
                    default:
                        if (vkCode != 0)
                            throw new ArgumentException($"Multiple keys specified in hotkey string: '{hotkey}'");
                        vkCode = ParseKey(token, hotkey);
                        break;
                }
            }

            if (vkCode == 0)
                throw new ArgumentException($"No key specified in hotkey string: '{hotkey}'");

            return (short)((modifiers << 8) | vkCode);
        }

        private static byte ParseKey(string token, string original)
        {
            if (token.Length == 1)
            {
                char c = char.ToUpperInvariant(token[0]);
                if (c >= 'A' && c <= 'Z') return (byte)c;
                if (c >= '0' && c <= '9') return (byte)c;
            }

            if (token.StartsWith("F", StringComparison.OrdinalIgnoreCase) && token.Length >= 2)
            {
                int fNum;
                if (int.TryParse(token.Substring(1), out fNum) && fNum >= 1 && fNum <= 24)
                    return (byte)(0x6F + fNum); // VK_F1 = 0x70
            }

            throw new ArgumentException($"Unrecognised key '{token}' in hotkey string: '{original}'. Valid keys are A-Z, 0-9, and F1-F24.");
        }
    }
}
