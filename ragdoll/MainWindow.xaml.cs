using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace ragdoll
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      VVMConnector.Window = this;

      (this.DataContext as MainViewModel).GetMainWindowCommand.Execute(null);
    }

    public override void OnApplyTemplate()
    {
      ApplyBlur.ChangeState(this, AccentState.ACCENT_ENABLE_BLURBEHIND);

      base.OnApplyTemplate();
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
      public WindowCompositionAttribute Attribute;
      public IntPtr Data;
      public int SizeOfData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
      public AccentState AccentState;
      public int AccentFlags;
      public uint GradientColor;
      public int AnimationId;
    }

    internal enum AccentState
    {
      ACCENT_DISABLED = 0,
      ACCENT_ENABLE_GRADIENT = 1,
      ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
      ACCENT_ENABLE_BLURBEHIND = 3,
      ACCENT_INVALID_STATE = 4
    }

    internal enum WindowCompositionAttribute
    {
      WCA_ACCENT_POLICY = 19
    }

    internal class NativeMethods
    {
      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }

    public class ApplyBlur
    {
      internal static void ChangeState(Window win, AccentState state)
      {
        var windowHelper = new WindowInteropHelper(win);

        var accent = new AccentPolicy
        {
          AccentState = state,
          AccentFlags = 2,
          // ABGRの順に指定
          GradientColor = 0x00000000,
        };

        var accentStructSize = Marshal.SizeOf(accent);
        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData
        {
          Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
          SizeOfData = accentStructSize,
          Data = accentPtr
        };

        _ = NativeMethods.SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        Marshal.FreeHGlobal(accentPtr);
      }
    }

    private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (e.ButtonState != MouseButtonState.Pressed) return;

      this.DragMove();
    }
  }
}
