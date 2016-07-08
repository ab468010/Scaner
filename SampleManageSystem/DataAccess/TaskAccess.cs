using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using SqlHelper;
using Npgsql;
using System.Data;
namespace DataAccess
{
     public  class TaskAccess:IDataAccess.ITaskAccess
    {
        public IList<Task> SelectTaskByTseterId(int number, int systemuserId)
        {
            IList<Task> ta = new List<Task>();
            string st = @"select taskid, ta.name taskname,pr.name projectname,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend,pr.statuscode
                from (dbo.task ta left join dbo.project pr on ta.projectid=pr.projectid) left join dbo.room ro on ta.roomid=ro.roomid 
                 where tester1=@systemuserid or tester2=@systemuserid order by ta.createdon desc limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number),
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    ta.Add(new Task(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3), drt.GetDateTime(4), drt.GetDateTime(5), drt.GetDateTime(6), drt.GetDateTime(7), drt.GetInt32(8)));
                }
            }
            return ta;
        }
        public IList<Task> SelectTaskByEngineerId(int number, int systemuserId)
        {
            IList<Task> ta = new List<Task>();
            string st = @"select taskid, ta.name taskname,pr.name projectname,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend,pr.statuscode
                from (dbo.task ta left join dbo.project pr on ta.projectid=pr.projectid) left join dbo.room ro on ta.roomid=ro.roomid 
                 where ta.projectid in(select projectid from dbo.project where engineerid=@systemuserid) order by ta.createdon desc limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number),
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            using (NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (drt.Read())
                {
                    ta.Add(new Task(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3), drt.GetDateTime(4), drt.GetDateTime(5), drt.GetDateTime(6), drt.GetDateTime(7), drt.GetInt32(8)));
                }
            }
            return ta;
        }
    
        public long GetGoingProjectCount()
        {
            string st = @"select count(1) from dbo.project
                          where  statuscode=2";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public long GetFinishProjectCount()
        {
            string st = @"select count(1) from dbo.project where statuscode>2";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public long GetDelayProjectCount()
        {
            string st = @"select count(distinct projectid) from dbo.task
                          where now() > estimatedend  and actualend is null and projectid is not null";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public long GetDelayTaskCount()
        {
            string st = "select count(1) from dbo.task where now()>estimatedend and actualend is null";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public IList<Task> GetTaskList()
        {
            IList<Task> tasklist = new List<Task>();
            string sqlStr = @"select taskid,ta.name,COALESCE(ta.projectid,-1) projectid,pr.name projectname,COALESCE(ta.roomid,-1) roomid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend
                                from dbo.task ta
                                left join dbo.project pr on ta.projectid = pr.projectid
                                left join dbo.room ro on ta.roomid = ro.roomid";

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["name"].ToString();
                    task.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    task.ProjectName = rdr["projectname"].ToString();
                    task.RoomName = rdr["roomname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(rdr["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(rdr["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(rdr["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(rdr["actualend"]);
                    tasklist.Add(task);
                }
            }
            return tasklist;
        }

        public IList<Task> GetTaskListByProjectId(int projectId)
        {
            IList<Task> tasklist =new List<Task>();
            string st = @"select taskid,ta.name taskname,COALESCE(ta.projectid,-1) projectid,pr.name projectname,COALESCE(ta.roomid,-1) roomid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend
                                from dbo.task ta
                                left join dbo.project pr on ta.projectid = pr.projectid
                                left join dbo.room ro on ta.roomid = ro.roomid
                                     where  ta.projectid=@projectid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projectid",projectId)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["taskname"].ToString();
                    task.ProjectName = rdr["projectname"].ToString();
                    task.RoomName = rdr["roomname"].ToString();
                    task.EstimatedStart =Convert.ToDateTime(rdr["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(rdr["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(rdr["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(rdr["actualend"]);
                    tasklist.Add(task);
                }
            }
            return tasklist;
        }
        public bool ExistsSample(int taskid)
        {
            string st = "select count(1) from dbo.tasksample where taskid=@taskid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskid",taskid)
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
        public long GetTaskCount()
        {
            string st = "select count(1) count from dbo.task";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public Task GetModel(int taskid)
        {
            Task task = null;
            string st = @"select taskid,ta.name taskname,ta.roomid roomid,pr.name projectname,ta.projectid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,
                        COALESCE(actualend,'1900-1-1') actualend,ta.description description 
                        from dbo.task ta 
                        left join dbo.project pr on ta.projectid=pr.projectid
                        left join dbo.room ro on ta.roomid=ro.roomid
                        where taskid=@taskid";
            NpgsqlParameter[] pa = new NpgsqlParameter[]
            {
              new NpgsqlParameter("@taskid",taskid)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, pa))
            {
                while (rdr.Read())
                {
                    task = new Task();
                    task.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["taskname"].ToString();
                    task.RoomId = Convert.ToInt32(rdr["roomid"]);
                    task.ProjectName = rdr["projectname"].ToString();
                    task.RoomName = rdr["roomname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(rdr["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(rdr["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(rdr["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(rdr["actualend"]);
                    task.Description = rdr["description"].ToString();
                }
            }
            return task;
        }
        public IList<Task> SelectTask(int number)
        {
            IList<Task> ta = new List<Task>();
            string st = @"select taskid, ta.name taskname,pr.name projectname,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend,pr.statuscode
                from (dbo.task ta inner join dbo.project pr on ta.projectid=pr.projectid) inner join dbo.room ro on ta.roomid=ro.roomid order by ta.createdon desc limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (drt.Read())
                {
                    ta.Add(new Task(drt.GetInt32(0), drt.GetString(1), drt.GetString(2), drt.GetString(3), drt.GetDateTime(4), drt.GetDateTime(5), drt.GetDateTime(6), drt.GetDateTime(7),drt.GetInt32(8)));
                }
            }
            return ta;
        }
         public bool Create(Task task)
        {
            string st = "insert into dbo.task (name,projectid,roomid,estimatedstart,estimatedend,description，tester1,tester2,createdby,createdon) values(@name,@projectid,@roomid,@estimatedstart,@estimatedend,@description,@tester1,@tester2,@createdby,now())";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",task.Name),
                new NpgsqlParameter("@projectid",task.ProjectId),
                new NpgsqlParameter("@roomid",task.RoomId),
                new NpgsqlParameter("@estimatedstart",task.EstimatedStart),
                new NpgsqlParameter("@estimatedend",task.EstimatedEnd),
                new NpgsqlParameter("@description",task.Description),
                new NpgsqlParameter("@tester1",task.Tester1),
                new NpgsqlParameter("@tester2",task.Tester2),
                new NpgsqlParameter("@createdby",task.CreatedBy)
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
        public   bool Delete(int taskid)
        {
            string st = "delete from dbo.task where taskid=@taskid";
            NpgsqlParameter[] pa = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskid",taskid)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, pa) > 0)
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
         public   bool Update(Task task)
        {
            string st = "update dbo.task set name=@name,estimatedstart=@estimatedstart,estimatedend=@estimatedend,description=@description where taskid=@taskid";
            NpgsqlParameter[] pa = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",task.Name),
                new NpgsqlParameter("@estimatedstart",task.EstimatedStart),
                new NpgsqlParameter("@estimatedend",task.EstimatedEnd),
                new NpgsqlParameter("@description",task.Description),
                new NpgsqlParameter("@taskid",task.TaskId)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, st, pa) > 0)
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
