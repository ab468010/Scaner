using System.Collections.Generic;
using Model;
namespace IDataAccess
{
    public interface ISystemUserAccess:IBaseAccess<Model.SystemUser>
    {
        IList<SystemUser> SelectUser();
        SystemUser GetSystemUser(SystemUser user);
        bool CreateUser(SystemUser userId);
        SystemUser GetUser(int userId);
  
    }
}
