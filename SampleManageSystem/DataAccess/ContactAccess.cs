using Model;
using System;
using System.Collections.Generic;
using System.Data;
using SqlHelper;
using Npgsql;


namespace DataAccess
{
      public class ContactAccess:IDataAccess.IContactAccess
    {
        public long GetContactCount()
        {
            string st = "select count(1) count from dbo.contact";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public IList<Contact> Scontact(int customerid)
        {
            IList<Contact> con = new List<Contact>();
            string st = "select contactid,name, customerid from dbo.contact where customerid=@customerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@customerid",customerid)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    con.Add(new Contact(drt.GetInt32(0), drt.GetString(1), drt.GetInt32(2)));
                }
            }
            return con;
        }
        public bool UpdateContact(Contact contact)
        {
            string st = "update dbo.contact set name = @name ,description=@description where contactid = @contactid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@contactid",contact.Contactid),
                new NpgsqlParameter("@name",contact.Name),
                new NpgsqlParameter("@description",contact.Description)
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
        public IList<Contact> Selectcontact(int number)
        {
            IList<Contact> cont = new List<Contact>();
            string st = @"select co.contactid Contactid,co.name contactname,co.description description,cu.Name Customername  
                        from dbo.contact co left join dbo.customer cu on co.Customerid=cu.Customerid limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using (NpgsqlDataReader drt =NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString,CommandType.Text,st,par))
            {
                while (drt.Read())
                {
                    cont.Add( new Contact(drt.GetInt32(0), drt.GetString(1), drt.IsDBNull(2)?"":drt.GetString(2),drt.GetString(3)));
                }
            }
            return cont;
        }
        public bool Delete(int contactid)
        {
            string st = "delete from dbo.contact where contactid=@contactid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@contactid",contactid)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            else { return false; }
          
        }
        public Contact GetModel(int contactid)
        {
            Contact con = null;
            string st = @"select co.contactid contactid, co.name contactame,co.description Description,cu.name Customername from dbo.contact co left join dbo.customer cu 
                         on cu.customerid=co.customerid where co.contactid=@contactid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@contactid",contactid)
            };
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    con=new Contact(drt.GetInt32(0), drt.GetString(1),drt.IsDBNull(2)?"":drt.GetString(2),drt.GetString(3));
                }
            }
            return con;
        }
        public bool Incontact(Contact contact)
        {
            string st = "insert into dbo.contact(name,description,customerid) values(@name,@description,@customerid) ";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",contact.Name),
                new NpgsqlParameter("@customerid",contact.Customerid),
                new NpgsqlParameter("@description",contact.Description)
            };
            if(NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par) > 0)
            {
                return true;
            }
            return false;
        }
        public bool Disable(int id)
        {
            return false;
        }
        public bool Update(Model.Contact user)
        {
            return true;
        }

      

        public bool Create(Contact model)
        {
            return true;
        }
    }
}
