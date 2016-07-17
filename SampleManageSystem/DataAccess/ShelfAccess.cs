using System;
using System.Collections.Generic;
using SqlHelper;
using Model;
using Npgsql;
using System.Data;
namespace DataAccess
{
    public  class ShelfAccess:IDataAccess.IShelfAccess
    {
        public bool ExsitsShelfCode(Shelf shelf)
        {
            string st = "select count(1) from dbo.shelf where shelfcode=@shelfcode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@shelfcode",shelf.ShelfCode)
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
        public IList<Shelf> GetShelfList()
        {
            IList<Shelf> shelfList = new List<Shelf>();
            string sqlStr = "select shelfid,name,shelfcode,description,createdby,createdon,modifiedby,modifiedon from dbo.shelf";
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (drt.Read())
                {
                    Shelf shelf = new Shelf();
                    shelf.ShelfId = Convert.ToInt32(drt["shelfid"]);
                    shelf.Name = drt["name"].ToString();
                    shelf.ShelfCode = drt["shelfcode"].ToString();
                    shelf.Description = drt["description"].ToString();
                    shelf.CreatedBy = Convert.ToInt32(drt["createdby"]);
                    shelf.CreatedOn = Convert.ToDateTime(drt["createdon"]);
                    shelf.ModifiedBy = Convert.ToInt32(drt["modifiedby"]);
                    shelf.ModifiedOn = Convert.ToDateTime(drt["modifiedon"]);
                }
            }
            return shelfList;
        }
        public long GetShelfCount()
        {
            string st = "select count(1) count from dbo.shelf";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public Shelf GetModel(int shelfid)
        {
            Shelf sh = null;
            string st = "select shelfid,name,shelfcode,description from dbo.shelf where shelfid=@shelfid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@shelfid",shelfid)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    sh = new Shelf(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3));
                }
            }
            return sh;
        }
        public IList<Shelf> GetShelf(int number)
        {
            IList<Shelf> sh = new List<Shelf>();
            string st = "select shelfid,name,shelfcode,description from dbo.shelf order by createdon desc limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (drt.Read())
                {
                    sh.Add(new Shelf(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3)));
                }
            }
            return sh;
        }
        public bool Create(Shelf shelf)
        {
           string st = "insert into dbo.shelf (name,shelfcode,description,createdby,createdon,modifiedby,modifiedon) values(@name,@shelfcode,@description,@createdby,now(),@modifiedby,now())";
            NpgsqlParameter[] par = new NpgsqlParameter[] 
            {
                new NpgsqlParameter("@name",shelf.Name),
                new NpgsqlParameter("@shelfcode",shelf.ShelfCode),
                new NpgsqlParameter("@description",shelf.Description),
                new NpgsqlParameter("@createdby",shelf.CreatedBy),
                new NpgsqlParameter("@modifiedby",shelf.CreatedBy)
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
        public bool Disable(int id)
        {
            return true;
        }
        public bool Update(Shelf shelf)
        {
            string st = "update dbo.shelf set name=@name,shelfcode=@shelfcode,description=@description,modifiedby=@modifiedby,modifiedon=now() where shelfid=@shelfid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",shelf.Name),
                new NpgsqlParameter("@shelfcode",shelf.ShelfCode),
                new NpgsqlParameter("@description",shelf.Description),
                new NpgsqlParameter("@shelfid",shelf.ShelfId),
                new NpgsqlParameter("@modifiedby",shelf.ModifiedBy)
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
  
        public bool Delete(int shelfid)
        {
            string st = "delete from dbo.shelf where shelfid=@shelfid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@shelfid",shelfid)
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
    }
}
