using System;
using System.Collections.Generic;
using SqlHelper;
using Model;
using Npgsql;
using System.Data;
namespace DataAccess
{
    public class PrivilegeAccess:IDataAccess.IPrivilegeAccess
    {
        public IList<Privilege> GetRolePrivilegeList(int roleid)
        {
            IList<Privilege> privilegelist = new List<Privilege>();
            string st = @"select pr.privilegeid,pr.moduleid,pr.name,canread,cancreate,candelete,canwrite,canmanage,mo.name modulename,mo.tablename 
                       from dbo.privilege pr left join dbo.module mo on pr.moduleid=mo.moduleid
                       Left Join dbo.roleprivilege rp on pr.privilegeId = rp.privilegeId
                       Where rp.roleid = @roleid";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleid)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (rdr.Read())
                {
                    Privilege privilege = new Privilege();
                    privilege.PrivilegeId = Convert.ToInt32(rdr["privilegeid"]);
                    privilege.ModuleId = Convert.ToInt32(rdr["moduleid"]);
                    privilege.Name = rdr["name"].ToString();
                    privilege.CanRead = Convert.ToBoolean(rdr["canread"]);
                    privilege.CanCreate = Convert.ToBoolean(rdr["cancreate"]);
                    privilege.CanDelete = Convert.ToBoolean(rdr["candelete"]);
                    privilege.CanWrite = Convert.ToBoolean(rdr["canwrite"]);
                    privilege.CanManage = Convert.ToBoolean(rdr["canmanage"]);
                    privilege.ModuleName = rdr["modulename"].ToString();
                    privilege.Tablename = rdr["tablename"].ToString();
                    privilegelist.Add(privilege);
                }
            }
            return privilegelist;
        }
        public bool CreateRolePrivilege(int privilegeId, int roleId)
        {
            string st = "insert into dbo.roleprivilege (roleid,privilegeid) values(@roleid,@privilegeid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleId),
                new NpgsqlParameter("@privilegeid",privilegeId)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st,par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Privilege GetModel(int id)
        {
            Privilege privilege = null;
            string st= @"select privilegeid,pr.moduleid,pr.name,canread,cancreate,candelete,canwrite,canmanage,mo.name modulename 
                       from dbo.privilege pr inner join dbo.module mo on pr.moduleid=mo.moduleid and privilegeid=@privilegeid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@privilegeid",id)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (rdr.Read())
                {
                    privilege = new Privilege();
                    privilege.PrivilegeId = Convert.ToInt32(rdr["privilegeid"]);
                    privilege.ModuleId = Convert.ToInt32(rdr["moduleid"]);
                    privilege.Name = rdr["name"].ToString();
                    privilege.CanRead = Convert.ToBoolean(rdr["canread"]);
                    privilege.CanCreate = Convert.ToBoolean(rdr["cancreate"]);
                    privilege.CanDelete = Convert.ToBoolean(rdr["candelete"]);
                    privilege.CanWrite = Convert.ToBoolean(rdr["canwrite"]);
                    privilege.CanManage = Convert.ToBoolean(rdr["canmanage"]);
                    privilege.ModuleName = rdr["modulename"].ToString();

                }
            }
            return privilege;
        }
        public IList<Privilege> GetPrivilegeList()
        {
            IList<Privilege> privilegelist =new List<Privilege>();
            string st = @"select privilegeid,pr.moduleid,pr.name,canread,cancreate,candelete,canwrite,canmanage,mo.name modulename 
                       from dbo.privilege pr inner join dbo.module mo on pr.moduleid=mo.moduleid";
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                while (rdr.Read())
                {
                    Privilege privilege = new Privilege();
                    privilege.PrivilegeId = Convert.ToInt32(rdr["privilegeid"]);
                    privilege.ModuleId = Convert.ToInt32(rdr["moduleid"]);
                    privilege.Name = rdr["name"].ToString();
                    privilege.CanRead = Convert.ToBoolean(rdr["canread"]);
                    privilege.CanCreate = Convert.ToBoolean(rdr["cancreate"]);
                    privilege.CanDelete = Convert.ToBoolean(rdr["candelete"]);
                    privilege.CanWrite = Convert.ToBoolean(rdr["canwrite"]);
                    privilege.CanManage = Convert.ToBoolean(rdr["canmanage"]);
                    privilege.ModuleName = rdr["modulename"].ToString();
                    privilegelist.Add(privilege);
                }
            }
            return privilegelist;
        }
        public  bool Create(Privilege privilege)
        {
            string st = @"insert into dbo.privilege (name,moduleid,canread,cancreate,candelete,canwrite,canmanage) 
                        values(@name,@moduleid,@canread,@cancreate,@candelete,@canwrite,@canmanage)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",privilege.Name),
                new NpgsqlParameter("@canread",privilege.CanRead),
                new NpgsqlParameter("@cancreate",privilege.CanCreate),
                new NpgsqlParameter("@candelete",privilege.CanDelete),
                new NpgsqlParameter("@canwrite",privilege.CanWrite),
                new NpgsqlParameter("@canmanage",privilege.CanManage),
                new NpgsqlParameter("@moduleid",privilege.ModuleId)
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

        public int CreatePrivilege(Privilege privilege)
        {
            string st = @"insert into dbo.privilege (name,moduleid,canread,cancreate,candelete,canwrite,canmanage) 
                        values(@name,@moduleid,@canread,@cancreate,@candelete,@canwrite,@canmanage) returning privilegeid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",privilege.Name),
                new NpgsqlParameter("@canread",privilege.CanRead),
                new NpgsqlParameter("@cancreate",privilege.CanCreate),
                new NpgsqlParameter("@candelete",privilege.CanDelete),
                new NpgsqlParameter("@canwrite",privilege.CanWrite),
                new NpgsqlParameter("@canmanage",privilege.CanManage),
                new NpgsqlParameter("@moduleid",privilege.ModuleId)
            };
            int privilegeid = (int)NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par);

            return privilegeid;
      
        }
        public bool DeleteRolePrivilege(int privilegeid)
        {
            string st = "delete from dbo.roleprivilege where privilegeid=@privilegeid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@privilegeid",privilegeid)
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
        public  bool Delete(int privilegeid)
        {
            string st = "delete from dbo.privilege where privilegeid=@privilegeid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@privilegeid",privilegeid)
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
        public  bool Disable(int id)
        {
            return true;
        }
          public  bool Update(Privilege privilege)
        {
            string st = @"update dbo.privilege set name=@name,canread=@canread,cancreate=@cancreate, 
                           candelete=@candelete,canwrite=@canwrite,canmanage=@canmanage where privilegeid=@privilegeid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",privilege.Name),
                new NpgsqlParameter("@canread",privilege.CanRead),
                new NpgsqlParameter("@cancreate",privilege.CanCreate),
                new NpgsqlParameter("@candelete",privilege.CanDelete),
                new NpgsqlParameter("@canwrite",privilege.CanWrite),
                new NpgsqlParameter("@canmanage",privilege.CanManage),
                new NpgsqlParameter("@privilegeid",privilege.PrivilegeId)
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
