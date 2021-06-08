using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ragdoll
{
  public static class NativeMethods
  {
    public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lparam);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("User32")]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("User32.dll")]
    public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

    public static void SetWindowPosition(IntPtr handle, IntPtr hWndInsertAfter, uint uFlags)
    {
      RECT rect = new RECT();
      GetWindowRect(handle, ref rect);
      SetWindowPos(
       handle,
       hWndInsertAfter,
       (int)rect.left,
       (int)rect.top,
       (int)(rect.right - rect.left),
       (int)(rect.bottom - rect.top),
       uFlags);
    }

    public static bool IsWindowTopmost(IntPtr handle)
    {
      long result = GetWindowLong(handle, -20);
      if ((result & 0x00000008) == 0) return false;
      return true;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct RECT
  {
    public int left;
    public int top;
    public int right;
    public int bottom;
  }

  enum hWndInsertAfter
  {
    BOTTOM = 1,
    NOTOPMOST = -2,
    TOP = 0,
    TOPMOST = -1,
  }

  enum uFlags : uint
  {
    ASYNCWINDOWPOS = 0x4000,
    DEFERERASE = 0x2000,
    DRAWFRAME = 0x0020,
    FRAMECHANGED = 0x0020,
    HIDEWINDOW = 0x0080,
    NOACTIVATE = 0x0010,
    NOCOPYBITS = 0x0100,
    NOMOVE = 0x0002,
    NOOWNERZORDER = 0x0200,
    NOREDRAW = 0x0008,
    NOREPOSITION = 0x0200,
    NOSENDCHANGING = 0x0400,
    NOSIZE = 0x0001,
    NOZORDER = 0x0004,
    SHOWWINDOW = 0x0040,
  }
}
