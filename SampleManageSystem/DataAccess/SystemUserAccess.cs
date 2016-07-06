using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Model;
using SqlHelper;
using Npgsql;
namespace DataAccess
{
    public class SystemUserAccess:IDataAccess.ISystemUserAccess
    { 
        public SystemUser GetUser(int userId)
        {
            SystemUser us = null;
            string st = "select systemuserid, name,username,password,statecode from dbo.systemuser where systemuserid=@systemuserid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",userId)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    us = new SystemUser(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3), drt.GetInt32(4));
                }
            }
            return us;
        }
        public bool CreateUser(SystemUser user)
        {
            string st= "insert into dbo.systemuser (name,username,password,statecode) values(@name,@username,@password,@statecode)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",user.Name),
                new NpgsqlParameter("@username",user.UserName),
                new NpgsqlParameter("@password",user.Password),
                new NpgsqlParameter("@statecode",1)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      public SystemUser GetSystemUser(SystemUser user)
        {
            SystemUser us = null;
            string st = "select systemuserid,name,username,password,statecode from dbo.systemuser where username=@username and password=@password";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@username",user.UserName),
                new NpgsqlParameter("@password",user.Password)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    us = new SystemUser(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3), drt.GetInt32(4));
                }
            }
            return us;
        }
        public IList<SystemUser> SelectUser()
        {
            IList<SystemUser> user = new List<SystemUser>();
            string st = "select systemuserid,name,username,password,statecode from dbo.systemuser";
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                 while (drt.Read())
               {
                    user.Add(new SystemUser(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3), drt.GetInt32(4)));
               }
            }
            return user;
        }
        public bool Create(SystemUser user)
        {
 
                return false;
        }
        public bool Disable(int id)
        {
            return true;
        }
        public bool Update(Model.SystemUser user)
        {
            string st = "update dbo.systemuser set name=@name,statecode=@statecode where systemuserid=@systemuserid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",user.Name),
         
                new NpgsqlParameter("@statecode",user.StateCode),
                new NpgsqlParameter("@systemuserid",user.SystemUserId)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public SystemUser GetModel(int id)
        {
            SystemUser user = null;
            return user;
        }
          public  bool Delete(int userId)
        {
            string st = "delete from dbo.systemuser where systemuserid=@systemuserid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",userId)
            };
           if(NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
