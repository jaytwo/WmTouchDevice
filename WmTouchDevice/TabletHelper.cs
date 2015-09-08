using System;
using System.Reflection;
using System.Windows.Input;

namespace WmTouchDevice
{
    /// <summary>
    /// Based on https://msdn.microsoft.com/en-us/library/vstudio/dd901337(v=vs.90).aspx
    /// </summary>
    public static class TabletHelper
    {
        public static bool HasRemovedDevices { get; private set; }

        private readonly static object _stylusLogic;
        private readonly static Type _stylusLogicType;
        private readonly static FieldInfo _countField452;

        static TabletHelper()
        {
            var inputManagerType = typeof(System.Windows.Input.InputManager);

            _stylusLogic = inputManagerType.InvokeMember("StylusLogic", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, System.Windows.Input.InputManager.Current, null);

            if (_stylusLogic != null)
            {
                _stylusLogicType = _stylusLogic.GetType();
                _countField452 = _stylusLogicType.GetField("_lastSeenDeviceCount", BindingFlags.Instance | BindingFlags.NonPublic);
            }
        }

        public static void DisableWPFTabletSupport(IntPtr hWnd)
        {
            while (Tablet.TabletDevices.Count > 0)
            {
                // Only in .Net Framework 4.5.2 - see https://connect.microsoft.com/VisualStudio/Feedback/Details/1016534
                if (_countField452 != null)
                    _countField452.SetValue(_stylusLogic, 1 + (int)_countField452.GetValue(_stylusLogic));

                int index = Tablet.TabletDevices.Count - 1;

                _stylusLogicType.InvokeMember("OnTabletRemoved", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, _stylusLogic, new object[] { (uint)index });

                HasRemovedDevices = true;
            }
        }
    }
}
