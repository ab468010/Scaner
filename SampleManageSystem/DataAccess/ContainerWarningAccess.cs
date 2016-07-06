using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Npgsql;
using SqlHelper;
using System.Data;
namespace DataAccess
{
    public class ContainerWarningAccess:IDataAccess.IContainerWarningAccess
    {
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
            string st = " update dbo.containerwarning set bigcontainer=@bigcontainer,bigmessage=@bigmessage,smallcontainer=@smallcontainer,smallmessage=@smallmessage where containerid=@id";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@bigcontainer",containerwarning.BigContainer),
                new NpgsqlParameter("@bigmessage",containerwarning.BigMessage),
                new NpgsqlParameter("@smallcontainer",containerwarning.SmallContainer),
                new NpgsqlParameter("@smallmessage",containerwarning.SmallMessage),
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
            string st = "select containerid,bigcontainer,bigmessage,smallcontainer,smallmessage from dbo.containerwarning where containerid=@id";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@id",id)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    con.BigMessage = rdr["bigcontainer"].ToString();
                    con.BigContainer = Convert.ToInt32(rdr["bigcontainer"]);
                    con.SmallContainer = Convert.ToInt32(rdr["smallcontainer"]);
                    con.SmallMessage = rdr["smallmessage"].ToString();
                    con.ContainerId = Convert.ToInt32(rdr["containerid"]);
                }
            }
            return con;
        }
    }
}
