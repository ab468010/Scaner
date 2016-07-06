using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace IDataAccess
{
     public  interface IPrivilegeAccess:IBaseAccess<Model.Privilege>
    {
        IList<Privilege> GetPrivilegeList();
        int CreatePrivilege(Privilege privilege);
        bool CreateRolePrivilege(int privilegeid, int roleId);
        IList<Privilege> GetRolePrivilegeList(int roleid);
        bool DeleteRolePrivilege(int privilegeid);
    }
}
