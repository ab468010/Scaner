using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Model;
using SqlHelper;
using Npgsql;
namespace DataAccess
{
    public class ProjectAccess : IDataAccess.IProjectAccess
    {
  
        public IList<Project> GetProjectListByStatusCode()
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
        public IList<Project> GetProjectListByTesterId(int userId,int statusCode)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                where testerid=@testerid and statuscode=@statuscode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@testerid",userId),
                new NpgsqlParameter("@statuscode",statusCode)
                
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public IList<Project> GetProjectListByTesterId(int userId)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                   where testerid=@testerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@testerid",userId)
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public IList<Project> GetAllProjectList(int statusCode)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                  where statuscode=@statuscode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@statuscode",statusCode)
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public IList<Project> GetAllProjectList()
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
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
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
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
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName,project.description
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    project.Description = rdr["description"].ToString();
                    project.StartTime = (DateTime)rdr["starttime"];
                    project.EndTime = (DateTime)rdr["endtime"];
                }
            }
            return project;

        }

        public IList<Project> GetProjectListByEngineerId(int userId,int statusCode)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                 where engineerid=@engineerid and statuscode=@statuscode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@engineerid",userId),
                new NpgsqlParameter("@statuscode",statusCode)
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }

        public IList<Project> GetProjectListByEngineerId(int userId)
        {
            string sqlStr = @"SELECT project.projectid, project.projectno, 
                                project.name,project.engineerid,engineer.Name EngineerIdName,project.statecode,project.statuscode,
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
                                FROM dbo.project project
                                Left Join dbo.SystemUser engineer On project.engineerid = engineer.systemuserid
                                Left Join dbo.SystemUser tester On project.testerid = tester.systemuserid
                                Left Join dbo.Customer customer On project.customerid = customer.customerid
                                 where engineerid=@engineerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@engineerid",userId)
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
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
                                project.testerid,tester.Name TesterIdName,COALESCE(project.customerid,-1) customerid,customer.Name CustomerIdName
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
                    project.TesterId = Convert.ToInt32(rdr["testerid"]);
                    project.TesterIdName = rdr["TesterIdName"].ToString();
                    project.CustomerId = Convert.ToInt32(rdr["customerid"]);
                    project.CustomerIdName = rdr["CustomerIdName"].ToString();
                    projectList.Add(project);
                }
            }
            return projectList;
        }
        public int CreateProject(Project project)
        {
            string sqlStr = @"Insert into dbo.Project ( projectno, name, engineerid, testerid,customerid, 
            description, statuscode, statecode) values(@ProjectNo,@Name,@EngineerId,@TesterId,@Customerid,@Description,1,1) returning projectid";
            NpgsqlParameter[] commandParameters = new NpgsqlParameter[]{
                new NpgsqlParameter("@ProjectNo",project.ProjectNo),
                new NpgsqlParameter("@Name",project.Name),
                new NpgsqlParameter("@EngineerId",project.EngineerId),
                new NpgsqlParameter("@TesterId",project.TesterId),
                new NpgsqlParameter("@CustomerId",project.CustomerId),
                new NpgsqlParameter("@Description",project.Description)
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
            string st = "update dbo.project set name=@name,projectno=@projectno,engineerid=@engineerid,testerid=@testerid,description=@description where projectid=@projectid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",project.Name),
                new NpgsqlParameter("@projectno",project.ProjectNo),
                new NpgsqlParameter("@engineerid",project.EngineerId),
                new NpgsqlParameter("@testerid",project.TesterId),
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
        public bool Create(Project p)
        {
            return true;
        }
    }
}
