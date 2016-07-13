using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Logics
{
     public class PrivilegeLogics:BaseLogics<Model.Privilege>
    {
        private const string _Type = "PrivilegeAccess";
        private IDataAccess.IPrivilegeAccess _Dal;

        public PrivilegeLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IPrivilegeAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public IList<Privilege> GetPrivilegeList()
        {
            return _Dal.GetPrivilegeList();
        }
        public bool CreateRolePrivilege(Privilege privilege,int roleId)
        {

                return _Dal.CreateRolePrivilege(_Dal.CreatePrivilege(privilege), roleId);

        }
        public IList<Privilege> GetRolePrivilegeList(int roleid)
        {
            return _Dal.GetRolePrivilegeList(roleid);
        }
        public bool DeletePrivilege(int privilegeid)
        {
            if (_Dal.DeleteRolePrivilege(privilegeid))
            {
                return _Dal.Delete(privilegeid);
            }
            else
            {
                return false;
            }
        }
      
    }
}
