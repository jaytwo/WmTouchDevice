using System;
using System.Runtime.InteropServices;

namespace WmTouchDevice.Native
{
    [StructLayout(LayoutKind.Sequential)]
    struct TOUCHINPUT
    {
        public Int32 x;
        public Int32 y;
        public IntPtr hSource;
        public Int32 dwID;
        public TOUCHEVENTF dwFlags;
        public Int32 dwMask;
        public Int32 dwTime;
        public IntPtr dwExtraInfo;
        public Int32 cxContact;
        public Int32 cyContact;
    }
}
