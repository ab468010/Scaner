using System.Collections.Generic;
using Npgsql;
using Model;
using System.Data;
using SqlHelper;
namespace DataAccess
{
    public  class CustomerAccess:IDataAccess.ICustomerAccess
    {
        public bool DeleteCus(int customerid)
        {
            string st = "delete from dbo.customer where customerid=@customerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@customerid",customerid)
            };
            NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, par);
            return true;
        }
        public bool SelectContact(int customerid)
        {
            string st = "select Count(1) from dbo.contact where customerid=@customerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@customerid",customerid)
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
        public IList<Customer> ScustomerId()
        {
            IList<Customer> cus = new List<Customer>();
            string st = "select customerid,name,description from dbo.customer";
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                while (drt.Read())
                {
                    cus.Add(new Customer(drt.GetInt32(0),drt.GetString(1),drt.IsDBNull(2)?"": drt.GetString(2)));
                }
            }
            return cus;
        }
        public Customer GetModel(int customerid)
        {
            Customer cus = null;
            string st = @"select customerid,name,description,statecode from dbo.customer where customerid=@customerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@customerid",customerid)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    cus=new Customer(drt.GetInt32(0),drt.GetString(1),drt.IsDBNull(2)?"":drt.GetString(2),drt.GetInt32(3));
                }
            }
            return cus;
        }
        public IList<Customer> GoPage(int number)
        {
            IList<Customer> cus = new List<Customer>();
            string st = "select customerid, name, description,statecode from dbo.customer order by createdon desc limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    cus.Add(new Customer(drt.GetInt32(0), drt.GetString(1), drt.IsDBNull(2)?"":drt.GetString(2), drt.GetInt32(3)));
                }

            }
            return cus;
        }


        public Customer Scount()
        {
            Customer cus = null;
            string st = "select count(customerid) count from dbo.customer";
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                while (drt.Read())
                {
                    cus = new Customer(drt.GetInt64(0));
                }
            }
            return cus;
        }
 
   
        
        public IList<Customer> Selectcustomer()
        {
            IList<Customer> user = new List<Customer>();
            string st = "select customerid, name, description,statecode from dbo.customer order by createdon desc  limit 10";
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                while (drt.Read())
                {
                    user.Add(new Customer(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetInt32(3)));
                }
            }
            return user;
        }
        public bool Delete(int customerid)
        {

            return true;
        }
        public bool Disable(int id)
        {
            return false;
        }
        public bool Update(Customer customer)
        {
            string st = "update dbo.customer set name=@name,description=@description where customerid=@customerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",customer.Name),
                new NpgsqlParameter("@description",customer.Description),
                new NpgsqlParameter("@customerid",customer.Customerid)
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
        public bool InCustomer(Customer customer)
        {
          
            string st = "insert into dbo.customer (name,description,statecode,createdon) values(@name,@description,@statecode,now())";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",customer.Name),
                new NpgsqlParameter("@description",customer.Description),
                new NpgsqlParameter("@statecode",customer.StateCode)
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
   
        public bool Create(Customer model)
        {
            return true;
        }
    }
}
