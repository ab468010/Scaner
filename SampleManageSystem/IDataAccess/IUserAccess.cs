using Model;
using System.Collections.Generic;

namespace IDataAccess
{
    public interface IUserAccess:IBaseAccess<Model.User>
    {
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool Exists(User user);


        /// <summary>
        /// 根据账户名密码获得用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User GetUser(string username, string password);

        /// <summary>
        /// 获得所有可用用户
        /// </summary>
        /// <returns></returns>
        IList<User> GetUserList();

        /// <summary>
        /// 获得指定角色用户
        /// </summary>
        /// <returns></returns>
        IList<User> GetUserListByRole(int RoleId);
        IList<User> GetUserByRoleId(int roleid);
        long GetUserCount();
        IList<User> GetUserList(int number);
        bool ChangeUserPwd(User user);
        IList<User> GetUserByRole(string rolename);
    }
}
