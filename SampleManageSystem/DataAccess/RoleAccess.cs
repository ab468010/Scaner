using System.Collections.Generic;
using SqlHelper;
using Model;
using Npgsql;
using System.Data;
using System;

namespace DataAccess
{
    public  class RoleAccess:IDataAccess.IRoleAccess
    {
        public int GetRoleType(int roleid)
        {
            string st = "select roletype from dbo.role where roleid=@roleid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleid)
            };
            int count = Convert.ToInt32(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st,par));
            return count;
        }
        public long GetRoleCount()
        {
            string st = "select count(1) count from dbo.role";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public bool ExistUser(int roleid)
        {
            string st = "select count(1) from dbo.systemuser where roleid=@roleid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleid)
            };
            if ((long)NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IList<Role> GetRoleList()
        {
            IList<Role> ro = new List<Role>();
            string st = "select roleid,name,description from dbo.role ";
         
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                while (drt.Read())
                {
                    ro.Add(new Role(drt.GetInt32(0), drt.GetString(1), drt.IsDBNull(2) ? "" : drt.GetString(2)));
                }
            }
            return ro;
        }
        public IList<Role> GetPageRoleList(int number)
        {
            IList<Role> ro = new List<Role>();
            string st = "select roleid,name,description from dbo.role limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (drt.Read())
                {
                    ro.Add(new Role(drt.GetInt32(0), drt.GetString(1),drt.IsDBNull(2)?"":drt.GetString(2)));
                }
            }
            return ro;
        }
        public bool Create(Role role)
        {
            string st = "insert into dbo.role (name,description,roletype) values(@name,@description,@roletype)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",role.RoleName),
                new NpgsqlParameter("@description",role.Description),
                new NpgsqlParameter("@roletype",2)
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
        public bool Disable(int id)
        {
            return true;
        }
        public bool Update(Role role)
        {
            string st = "update dbo.role set name=@name,description=@description where roleid=@roleid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",role.RoleName),
                new NpgsqlParameter("@description",role.Description),
                new NpgsqlParameter("@roleid",role.RoleId)
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
        public bool Delete(int roleid)
        {
            string st = "delete from dbo.role where roleid=@roleid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleid)
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
        public Role GetModel(int roleid)
        {
            Role ro = null;
            string st = "select roleid,name,description from dbo.role where roleid=@roleid";
            NpgsqlParameter[] pa = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleid)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, pa))
            {
                while (drt.Read())
                {
                    ro = new Role(drt.GetInt32(0), drt.GetString(1), drt.IsDBNull(2) ? "" : drt.GetString(2));
                }
                
            }
            return ro;
        }
        public bool DeleteRolePrivilege(int roleid)
        {
            string st = "delete from dbo.roleprivilege where roleid=@roleid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@roleid",roleid)
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
