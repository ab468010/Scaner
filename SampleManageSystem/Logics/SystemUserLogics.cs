using System;
using System.Collections.Generic;
using Model;
namespace Logics
{
    public class SystemUserLogics : BaseLogics<Model.SystemUser>
    {

        private const string _Type = "SystemUserAccess";
        private IDataAccess.ISystemUserAccess _Dal;

        public SystemUserLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.ISystemUserAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }

        }
        public IList<SystemUser> SelectUser()
        {
            return _Dal.SelectUser();
        }
        public SystemUser Login(SystemUser user)
        {
            return _Dal.GetSystemUser(user);
        }
        public bool CreateUser(SystemUser user)
        {
        
            return _Dal.CreateUser(user);
           
        }
        public SystemUser GetUser(int userId)
        {
            return _Dal.GetUser(userId);
        }
        public bool DeleteUser(int userId)
        {
            return _Dal.Delete(userId);
        }
        public bool UpdateUser(SystemUser user)
        {
            return _Dal.Update(user);
        }
    }
}