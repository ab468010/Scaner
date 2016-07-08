using System;
using Model;
using Npgsql;
using SqlHelper;
using System.Data;
namespace DataAccess
{
    public class ContainerWarningAccess:IDataAccess.IContainerWarningAccess
    {
        public int GetSmallContaierWarning(int id)
        {
            string st = "select smallcontainer from dbo.containerwarning where containerid=@id";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id",id)
            };
            
            int co = Convert.ToInt32(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st,par));
            return co;
        }
        public  int   GetBigContainerWarning(int id)
        {
            string st = "select bigcontainer from dbo.containerwarning where containerid = @id";
            NpgsqlParameter[] par = new NpgsqlParameter[]
         {
                new NpgsqlParameter("@id",id)
         };
            int co=Convert.ToInt32(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st,par));
            return co;
        }
        public long UseSmallContainer()
        {
            string st = @"select count(1) from dbo.container where right(size,1)='小' and containerid in
                              (select distinct containerid from dbo.sample where containerid is not null)";

            long co = (long)NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st);
            return co;
        }
        public long UseBigContainer()
        {
            string st = @"select count(1) from dbo.container where right(size,1)='大' and containerid in
                              (select distinct containerid from dbo.sample where containerid is not null)";
          
           long  co = (long) NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st);
            return co;
        }
       public bool Create(ContainerWarning model)
        {
            return true;
        }
        public bool Delete(int id)
        {
            return true;
        }
       public bool Disable(int id)
        {
            return true;
        }
         public bool Update(ContainerWarning containerwarning)
        {
            string st = " update dbo.containerwarning set bigcontainer=@bigcontainer,smallcontainer=@smallcontainer where containerid=@id";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@bigcontainer",containerwarning.BigContainer),
                
                new NpgsqlParameter("@smallcontainer",containerwarning.SmallContainer),
              
                new NpgsqlParameter("@id",containerwarning.ContainerId),

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
        public ContainerWarning GetModel(int id)
        {
            ContainerWarning con = new ContainerWarning();
            string st = "select containerid,bigcontainer,smallcontainer from dbo.containerwarning where containerid=@id";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id",id)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
           
                    con.BigContainer = Convert.ToInt32(rdr["bigcontainer"]);
                    con.SmallContainer = Convert.ToInt32(rdr["smallcontainer"]);
         
                    con.ContainerId = Convert.ToInt32(rdr["containerid"]);
                }
            }
            return con;
        }
    }
}
