using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace IDataAccess
{
      public interface IModuleAccess:IBaseAccess<Model.Module>
    {
        IList<Module> GetModuleList(int roleId);
  
    }
}
