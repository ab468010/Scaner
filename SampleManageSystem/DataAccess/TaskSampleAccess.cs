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
      public class TaskSampleAccess:IDataAccess.ITaskSampleAccess
    {
        public bool CreateTaskSample(int taskid,int sampleid)
        {
            string st = "insert into dbo.tasksample (taskid,sampleid) values(@taskid,@sampleid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskid",taskid),
                new NpgsqlParameter("@sampleid",sampleid)
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

        public IList<TaskSample> GetTaskSampleList()
        {
            string sqlStr = "Select tasksampleid,taskid,sampleid from dbo.tasksample";
            
            IList<TaskSample> taskSampleList = new List<TaskSample>();
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (drt.Read())
                {
                    TaskSample taskSample = new TaskSample();
                    taskSample.TaskId = Convert.ToInt32(drt["taskid"]);
                    taskSample.SampleId = Convert.ToInt32(drt["sampleid"]);
                    taskSampleList.Add(taskSample);
                }
            }
            return taskSampleList;
        }
       public  bool Create(TaskSample model)
        {
            return true;

        }
         public bool Delete(int sampleId)
        {
            string st = "delete from dbo.tasksample where sampleid=@sampleid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@sampleid",sampleId)
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
       public bool Update(TaskSample model)
        {
            return true;
        }
          public  TaskSample GetModel(int id)
        {
            TaskSample ta = null;
            return ta;
        }

    }
}
