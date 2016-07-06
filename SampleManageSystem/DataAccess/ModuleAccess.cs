using System.Collections.Generic;
using Model;
using SqlHelper;
using Npgsql;
using System.Data;
using System;

namespace DataAccess
{
    public class ModuleAccess:IDataAccess.IModuleAccess
    {
       
        public IList<Module> GetModuleList()
        {
            IList<Module> moduleList = new List<Module>();
            string st = "select moduleid,name,tablename from dbo.module";
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                while (drt.Read())
                {
                    Module module = new Module();
                    module.ModuleId = Convert.ToInt32(drt["moduleid"]);
                    module.Name = drt["name"].ToString();
                    module.TableName = drt["tablename"].ToString();
                    moduleList.Add(module);
                }
            }
            return moduleList;
        }
        public bool Create(Module model)
        {
            return true;
        }
       public bool Delete(int id)
        {
            return true;
        }
      public  bool Disable(int id)
        {
            return true;
        }
       public bool Update(Module model)
        {
            return true;
        }
      public  Module GetModel(int id)
        {
            Module mo = null;
            return mo;
        }
    }
}
