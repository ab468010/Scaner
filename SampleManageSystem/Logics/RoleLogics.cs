using System;
using System.Collections.Generic;
using Model;
namespace Logics
{
    public class RoleLogics:BaseLogics<Model.Role>
    {
        private const string _Type = "RoleAccess";
        private IDataAccess.IRoleAccess _Dal;

        public RoleLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IRoleAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public IList<Role> GetPageRoleList(int number)
        {
            return _Dal.GetPageRoleList(number);
        }
        public IList<Role> GetRoleList()
        {
            return _Dal.GetRoleList();
        }
        public bool CreateRole(Role role)
        {
            return _Dal.Create(role);
        }
        public Role GetRole(int roleid)
        {
            return _Dal.GetModel(roleid);
        }
        public bool DeleteRole(int roleid)
        {
            if (_Dal.ExistUser(roleid))
            {
                return false;
            }
            else {
                _Dal.DeleteRolePrivilege(roleid);
                return _Dal.Delete(roleid);
            }
           
          
        }
        public long GetRoleCount()
        {
            return _Dal.GetRoleCount();
        }
        public int GetRoleType(int roleid)
        {
            return _Dal.GetRoleType(roleid);
        }
    }
}
