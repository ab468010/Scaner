using System;
using System.Collections.Generic;
using System.Data;
using Model;
using SqlHelper;
using Npgsql;
namespace DataAccess
{
    public class ProjectAccess : IDataAccess.IProjectAccess
    {
        public long GetAllProjectCount()
        {
            string sqlStr = "select count(1) from dbo.project ";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr));
            return count;
        }
        public long GetProjectCountByEngineer(int userId)
        {
            string sqlStr = "select count(1) from dbo.project where  and engineerid=@systemuserid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",userId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par));
            return count;
        }
        public long GetProjectByTaksTester(int userId)
        {
            string sqlStr = "select count(1) from dbo.project where projectid in(select projectid from dbo.task where tester1=@systemuserid or tester2=@systemuserid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",userId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par));
            return count;
        }
        public long GetDelayProjectCountByTesterId(int systemuserId)
        {
            string st = @"select count(1) from dbo.project pr 
                         left join dbo.systemuser sy on sy.systemuserid = pr.engineerid
                         where exists(
                         select 1 from dbo.task task
                         where now() > estimatedend and actualend is null and(tester1=@systemuserid or tester2=@systemuserid) and pr.projectid = task.projectid limit 1  )";
            NpgsqlParameter[] par = new NpgsqlParameter[]
           {
                new NpgsqlParameter("@systemuserid",systemuserId)
           };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par));
            return count;
        }
        public long GetDelayProjectCountByEngineerId(int systemuserId)
        {
            string st = @"select count(1) from dbo.project pr left join dbo.task ta
                         on pr.projectid = ta.projectid
                         left join dbo.systemuser sy on sy.systemuserid = pr.engineerid
                         where now() > estimatedend and actualend is null and pr.engineerid=@engineerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@engineerid",systemuserId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st,par));
            return count;
        }
        public long GetDelayProjectCount()
        {
            string st = @"select count(distinct projectid) from dbo.task
                          where now() > estimatedend  and actualend is null and projectid is not null";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }

        public IList<Project> GetDelayProjectByTester(int systemuserId,int Page)
        {
            string st = @"select pr.projectid,pr.projectno,pr.name,sy.username from dbo.project pr 
                         left join dbo.systemuser sy on sy.systemuserid = pr.engineerid
                         where exists(
                         select 1 from dbo.task task
                         where now() > estimatedend and actualend is null and(tester1=@systemuserid or tester2=@systemuserid) and pr.projectid = task.projectid limit 1) limit 5 offset @page";
            IList<Project> projectList = new List<Project>();
            NpgsqlParameter[] par = new NpgsqlParameter[]
         {
                new NpgsqlParameter("@systemuserid",systemuserId),
                new NpgsqlParameter("@page",Page)
         };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId= Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerIdName = rdr["username"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;

        }
        public IList<Project> GetDelayProjectByEngineerId(int systemuserId,int Page)
        {
            string st = @"select pr.projectid,pr.projectno,pr.name,sy.username from dbo.project pr left join dbo.task ta
                         on pr.projectid = ta.projectid
                         left join dbo.systemuser sy on sy.systemuserid = pr.engineerid
                         where now() > estimatedend and actualend is null and pr.engineerid=@engineerid limit 5 offset @page";
            IList<Project> projectList = new List<Project>();
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@engineerid",systemuserId),
                new NpgsqlParameter("@page",Page)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st,par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerIdName = rdr["username"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public IList<Project> GetALLDelayProject(int Page)
        {
            string st = @"select pr.projectid,pr.projectno,pr.name,sy.username from dbo.project pr left join dbo.task ta
                        on pr.projectid = ta.projectid
                       left join dbo.systemuser sy on sy.systemuserid = pr.engineerid
                        where now() > estimatedend and actualend is null limit 5 offset @page";
            IList<Project> projectList = new List<Project>();
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@page",Page)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par)) 
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerIdName = rdr["username"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public IList<Project> GetUProjectListByTesterId(int systemuserId, int page)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                where project.statuscode<3 and project.projectid in(select projectid from dbo.task where tester1=@systemuserid or tester2=@systemuserid) 
                                order by project.createdon desc limit 10 offset @page ";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@page",page),
                new NpgsqlParameter("@systemuserid",systemuserId)
            };

            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                   
                    
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }

        public IList<Project> GetUProjectListByEnginnerId(int systemuserId,int page)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                where project.statuscode<3 and engineerid=@engineerid order by project.createdon desc limit 10 offset @page ";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@page",page),
                new NpgsqlParameter("@engineer",systemuserId)
            };

            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                    
                   
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
    
        public IList<Project> GetUProjectList(int page)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                where project.statuscode<3 order by project.createdon desc limit 10 offset @page ";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@page",page)
            };

            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                  
                
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public long GetAllUProjectCount()
        {
            string sqlStr = "select count(1) from dbo.project where statuscode<3";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr));
            return count;
        }
        public long GetUProjectCountByEngineer(int systemuserId)
        {
            string sqlStr = "select count(1) from dbo.project where statuscode<3 and engineerid=@systemuserid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par));
            return count;
        }
        public long GetUProjectByTaksTester(int systemuserId)
        {
            string sqlStr = "select count(1) from dbo.project where statuscode<3 and projectid in(select projectid from dbo.task where tester1=@systemuserid or tester2=@systemuserid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@systemuserid",systemuserId)
            };
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par));
            return count;
        }
        public bool UpdateSampleProjectId(int projectId)
        {
            string sqlStr = "update  dbo.sample set projectid=-1 where projectid=@projectid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projectid",projectId)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateContainerProjectId(int projectId)
        {
            string sqlStr = "update  dbo.container set projectid=-1 where projectid=@projectid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projectid",projectId)
            };
            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IList<Project> GetProjectListByStatus()
        {
            IList<Project> projectList = new List<Project>();
            string sqlStr = "select projectid,name from dbo.project where statuscode<3";
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.Name = rdr["name"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
      

  

        public IList<Project> GetProjectListByTesterId(int userId,int Page)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName,project.createdon
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                   where testerid=@testerid order by project.createdon desc limit 10 offset @page";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@testerid",userId),
                new NpgsqlParameter("@page",Page)
            };

            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                   
                   
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
      
        public IList<Project> GetAllProjectList(int Page)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName,project.createdon
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid order by project.createdon desc limit 10 offset @page";

            IList<Project> projectList = new List<Project>();
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@page",Page)
            };

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
         
                 
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public bool UpdateProjectStatusCode(int projectId)
        {
            string st = " update dbo.project set statuscode=5 where projectid=@projectid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projectid",projectId)
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
     
        public IList<Project> GetUProjectListByUserId()
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                where project.statuscode<3";

            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                   
                    
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public bool ExistTask(int projectId)
        {
            string st = "select count(1) from dbo.task where projectid=@projectid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projectid",projectId)
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
        public bool Delete(int projectId)
        {
            string sqlStr = @"Delete from dbo.Project
                                Where ProjectId = @ProjectId";
            NpgsqlParameter commandParameters = new NpgsqlParameter("@ProjectId", projectId);


            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters) > 0)
                return true;
            else
                return false;
        }
        public bool UpdateStatus(int projectId,int status)
        {
            string sqlStr = @"UPDATE dbo.project SET statuscode=@StatusCode Where projectid = @ProjectId";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ProjectId",projectId),
                new NpgsqlParameter("@StatusCode",status)
            };

            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par) > 0)
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
        public Project GetModel(int projectId)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,COALESCE(project.starttime,'1900-1-1') starttime,COALESCE(project.endtime,'1900-1-1') endtime,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName,project.description
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                Where project.projectid = @ProjectId";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ProjectId",projectId)
            };

            Project project = null;

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);


                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    project.Description = rdr["description"].ToString();
                    project.StartTime = (DateTime)rdr["starttime"];
                    project.EndTime = (DateTime)rdr["endtime"];
                }
            }
            return project;

        }

       

        public IList<Project> GetProjectListByEngineerId(int userId,int Page)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName,project.createdon
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                 where engineerid=@engineerid order by project.createdon desc limit 10 offset @page";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@engineerid",userId),
                new NpgsqlParameter("@page",Page)
            };

            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                    
                  
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }

        public IList<Project> GetProjectList()
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid";

            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                    
                   
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public int CreateProject(Project project)
        {
            string sqlStr = @"Insert into dbo.Project ( projectno, name, engineerid,customerid, 
            description, statuscode, statecode,createdby,createdon) values(@ProjectNo,@Name,@EngineerId,@Customerid,@Description,1,1,@createdby,now()) returning projectid";
            NpgsqlParameter[] commandParameters = new NpgsqlParameter[]{
                new NpgsqlParameter("@ProjectNo",project.ProjectNo),
                new NpgsqlParameter("@Name",project.Name),
                new NpgsqlParameter("@EngineerId",project.EngineerId),
             
                new NpgsqlParameter("@CustomerId",project.CustomerId),
                new NpgsqlParameter("@Description",project.Description),
                new NpgsqlParameter("@createdby",project.CreatedBy)
            };

            int projectid = (int)(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters));

            return projectid;
          
        }
        public bool Disable(int id)
        {
            return true;
        }
        public bool Update(Project project)
        {
            string st = "update dbo.project set name=@name,projectno=@projectno,engineerid=@engineerid,description=@description where projectid=@projectid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",project.Name),
                new NpgsqlParameter("@projectno",project.ProjectNo),
                new NpgsqlParameter("@engineerid",project.EngineerId),
               
                new NpgsqlParameter("@description",project.Description),
                new NpgsqlParameter("@projectid",project.ProjectId)
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
        public IList<Project> GetNotFinishedProjectList()
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                where project.statuscode<3 order by project.createdon desc ";
     
            IList<Project> projectList = new List<Project>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    project.ProjectNo = rdr["projectno"].ToString();
                    project.Name = rdr["name"].ToString();
                    project.EngineerId = Convert.ToInt32(rdr["engineerid"]);
                    project.EngineerIdName = rdr["EngineerIdName"].ToString();
                    project.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    project.StateCode = Convert.ToInt32(rdr["statecode"]);
                 
                    
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public bool Create(Project p)
        {
            return true;
        }
    }
}
