using System;
using System.Runtime.InteropServices;

namespace WmTouchDevice.Native
{
    static class NativeMethods
    {
        public const int WM_TOUCH = 0x0240;
        public const uint TWF_WANTPALM = 0x00000002;

        public static readonly Int32 TouchInputSize = Marshal.SizeOf(new TOUCHINPUT());

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean RegisterTouchWindow(IntPtr hWnd, UInt32 ulFlags);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean GetTouchInputInfo(IntPtr hTouchInput, Int32 cInputs, [In, Out] TOUCHINPUT[] pInputs, Int32 cbSize);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern void CloseTouchInputHandle(IntPtr lParam);
    }
}