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
    public class UserAccess:IDataAccess.IUserAccess
    {
        public IList<User> GetUserByRole(string rolename)
        {
            IList<User> userlist = new List<User>();
            string st = "select systemuserid ,name  from dbo.systemuser where exists (select 1 from dbo.role where name=@name and systemuser.roleid = role.roleid LIMIT 1 ) ";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",rolename)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    User user = new User();
                    user.SystemUserId = Convert.ToInt32(rdr["systemuserid"]);
                    user.Name = rdr["name"].ToString();
                    userlist.Add(user);
                }
            }
            return userlist;
        }
        public bool ChangeUserPwd(User user)
        {
            string st = "update dbo.systemuser set password=@password where username=@username";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@password",user.Password),
                new NpgsqlParameter("@username",user.Username)
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
        public long GetUserCount()
        {
            string st = "select count(1) count from dbo.systemuser";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public IList<User> GetUserByRoleId(int roleid)
        {
            
            IList<User> user = new List<User>();
            string st = "select Systemuserid,username,name,password,statecode,roleid from dbo.systemuser where roleid=@roleid and statecode=@statecode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleid),
                new NpgsqlParameter("@statecode",1)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    user.Add(new User(drt.GetInt32(0),drt.GetString(1),drt.GetString(2),drt.GetString(3),drt.GetInt32(4),drt.GetInt32(5)));
                }
            }
            return user;
        }
        public bool Exists(User user)
        {
            string st = "select count(1) from dbo.systemuser where username=@username";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("username",user.Username)
            };
            if ((long)(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int userid)
        {
            string sqlStr = @"Delete from dbo.SystemUser
                                Where SystemUserId = @SystemUserId";
            NpgsqlParameter commandParameters = new NpgsqlParameter("@SystemUserId", userid);


            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters) > 0)
                return true;
            else
                return false;
        }

        public User GetModel(int userId)
        {
            User user = null;
            string sqlStr = @"Select SystemUserId,s.Name sname,Username,statecode,userCode,email,s.description,s.RoleId,ro.name roleidname from
                                 dbo.SystemUser s inner join dbo.role ro on s.roleid=ro.roleid and SystemUserId = @SystemUserId";
            NpgsqlParameter commandParameters = new NpgsqlParameter("@SystemUserId",userId);

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters))
            {
                if (rdr.Read())
                    user = new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2),rdr.GetInt32(3) ,rdr.IsDBNull(4)?"": rdr.GetString(4),rdr.IsDBNull(5)?"": rdr.GetString(5),rdr.IsDBNull(6)?"": rdr.GetString(6),rdr.GetInt32(7),rdr.GetString(8));
                else
                    user = null;
            }

            return user;
        }

        public User GetUser(string username, string password)
        {
            string sqlStr = @"Select SystemUserId,Name,Username,Password,StateCode,RoleId from dbo.SystemUser 
                              Where Username = @Username And Password = @Password";

            User user = null;

            NpgsqlParameter[] commandParameters = new NpgsqlParameter[] 
            {
                new NpgsqlParameter("@Username",username),
                new NpgsqlParameter("@Password",password)
            };

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters))
            {
                if (rdr.Read())
                    user = new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3),rdr.GetInt32(4),rdr.GetInt32(5));
                else
                    user = new User();
            }
            return user;
        }
        public IList<User> GetUserList()
        {
            IList<User> userList = new List<User>();
            string sqlStr = @"Select SystemUserId,sy.Name,Username,email,sy.roleid,ro.name roleidname,usercode,password from dbo.SystemUser sy left join dbo.role ro
                              on  sy.roleid=ro.roleid and statecode=1 ";


            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    User user = new User();

                    user.Username = rdr["Username"].ToString();
                    user.Password = rdr["password"].ToString();
                    user.SystemUserId = Convert.ToInt32(rdr["SystemUserId"]);
                    user.Name = rdr["Name"].ToString();
                    user.RoleId = Convert.ToInt32(rdr["roleid"]);
                    user.RoleIdName = rdr["roleidname"] == DBNull.Value ? "" : rdr["roleidname"].ToString();
                    user.UserCode = rdr["usercode"] == DBNull.Value ? "" : rdr["usercode"].ToString();
                    user.Email = rdr["email"] == DBNull.Value ? "" : rdr["email"].ToString();
                    userList.Add(user);
                }
            }
            return userList;
        }

        public IList<User> GetUserListA(int number)
        {
            IList<User> userList = new List<User>();
            string sqlStr = @"Select SystemUserId,sy.Name,Username,email,ro.name roleidname,usercode from dbo.SystemUser sy left join dbo.role ro
                              on  sy.roleid=ro.roleid and statecode=1 order by sy.createdon desc limit 10 offset @number";
           
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    userList.Add(new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2),rdr.IsDBNull(3)?" ": rdr.GetString(3),rdr.GetString(4), rdr.IsDBNull(5) ? "" : rdr.GetString(5)));
                }
            }
            return userList;
        }

        public IList<User> GetUserListByRole(int RoleId)
        {
            string sqlStr = @"Select systemuser.SystemUserId,systemuser.Name,systemuser.Username,systemuser.Password,systemuser.StateCode,systemuser.RoleId,role.Name RoleIdName from dbo.SystemUser systemuser
                                Inner Join dbo.Role role On systemuser.RoleId = role.RoleId
                              Where systemuser.StateCode = 1 And systemuser.RoleId = @RoleId";



            NpgsqlParameter[] commandParameters = new NpgsqlParameter[] 
            {
                new NpgsqlParameter("@RoleId",RoleId),
            };

            IList<User> userList = new List<User>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,commandParameters))
            {
                while (rdr.Read())
                {
                    userList.Add(new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(4),rdr.GetInt32(5),rdr.GetString(6)));
                }
            }
            return userList;
        }
        public bool Create(User user)
        {
            //if (Authorization)
            string sqlStr = @"Insert Into dbo.SystemUser(Name,Username,Password,StateCode,RoleId,UserCode,Email,Description,createdby,createdon) 
                                Values(@Name,@Username,@Password,@StateCode,@RoleId,@UserCode,@Email,@Description,@createdby,now())";

            NpgsqlParameter[] commandParameters = new NpgsqlParameter[] 
            {
                new NpgsqlParameter("@name",user.Name),
                new NpgsqlParameter("@Username",user.Username),
                new NpgsqlParameter("@Password",user.Password),
                new NpgsqlParameter("@StateCode",1),
                new NpgsqlParameter("@RoleId",user.RoleId),
                new NpgsqlParameter("@UserCode",user.UserCode),
                new NpgsqlParameter("@Email",user.Email),
                new NpgsqlParameter("@Description",user.Description),
                new NpgsqlParameter("@createdby",user.CreatedBy)
            };

            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters) > 0)
                return true;
            else
                return false;
        }
        public bool Disable(int id)
        {
            return true;
        }
        public bool Update(User user)
        {
            string st = "update dbo.systemuser set name=@name,usercode=@usercode,roleid=@roleid,email=@email,description=@description where systemuserid=@systemuserid";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",user.Name),
                new NpgsqlParameter("@RoleId",user.RoleId),
                new NpgsqlParameter("@UserCode",user.UserCode),
                new NpgsqlParameter("@Email",user.Email),
                new NpgsqlParameter("@Description",user.Description),
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
    }
}
