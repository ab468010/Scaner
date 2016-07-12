using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class UserLogics : BaseLogics<User>
    {
        private const string _Type = "UserAccess";
        private IDataAccess.IUserAccess _Dal;

        public UserLogics():base(_Type)
        {
            _Dal = base.dal as IDataAccess.IUserAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
     
        public bool CreateUser(User user)
        {
            if (_Dal.Exists(user))
            {
                return false;
            }
            else
            {
                return _Dal.Create(user);
            }
            
        }

        public User UserLogin(string username, string password)
        {
            return _Dal.GetUser(username,password);
        }
        public IList<User> GetUserList()
        {
            return _Dal.GetUserList();
        }
        public IList<User> GetUserListA(int number)
        {
            return _Dal.GetUserListA(number);
        }

        public IList<User> GetUserListByRole(int roleId)
        {
            return _Dal.GetUserListByRole(roleId);
        }
        public IList<User> GetUserByRoleId(int roleid)
        {
            return _Dal.GetUserByRoleId(roleid);
        }

        public long GetUserCount()
        {
            return _Dal.GetUserCount();
        }
        public bool ChangeUserPwd(User user)
        {
            return _Dal.ChangeUserPwd(user);
        }
        public bool DeleteUser(int userid)
        {
     
                return _Dal.Delete(userid);
            
        }
        public IList<User> GetUserByRole(string rolename)
        {
            return _Dal.GetUserByRole(rolename);
        }
    }
}
