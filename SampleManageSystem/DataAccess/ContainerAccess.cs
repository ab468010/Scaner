using System;
using System.Collections.Generic;
using System.Data;
using Model;
using SqlHelper;
using Npgsql;
namespace DataAccess
{
    public class ContainerAccess : IDataAccess.IContainerAccess
    {
        public long GetBigContainer()
        {
            string sqlStr = " select count(1) from dbo.container where right(size,1)='大'";
           
         
            long count =Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr));
            return count;
        }
        public long GetSmallContainer()
        {
            string sqlStr = " select count(1) from dbo.container where right(size,1)='小'";
         
            long count =Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr));
            return count;
        }
        public bool ExsitsContainerCode(Container container)
        {
            string st = "select count(1) from dbo.container where containercode=@containercode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@containercode",container.ContainerCode)
            };
            if ((long)(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st, par)) > 0)
            {
                //1
                return true;
            }
            else
            {
                return false;
            }
        }
        public long GetUseContainerCount()
        {
            string st = "select count(distinct containerid) from dbo.sample";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public IList<Container> GetContainerPageList(int pageNo)
        {
            string sqlStr = @"SELECT box.containerid, box.name, box.size, box.containercode, box.statuscode, COALESCE(box.projectid,-1) projectid,project.Name ProjectIdName,box.description ,COALESCE(project.statuscode,-1) projectstatuscode
                                FROM dbo.container box
                                Left Join dbo.Project project on box.projectid = project.projectid order by box.createdon desc limit 10 offset @pageNo";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@pageNo",pageNo)
            };
            IList<Container> containerList = new List<Container>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Container container = new Container();
                    container.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    container.Name = rdr["name"].ToString();
                    container.Size = rdr["size"].ToString();
                    container.ContainerCode = rdr["containercode"].ToString();
                    container.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    container.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    container.ProjectIdName = rdr["projectIdName"].ToString();
                    container.Description = rdr["Description"].ToString();
                    container.ProjectStatusCode = Convert.ToInt32(rdr["projectstatuscode"]);
                    containerList.Add(container);
                }
            }
            return containerList;
        }
        public long GetUseBigContainer()
        {
            string st = @"select count(size) from dbo.container
                        where containerid in (select distinct containerid from dbo.sample where containerid>0) and right(size,1) ='大'";

            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;

        }
        public long GetContainerCount()
        {
            string st = "select count(1) from dbo.container";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public long GetUseSmallContainer()
        {
            string st = @"select count(size) from dbo.container
                        where containerid in (select distinct containerid from dbo.sample where containerid>0) and right(size,1) ='6'
                        group by size";
         
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public bool ExistsSample(int containerId)
        {
            string st = "select count(1) from dbo.sample where containerid=@containerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@containerid",containerId)
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
        public IList<Container> GContainerList(int taskid)
        {
            string sqlStr = @"select co.containerid,name,size,containercode from 
                      dbo.container co left join dbo.taskcontainer ta on co.containerid=ta.containerid where taskid=@taskid";


            IList<Container> containerList = new List<Container>();
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskid",taskid)
            };
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Container container = new Container();
                    container.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    container.Name = rdr["name"].ToString();
                    container.Size = rdr["size"].ToString();
                    container.ContainerCode = rdr["containercode"].ToString();


                    containerList.Add(container);
                }
            }
            return containerList;
        }
        public bool Delete(int containerId)
        {
            string sqlStr = @"Delete from dbo.Container
                                Where ContainerId = @ContainerId";
            NpgsqlParameter commandParameters = new NpgsqlParameter("@ContainerId", containerId);


            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters) > 0)
                return true;
            else
                return false;
        }

        public Model.Container GetModel(int containerId)
        {
            string sqlStr = @"SELECT box.containerid, box.name, box.size, box.containercode, box.statuscode, COALESCE(box.projectid,-1) projectid,project.Name ProjectIdName,box.description,COALESCE(project.statuscode,-1) ProjectStatusCode
                                FROM dbo.container box
                                Left Join dbo.Project project on box.projectid = project.projectid
                                Where containerId = @ContainerId";


            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ContainerId",containerId)
            };

            Container container = new Container();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par))
            {
                while (rdr.Read())
                {
                    container.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    container.Name = rdr["name"].ToString();
                    container.Size = rdr["size"].ToString();
                    container.ContainerCode = rdr["containercode"].ToString();
                    container.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    container.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    container.ProjectIdName = rdr["projectIdName"].ToString();
                    container.Description = rdr["Description"].ToString();
                    container.ProjectStatusCode = Convert.ToInt32(rdr["ProjectStatusCode"]);
                }
                return container;
            }
        }

        public bool UpdateContainerProject(int containerId,int projectId)
        {
            string sqlStr = @"UPDATE dbo.container SET projectid = @ProjectId Where containerid=@ContainerId";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ProjectId",projectId),
                new NpgsqlParameter("@ContainerId",containerId)
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

        public IList<Container> GetContainerList()
        {
            string sqlStr = @"SELECT box.containerid, box.name, box.size, box.containercode, box.statuscode, COALESCE(box.projectid,-1) projectid,project.Name ProjectIdName,box.description
                                FROM dbo.container box
                                Left Join dbo.Project project on box.projectid = project.projectid";


            IList<Container> containerList = new List<Container>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Container container = new Container();
                    container.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    container.Name = rdr["name"].ToString();
                    container.Size = rdr["size"].ToString();
                    container.ContainerCode = rdr["containercode"].ToString();
                    container.StatusCode = Convert.ToInt32(rdr["statuscode"]);
                    container.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    container.ProjectIdName = rdr["projectIdName"].ToString();
                    container.Description = rdr["Description"].ToString();
                    containerList.Add(container);
                }
            }
            return containerList;
        }

        public bool Create(Model.Container container)
        {
            string sqlStr = @"Insert into dbo.Container ( name, containercode, size,description, 
             statuscode,createdby,createdon) values(@Name,@ContainerCode,@Size,@Description,1,@createdby,now())";
            NpgsqlParameter[] commandParameters = new NpgsqlParameter[]{
                new NpgsqlParameter("@Name",container.Name),
                new NpgsqlParameter("@ContainerCode",container.ContainerCode),
                new NpgsqlParameter("@Size",container.Size),
                new NpgsqlParameter("@Description",container.Description),
                new NpgsqlParameter("@createdby",container.CreatedBy)
            };

            int createCount = NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters);

            if (createCount > 0)
                return true;
            else
                return false;
        }
        public bool Disable(int id)
        {
            return true;
        }
        public bool Update(Model.Container container)
        {
            string sqlStr = @"update dbo.Container Set name = @Name,containercode = @ContainerCode, size = @Size,description = @Description 
                               Where ContainerId = @ContainerId";
            NpgsqlParameter[] commandParameters = new NpgsqlParameter[]{
                new NpgsqlParameter("@ContainerId",container.ContainerId), 
                new NpgsqlParameter("@Name",container.Name),
                new NpgsqlParameter("@ContainerCode",container.ContainerCode),
                new NpgsqlParameter("@Size",container.Size),
                new NpgsqlParameter("@Description",container.Description)
            };

            int createCount = NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters);

            if (createCount > 0)
                return true;
            else
                return false;
        }
    }
}
