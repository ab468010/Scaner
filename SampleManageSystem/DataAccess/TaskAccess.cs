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
        public IList<Task> GetTaskListByPId(int number, int systemuserId, int projectId)
        {
            IList<Task> tasklist = new List<Task>();
            string st = @"select taskid,ta.name taskname,COALESCE(ta.projectid,-1),ta.roomid,ta.projectid,ta.description,pr.name projectname,
                                 ta.modifiedon,tester1.name tester1idname,tester2.name tester2idname,ta.tester1,ta.tester2,
                                 COALESCE(ta.roomid,-1) roomid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend
                                 from dbo.task ta
                                 left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                                 left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
                                 left join dbo.project pr on ta.projectid = pr.projectid
                                 left join dbo.room ro on ta.roomid = ro.roomid
                                 where  ta.projectid=@projectid and (tester1=@systemuserid or tester2=@systemuserid) order by estimatedstart limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
          {
                new NpgsqlParameter("@projectid",projectId),
                new NpgsqlParameter("@systemuserid",systemuserId),
                new NpgsqlParameter("@number",number)
          };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["taskname"].ToString();
                    task.ProjectName = rdr["projectname"].ToString();
                    task.RoomName = rdr["roomname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(rdr["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(rdr["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(rdr["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(rdr["actualend"]);
                    task.Description = rdr["description"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.Tester1 = Convert.ToInt32(rdr["tester1"]);
                    task.Tester2 = Convert.ToInt32(rdr["tester2"]);
                    task.RoomId = Convert.ToInt32(rdr["roomid"]);
                    tasklist.Add(task);
                }
            }
            return tasklist;
        }
        public IList<Task> GetAllTaskListByPId(int number, int projectId)
        {
            IList<Task> tasklist = new List<Task>();
            string st = @"select taskid,ta.name taskname,COALESCE(ta.projectid,-1),ta.roomid,ta.projectid,ta.description,pr.name projectname,
                                 ta.modifiedon,tester1.name tester1idname,tester2.name tester2idname,ta.tester1,ta.tester2,
                                 COALESCE(ta.roomid,-1) roomid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend
                                 from dbo.task ta
                                 left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                                 left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
                                 left join dbo.project pr on ta.projectid = pr.projectid
                                 left join dbo.room ro on ta.roomid = ro.roomid
                                 where  ta.projectid=@projectid order by estimatedstart limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projectid",projectId),
                new NpgsqlParameter("@number",number)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["taskname"].ToString();
                    task.ProjectName = rdr["projectname"].ToString();
                    task.RoomName = rdr["roomname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(rdr["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(rdr["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(rdr["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(rdr["actualend"]);
                    task.Description = rdr["description"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.Tester1 = Convert.ToInt32(rdr["tester1"]);
                    task.Tester2 = Convert.ToInt32(rdr["tester2"]);
                    task.RoomId = Convert.ToInt32(rdr["roomid"]);
                    tasklist.Add(task);
                }
            }
            return tasklist;
        }

        public bool UpdateTaskActualStart(int taskid, DateTime startTime, int systemuserId)
        {
            string st = "update dbo.task set actualStart=@startTime ,modifiedby=@modifiedby where taskid=@taskid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@startTime",startTime),
                new NpgsqlParameter("@modifiedby",systemuserId),
                new NpgsqlParameter("@taskid",taskid)
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
        public bool UpdateTaskActualEnd(int taskid, DateTime endTime, int systemuserId)
        {
            string st = "update dbo.task set actualend=@endTime ,modifiedby=@modifiedby where taskid=@taskid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@endTime",endTime),
                new NpgsqlParameter("@modifiedby",systemuserId),
                new NpgsqlParameter("@taskid",taskid)
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
        public IList<Task> GetTaskListByProjectIdTesterId(int projectId, int systemuserId)
        {
            IList<Task> tasklist = new List<Task>();
            string st = @"select taskid,ta.name taskname,COALESCE(ta.projectid,-1),ta.roomid,ta.projectid,ta.description,pr.name projectname,
                                 ta.modifiedon,tester1.name tester1idname,tester2.name tester2idname,ta.tester1,ta.tester2,
                                 COALESCE(ta.roomid,-1) roomid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend
                                 from dbo.task ta
                                 left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                                 left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
                                 left join dbo.project pr on ta.projectid = pr.projectid
                                 left join dbo.room ro on ta.roomid = ro.roomid
                                 where  ta.projectid=@projectid and (tester1=@systemuserid or tester2=@systemuserid) order by estimatedstart";
            NpgsqlParameter[] par = new NpgsqlParameter[]
          {
                new NpgsqlParameter("@projectid",projectId),
                new NpgsqlParameter("@systemuserid",systemuserId)
          };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["taskname"].ToString();
                    task.ProjectName = rdr["projectname"].ToString();
                    task.RoomName = rdr["roomname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(rdr["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(rdr["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(rdr["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(rdr["actualend"]);
                    task.Description = rdr["description"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.Tester1 = Convert.ToInt32(rdr["tester1"]);
                    task.Tester2 = Convert.ToInt32(rdr["tester2"]);
                    task.RoomId = Convert.ToInt32(rdr["roomid"]);
                    tasklist.Add(task);
                }
            }
            return tasklist;
        }
        public long GetDelayTaskCountByEngineerId(int systemuserId)
        {
            string st = @"select count(1)  from dbo.task ta
                        left join dbo.project pr on ta.projectid = pr.projectid
                        where now() > estimatedend and actualend is null and pr.engineerid = @systemuserid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par));
            return count;
        }
        public long GetDelayTaskCountByTesterId(int systemuserId)
        {
            string st = @"select count(1) from dbo.task task
                          left join dbo.systemuser tester1 on task.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on task.tester2 = tester2.systemuserid
                          left join dbo.project project on project.projectid = task.projectid
                          where now() > estimatedend and actualend is null and(tester1 = @systemuserid or tester2 = @systemuserid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par));
            return count;
        }
        public long GetUTaskByTesterId(int systemuserId)
        {
            string st = @"select count(1) from dbo.task task
                          left join dbo.systemuser tester1 on task.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on task.tester2 = tester2.systemuserid
                            where  actualend is null and (tester1=@systemuserid or tester2=@systemuserid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par));
            return count;
        }
        public long GetUTaskByEngineerId(int systemuserId)
        {
            string st = @"select count(1)  from dbo.task ta
                        left join dbo.project pr on ta.projectid = pr.projectid
                        where actualend is null and pr.engineerid = @systemuserid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st,par));
            return count;
        }
        public long GetAllUTaskCount()
        {
            string st = "select count(1) from dbo.task where actualend is null";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public IList<Task> GetDelayTaskByEngineer(int systemuserId,int Page)
        {
            IList<Task> taskList = new List<Task>();
            string st = @"select taskid ,task.name,tester1.name tester1idname,tester2.name tester2idname,project.name projectidname from dbo.task task
                          left join dbo.systemuser tester1 on task.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on task.tester2 = tester2.systemuserid
                          left join dbo.project project on project.projectid = task.projectid
                          where now() > estimatedend and actualend is null and project.engineerid=@systemuserid limit 5 offset @page";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId),
                new NpgsqlParameter("@page",Page)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["name"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.ProjectName = rdr["projectidname"].ToString();
                    taskList.Add(task);
                   
                }
            }
            return taskList;
        }


        public IList<Task> GetDelayTaskByTester(int systemuserId,int Page)
        {
            IList<Task> taskList = new List<Task>();
            string st = @"select taskid ,task.name,tester1.name tester1idname,tester2.name tester2idname,project.name projectidname from dbo.task task
                          left join dbo.systemuser tester1 on task.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on task.tester2 = tester2.systemuserid
                          left join dbo.project project on project.projectid = task.projectid
                          where now() > estimatedend and actualend is null and (tester1=@systemuserid or tester2=@systemuserid) limit 5 offset @page";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId),
                new NpgsqlParameter("@page",Page)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["name"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.ProjectName = rdr["projectidname"].ToString();
                    taskList.Add(task);
                }
            }
            return taskList;
        }
        public IList<Task> GetAllDelayTask(int Page)
        {
            IList<Task> taskList = new List<Task>();
            string st = @"select taskid ,task.name,tester1.name tester1idname,tester2.name tester2idname,project.name projectidname from dbo.task task
                          left join dbo.systemuser tester1 on task.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on task.tester2 = tester2.systemuserid
                          left join dbo.project project on project.projectid = task.projectid
                          where now() > estimatedend and actualend is null limit 5 offset @page";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@page",Page)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (rdr.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(rdr["taskid"]);
                    task.Name = rdr["name"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.ProjectName = rdr["projectidname"].ToString();
                    taskList.Add(task);
                }
            }
            return taskList;
        }
        public IList<Task> SelectTaskByTseterId(int number, int systemuserId)
        {
            IList<Task> taskList = new List<Task>();
            string st = @"select taskid, ta.name taskname,pr.name projectname,ro.name roomname,ta.tester1,ta.tester2,ta.roomid roomid,tester1.name tester1idname,tester2.name tester2idname,
                          COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend,pr.statuscode pstatuscode
                          from dbo.task ta left join dbo.project pr on ta.projectid=pr.projectid
                          left join dbo.room ro on ta.roomid=ro.roomid 
                          left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
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
                    Task task =new Task();
                    task.TaskId = Convert.ToInt32(drt["taskid"]);
                    task.Name = drt["taskname"].ToString();
                    task.ProjectName = drt["projectname"].ToString();
                    task.RoomName = drt["roomname"].ToString();
                    task.Tester1IdName = drt["tester1idname"].ToString();
                    task.Tester2IdName = drt["tester2idname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(drt["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(drt["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(drt["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(drt["actualend"]);
                    task.ProjectStatusCode = Convert.ToInt32(drt["pstatuscode"]);
                    task.Tester1 = Convert.ToInt32(drt["tester1"]);
                    task.Tester2 = Convert.ToInt32(drt["tester2"]);
                    task.RoomId = Convert.ToInt32(drt["roomid"]);
                    taskList.Add(task);
                }
            }
            return taskList;
        }
        public IList<Task> SelectTaskByEngineerId(int number, int systemuserId)
        {
            IList<Task> taskList = new List<Task>();
            string st = @"select taskid, ta.name taskname,pr.name projectname,ro.name roomname,ta.tester1,ta.tester2,ta.roomid roomid,tester1.name tester1idname,tester2.name tester2idname,
                          COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend,pr.statuscode pstatuscode
                          from dbo.task ta left join dbo.project pr on ta.projectid=pr.projectid
                          left join dbo.room ro on ta.roomid=ro.roomid 
                          left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
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
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(drt["taskid"]);
                    task.Name = drt["taskname"].ToString();
                    task.ProjectName = drt["projectname"].ToString();
                    task.RoomName = drt["roomname"].ToString();
                    task.Tester1IdName = drt["tester1idname"].ToString();
                    task.Tester2IdName = drt["tester2idname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(drt["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(drt["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(drt["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(drt["actualend"]);
                    task.ProjectStatusCode = Convert.ToInt32(drt["pstatuscode"]);
                    task.Tester1 = Convert.ToInt32(drt["tester1"]);
                    task.Tester2 = Convert.ToInt32(drt["tester2"]);
                    task.RoomId = Convert.ToInt32(drt["roomid"]);
                    taskList.Add(task);
                }
            }
            return taskList;
        }
    
        public long GetGoingProjectCount()
        {
            string st = @"select count(1) from dbo.project
                          where  statuscode=2";
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
            string sqlStr = @"select taskid,ta.name,COALESCE(ta.projectid,-1) projectid,pr.name projectname,ta.description,ta.tester1,ta.tester2,ta.createdby,ta.createdon,ta.modifiedby,
                              ta.modifiedon,tester1.name tester1idname,tester2.name tester2idname,
                              COALESCE(ta.roomid,-1) roomid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend,
                               cname.name createdbyname,mname.name modifiedbyname
                                from dbo.task ta
                                left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                                left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
                                left join dbo.systemuser cname on ta.createdby = cname.systemuserid
                                left join dbo.systemuser mname on ta.modifiedby = mname.systemuserid
                                left join dbo.project pr on ta.projectid = pr.projectid
                                left join dbo.room ro on ta.roomid = ro.roomid where pr.statuscode<=3";

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
                    task.RoomId = Convert.ToInt32(rdr["roomid"]);
                    task.EstimatedStart = Convert.ToDateTime(rdr["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(rdr["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(rdr["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(rdr["actualend"]);
                    task.CreatedOn = Convert.ToDateTime(rdr["createdon"]);
                    task.CreatedBy = Convert.ToInt32(rdr["createdby"]);
                    task.ModifiedBy = Convert.ToInt32(rdr["modifiedby"]);
                    task.ModifiedOn = Convert.ToDateTime(rdr["modifiedon"]);
                    task.Description = rdr["description"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.Tester1 = Convert.ToInt32(rdr["tester1"]);
                    task.Tester2 = Convert.ToInt32(rdr["tester2"]);
                    task.CreatedByName = rdr["createdbyname"].ToString();
                    task.ModifiedByName = rdr["modifiedbyname"].ToString();
                    tasklist.Add(task);
                }
            }
            return tasklist;
        }

        public IList<Task> GetTaskListByProjectId(int projectId)
        {
            IList<Task> tasklist =new List<Task>();
            string st = @"select taskid,ta.name taskname,COALESCE(ta.projectid,-1),ta.roomid,ta.projectid,ta.description,pr.name projectname,
                                 ta.modifiedon,tester1.name tester1idname,tester2.name tester2idname,ta.tester1,ta.tester2,
                                 COALESCE(ta.roomid,-1) roomid,ro.name roomname,COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend
                                 from dbo.task ta
                                 left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                                 left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
                                 left join dbo.project pr on ta.projectid = pr.projectid
                                 left join dbo.room ro on ta.roomid = ro.roomid
                                 where  ta.projectid=@projectid order by estimatedstart";
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
                    task.Description = rdr["description"].ToString();
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                    task.Tester1 = Convert.ToInt32(rdr["tester1"]);
                    task.Tester2 = Convert.ToInt32(rdr["tester2"]);
                    task.RoomId = Convert.ToInt32(rdr["roomid"]);
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
            string st = @"select taskid,ta.name taskname,ta.roomid roomid,pr.name projectname,ta.projectid,ro.name roomname,
                          ta.tester1,ta.tester2,tester1.name tester1idname,tester2.name tester2idname, 
                        COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,
                        COALESCE(actualend,'1900-1-1') actualend,ta.description description 
                        from dbo.task ta 
                        left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                        left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
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
                    task.Tester1 = Convert.ToInt32(rdr["tester1"]);
                    task.Tester2 = Convert.ToInt32(rdr["tester2"]);
                    task.Tester1IdName = rdr["tester1idname"].ToString();
                    task.Tester2IdName = rdr["tester2idname"].ToString();
                }
            }
            return task;
        }
        public IList<Task> SelectTask(int number)
        {
            IList<Task> taskList = new List<Task>();
            string st = @"select taskid, ta.name taskname,pr.name projectname,ro.name roomname,ta.tester1,ta.tester2,ta.roomid roomid,tester1.name tester1idname,tester2.name tester2idname,
                          COALESCE(estimatedstart,'1900-1-1') estimatedstart,COALESCE(estimatedend,'1900-1-1') estimatedend,COALESCE(actualstart,'1900-1-1') actualstart,COALESCE(actualend,'1900-1-1') actualend,pr.statuscode pstatuscode
                          from dbo.task ta left join dbo.project pr on ta.projectid=pr.projectid 
                          left join dbo.room ro on ta.roomid=ro.roomid 
                          left join dbo.systemuser tester1 on ta.tester1 = tester1.systemuserid
                          left join dbo.systemuser tester2 on ta.tester2 = tester2.systemuserid
                          order by ta.createdon desc limit 10 offset @number";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@number",number)
            };
            using(NpgsqlDataReader drt = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (drt.Read())
                {
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(drt["taskid"]);
                    task.Name = drt["taskname"].ToString();
                    task.ProjectName = drt["projectname"].ToString();
                    task.RoomName = drt["roomname"].ToString();
                    task.Tester1IdName = drt["tester1idname"].ToString();
                    task.Tester2IdName = drt["tester2idname"].ToString();
                    task.EstimatedStart = Convert.ToDateTime(drt["estimatedstart"]);
                    task.EstimatedEnd = Convert.ToDateTime(drt["estimatedend"]);
                    task.ActualStart = Convert.ToDateTime(drt["actualstart"]);
                    task.ActualEnd = Convert.ToDateTime(drt["actualend"]);
                    task.ProjectStatusCode = Convert.ToInt32(drt["pstatuscode"]);
                    task.Tester1 = Convert.ToInt32(drt["tester1"]);
                    task.Tester2 = Convert.ToInt32(drt["tester2"]);
                    task.RoomId = Convert.ToInt32(drt["roomid"]);
                    taskList.Add(task);
                }
            }
            return taskList;
        }
         public bool Create(Task task)
        {
            string st = @"insert into dbo.task (name,projectid,roomid,estimatedstart,estimatedend,description,tester1,tester2,createdby,createdon,modifiedby,modifiedon)
             values(@name,@projectid,@roomid,@estimatedstart,@estimatedend,@description,@tester1,@tester2,@createdby,now(),@modifiedby,now())";
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
                new NpgsqlParameter("@createdby",task.CreatedBy),
                new NpgsqlParameter("@modifiedby",task.CreatedBy)
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
            string st = @"update dbo.task set name=@name,estimatedstart=@estimatedstart,estimatedend=@estimatedend,description=@description,modifiedby=@modifiedby,modifiedon=now(),
                          tester1=@tester1,tester2=@tester2,  roomid=@roomid where taskid=@taskid";
            NpgsqlParameter[] pa = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",task.Name),
                new NpgsqlParameter("@estimatedstart",task.EstimatedStart),
                new NpgsqlParameter("@estimatedend",task.EstimatedEnd),
                new NpgsqlParameter("@description",task.Description),
                new NpgsqlParameter("@taskid",task.TaskId),
                new NpgsqlParameter("@modifiedby",task.ModifiedBy),
                new NpgsqlParameter("@tester1",task.Tester1),
                 new NpgsqlParameter("@tester2",task.Tester2),
                new NpgsqlParameter("@roomid",task.RoomId)
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
