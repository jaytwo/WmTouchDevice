using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using WmTouchDevice;

namespace WpfSample
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnSourceInitialized(object sender, EventArgs e)
        {
            var window = GetWindow(this);
            if (window != null)
            {
                var wih = new WindowInteropHelper(window);
                var hWnd = wih.Handle;
                MessageTouchDevice.RegisterTouchWindow(hWnd);
                var s = HwndSource.FromHwnd(hWnd);
                s?.AddHook(Hook);
            }
        }

        private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var window = GetWindow(this);
            if (window != null)
                MessageTouchDevice.WndProc(window, msg, wParam, lParam, ref handled);
            return IntPtr.Zero;
        }

        private void MainWindow_OnTouchDown(object sender, TouchEventArgs e)
        {
            var p = e.GetTouchPoint(MainCanvas).Position;

            var el = new Ellipse
            {
                Height = 15,
                Width = 15,
                Fill = Brushes.Green
            };
            MainCanvas.Children.Add(el);
            Canvas.SetLeft(el, p.X - 7.5);
            Canvas.SetTop(el, p.Y - 7.5);
        }

        private void MainWindow_OnTouchUp(object sender, TouchEventArgs e)
        {
            var p = e.GetTouchPoint(MainCanvas).Position;

            var el = new Ellipse
            {
                Height = 15,
                Width = 15,
                Fill = Brushes.Red
            };
            MainCanvas.Children.Add(el);
            Canvas.SetLeft(el, p.X - 7.5);
            Canvas.SetTop(el, p.Y - 7.5);
        }

        private void MainWindow_OnTouchMove(object sender, TouchEventArgs e)
        {
            var p = e.GetTouchPoint(MainCanvas).Position;
            var el = new Ellipse
            {
                Height = 5,
                Width = 5,
                Fill = Brushes.Black
            };
            MainCanvas.Children.Add(el);
            Canvas.SetLeft(el, p.X - 2.5);
            Canvas.SetTop(el, p.Y - 2.5);
        }
    }
}