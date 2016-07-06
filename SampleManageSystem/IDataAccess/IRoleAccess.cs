using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace IDataAccess
{
      public interface IRoleAccess:IBaseAccess<Model.Role>
    {
        IList<Role> GetRoleList();
        IList<Role> GetPageRoleList(int number);
   
        bool ExistUser(int roleid);
        long GetRoleCount();
        bool DeleteRolePrivilege(int roleid);
        int GetRoleType(int roleid);
    }
}
