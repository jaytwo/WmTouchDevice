
# Multi-touch and WPF

Microsoft’s WPF desktop UI framework can suffer [a severe lag in input events](https://connect.microsoft.com/VisualStudio/feedback/details/782456/wpf-touch-event-fires-with-delay) when multiple fingers are on the touch screen.

This library contains a basic re-implementation of WPF's TouchDevice class based on the data received in WM_TOUCH window messages rather than the Tablet PC API.

This is primarily intended to work around the lag bug, but could be useful to enable other functions that require WM_TOUCH such as App-style focus tracking in Windows 8.

Two steps (or equivalents) are needed, firstly to disable the Tablet PC API, then to use replacement TouchDevice implementations.


## Step 1: Disable StylusLogic

WPF uses the Windows 7+ RealTimeStylus API to raise pen and touch events:
```
PenIMC.dll > StylusLogic > TouchDevice > Events
```
The the lag is in StylusLogic, which we can't fix but can at least trick into no longer raising events using a  [by faking disconnection of the touch hardware](https://msdn.microsoft.com/en-us/library/vstudio/dd901337(v=vs.90).aspx).

*Step 1: Add a call to `MessageTouchDevice.RegisterTouchWindow(IntPtr hWnd)` during or after your Window's SourceInitialized event.*

Your window will still respond to touches after adding this, but you should find that no touch events are being raised - touch input will only be seen by your code as mouse events. WPF touch behaviours such as panning will stop working as they rely on touch events.

## Step 2: Re-implement TouchDevice

WPF's TouchDevice is abstract, so a subclass can raise events and continue to manage properties like Focus:

```
(StylusLogic replacement) > TouchDevice > Events
```

We could base an implementation on any of:

-   PenIMC.dll, seemingly undocumented
-   DirectInput, poorly implemented by touch drivers
-   RawInput,  [requiring hardware-specific code](http://www.codeproject.com/Articles/381673/Using-the-RawInput-API-to-Process-MultiTouch-Digit)
-   WM_POINTER window messages, from Windows 8+
-   WM_TOUCH window messages, from Windows 7+

For maximum compatibility this library contains an implementation using WM_TOUCH window messages, that need to be forwarded to its handler.

*Step 2: Add a call to `MessageTouchDevice.WndProc` from a function you've attached to your Window using HwndSource.AddHook.*

Your window should start raising WPF touch events again after adding this, in turn restoring WPF gestures such as panning.

*Having used the RealTimeStylus API prevents a window receiving WM_TOUCH and WM_POINTER window messages (bar WM_POINTERDOWN and UP) but the call in step 1 will have both shut down the RealTimeStylusAPI and called `RegisterTouchWindow` to undo this.*

This won’t quite restore multi-touch visual feedback on Windows 8, but the [SetWindowFeedbackSetting](https://msdn.microsoft.com/en-us/library/windows/desktop/hh802871(v=vs.85).aspx) function can help.