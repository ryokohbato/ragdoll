using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ragdoll
{
  public class AppContext : BindableBase
  {
    public MainModel MainModel { get; private set; }
    
    public static int ItemMarginWidth = 6;

    public AppContext()
    {
      this.MainModel = new MainModel(this);
    }
  }
}
