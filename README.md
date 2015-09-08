# WmTouchDevice

WmTouchDevice contains a basic implementation of WPF's TouchDevice class based on the data received in WM_TOUCH window messages rather than the Tablet PC API.

This is primarily intended to work around the [WPF Touch Event Fires with Delay](https://connect.microsoft.com/VisualStudio/feedback/details/782456/wpf-touch-event-fires-with-delay) bug in WPF, particularly severe with multi-touch.  However, this libary can be useful to enable other functions that require WM_TOUCH such as App-style focus tracking in Windows 8.

More information on this issue and solution is posted [here](http://jayheu.lingohq.io/blog/archives/2015/07/25/multi-touch-and-wpf).

### Usage

* Call MessageTouchDevice.RegisterTouchWindow(IntPtr hWnd) during or after your Window's SourceInitialized event
* Call MessageTouchDevice.WndProc from a function you attach to your Window using HwndSource.AddHook

### Contact

If you wish to contact me directly please use twitter https://twitter.com/jaytwotwit.

### Contributing

I am open to pull requests, so you're very welcome to create them.

I haven't had time to add a sample project yet, but would like one.