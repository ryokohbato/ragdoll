using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ragdoll
{
  public class MainModel : BindableBase
  {
    private AppContext appContext;
    public MainModel(AppContext appContext)
    {
      this.appContext = appContext;
    }
  }
}
