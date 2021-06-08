using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Diagnostics;
using System.Windows.Data;

namespace ragdoll
{
  public partial class MainViewModel : BindableBase
  {
    public DelegateCommand GetMainWindowCommand { get; private set; }
    public DelegateCommand PinWindowCommand { get; private set; }
    public DelegateCommand UnPinWindowCommand { get; private set; }
    public DelegateCommand CloseWindowCommand { get; private set; }

    private void UpdateWindowInformation()
    {
      this.WindowInformations__pinned = new();
      this.WindowInformations__not_pinned = new();
      this.GetMainWindowCommand.Execute(null);
      this.WindowInformations__pinned = this.WindowInformations__pinned;
    }

    private void ExecuteGetMainWindow(object _)
    {
      NativeMethods.EnumWindows(EnumWindowCallBack, IntPtr.Zero);
    }

    private bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
    {
      if (NativeMethods.IsWindowVisible(hWnd))
      {
        RECT rect = new();
        NativeMethods.GetWindowRect(hWnd, ref rect);
        if (rect.left == rect.right || rect.top == rect.bottom)
        {
          return true;
        }
        StringBuilder caption = new StringBuilder(0x1000);
        NativeMethods.GetWindowText(hWnd, caption, caption.Capacity);
        if (caption.Length > 0)
        {
          if (NativeMethods.IsWindowTopmost(hWnd))
          {
            this.WindowInformations__pinned.Add(new WindowInformation(caption.ToString(), hWnd));
          }
          else
          {
            this.WindowInformations__not_pinned.Add(new WindowInformation(caption.ToString(), hWnd));
          }
        }
      }

      return true;
    }

    private bool CanExecuteGetMainWindow()
    {
      return true;
    }

    private void ExecutePinWindow(object parameter)
    {
      if (parameter != null)
      {
        NativeMethods.SetWindowPosition((IntPtr)parameter, (IntPtr)hWndInsertAfter.TOPMOST, (uint)(uFlags.NOMOVE | uFlags.NOSIZE));
      }
      UpdateWindowInformation();
    }

    private bool CanExecutePinWindow()
    {
      return true;
    }

    private void ExecuteUnPinWindow(object parameter)
    {
      if (parameter != null)
      {
        NativeMethods.SetWindowPosition((IntPtr)parameter, (IntPtr)hWndInsertAfter.NOTOPMOST, (uint)(uFlags.NOMOVE | uFlags.NOSIZE));
      }
      UpdateWindowInformation();
    }

    private bool CanExecuteUnPinWindow()
    {
      return true;
    }

    private void ExecuteCloseWindow(object _)
    {
      VVMConnector.Window.Close();
    }

    private bool CanExecuteCloseWindow()
    {
      return true;
    }
  }
}
