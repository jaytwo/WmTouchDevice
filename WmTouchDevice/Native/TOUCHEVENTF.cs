using System;

namespace WmTouchDevice.Native
{
    [Flags]
    enum TOUCHEVENTF
    {
        TOUCHEVENTF_MOVE = 0x0001,
        TOUCHEVENTF_DOWN = 0x0002,
        TOUCHEVENTF_UP = 0x0004,
        TOUCHEVENTF_INRANGE = 0x0008,
        TOUCHEVENTF_PRIMARY = 0x0010,
        TOUCHEVENTF_NOCOALESCE = 0x0020,
        TOUCHEVENTF_PALM = 0x0080,
    }
}
