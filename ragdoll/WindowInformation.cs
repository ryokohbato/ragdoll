using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ragdoll
{
  public class WindowInformation
  {
    public string WindowTitle { get; private set; }
    public IntPtr WindowHandle { get; private set; }

    public WindowInformation(string windowTitle, IntPtr windowHandle)
    {
      this.WindowTitle = windowTitle;
      this.WindowHandle = windowHandle;
    }
  }
}
