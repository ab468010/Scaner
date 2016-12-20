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
    public class TaskContainerAccess : IDataAccess.ITaskContainerAccess
    {
        public bool CreateTaskContainer(int taskid,int containerid)
        {
            string st = "insert into dbo.taskcontainer (taskid,containerid) values(@taskid,@containerid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskid",taskid),
                new NpgsqlParameter("@containerid",containerid)
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

        public IList<TaskContainer> GetTaskContainerList()
        {
            string sqlStr = "Select taskcontainerid,taskid,containerid from dbo.taskcontainer";

            IList<TaskContainer> taskContainerList = new List<TaskContainer>();
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (drt.Read())
                {
                    TaskContainer taskContainer = new TaskContainer();
                    taskContainer.TaskId = Convert.ToInt32(drt["taskid"]);
                    taskContainer.ContainerId = Convert.ToInt32(drt["containerid"]);
                    taskContainerList.Add(taskContainer);
                }
            }
            return taskContainerList;
        }
        public bool Create(TaskContainer model)
        {
            string st = "insert into dbo.taskcontainer (taskid,containerid) values(@taskid,@containerid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskid",model.TaskId),
                new NpgsqlParameter("@containerid",model.ContainerId)
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
         public bool Delete(int containerId)
        {
            string st = "delete from dbo.taskcontainer where containerid=@containerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@containerid",containerId)
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
      public   bool Disable(int id)
        {
            return true;
        }

        public bool Update(TaskContainer model)
        {
            return true;
        }

       public TaskContainer GetModel(int id)
        {
            TaskContainer ta = null;
            return ta;
        }

    }
}
