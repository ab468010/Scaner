using System;
using System.Collections.Generic;
using System.Data;
using Model;
using SqlHelper;
using Npgsql;
namespace DataAccess
{
    public class SampleAccess : IDataAccess.ISampleAccess
    {
        public bool UpdateContainerStatusCode(Sample sample)
        {
            string sqlStr = "update dbo.container set statuscode=2 where containerid=@containerid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@containerid",sample.ContainerId)
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
        public bool ExistsSampleCode(Sample sample)
        {
            string st = "select count(1) from dbo.sample where samplecode=@samplecode";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@samplecode",sample.SampleCode)
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
        public IList<Sample> GetSamplePageList(int pageNo)
        {
            string sqlStr = @"SELECT sampleid, sample.name, COALESCE(sample.shelfid,-1) shelfid, shelf.Name ShelfIdName,
				                COALESCE(sample.containerid,-1) containerid,container.Name ContainerIdName, samplecode, sampleclass,
				                COALESCE(sample.projectid,-1) projectid,project.Name ProjectIdName
                                FROM dbo.sample sample
                                Left Join dbo.Shelf shelf On sample.shelfid = shelf.shelfid
                                Left Join dbo.container container On sample.containerid = container.containerid
                                Left Join dbo.project project On sample.projectid = project.projectid limit 10 offset @pageNo";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@pageNo",pageNo)
            };

            IList<Sample> sampleList = new List<Sample>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Sample sample = new Sample();
                    sample.SampleId = Convert.ToInt32(rdr["sampleid"]);
                    sample.Name = rdr["name"].ToString();
                    sample.ShelfId = Convert.ToInt32(rdr["shelfid"]);
                    sample.SampleCode = rdr["samplecode"].ToString();
                    sample.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    sample.SampleClass = Convert.ToInt32(rdr["sampleclass"]);
                    sample.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    sample.ShelfIdName = rdr["ShelfIdName"].ToString();
                    sample.ContainerIdName = rdr["ContainerIdName"].ToString();
                    sample.ProjectIdName = rdr["ProjectIdName"].ToString();
                    sampleList.Add(sample);
                }
            }
            return sampleList;
        }
       public long GetSampleCount()
        {
            string st = "select count(1) count from dbo.sample";
            long count = Convert.ToInt64(NpgSqlHelper.ExecuteScalar(NpgSqlHelper.ConnectionString, CommandType.Text, st));
            return count;
        }
        public bool UpdateContainerId(int sampleId)
        {
            string st = "update dbo.sample set containerid=@containerid where sampleid=@sampleid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@containerid",DBNull.Value),
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
        public bool DeleteTaskSample(int sampleId)
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
        public IList<Sample> ExceptSampleList(int taskid,int projectid)
        {
            IList<Sample> sampleList = new List<Sample>();
            string st = @"select sampleid ,name from dbo.sample
                        where projectid =@projectid and sampleid
                        not in(
                        select sampleid from dbo.tasksample
                        where taskid =@taskid)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projectid",projectid),
                new NpgsqlParameter("@taskid",taskid)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Sample sample = new Sample();
                    sample.SampleId = Convert.ToInt32(rdr["sampleid"]);
                    sample.Name = rdr["name"].ToString();
                 
                    sampleList.Add(sample);
                }
            }
            return sampleList;
        }
        public IList<Sample> GetSampleListByTaskId(int taskid)
        {
            IList<Sample> samplelist = new List<Sample>();
            string st = @"select sam.sampleid,sam.name samplename,samplecode,sampleclass,coalesce(sam.containerid,-1) containerid ,con.name containername
                         from dbo.sample sam 
                         left join dbo.container con on sam.containerid = con.containerid
                         left join dbo.tasksample task on sam.sampleid = task.sampleid  
                          where task.taskid = @taskid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskid",taskid)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    Sample sample = new Sample();
                    sample.SampleId = Convert.ToInt32(rdr["sampleid"]);
                    sample.Name = rdr["samplename"].ToString();
                    sample.SampleCode = rdr["samplecode"].ToString();
                    sample.SampleClass = Convert.ToInt32(rdr["sampleclass"]);
                    sample.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    sample.ContainerIdName = rdr["containername"].ToString();
                    samplelist.Add(sample);
                }
               
            }
            return samplelist;
        }
        public Sample GetModel(int id)
        {
            string sqlStr = @"SELECT sampleid, sample.name, COALESCE(sample.shelfid,-1) shelfid, shelf.Name ShelfIdName,
				                COALESCE(sample.containerid,-1) containerid,container.Name ContainerIdName, samplecode, sampleclass,
				                COALESCE(sample.projectid,-1) projectid,project.Name ProjectIdName,sample.description,COALESCE(project.statuscode,-1) statuscode
                                FROM dbo.sample sample
                                Left Join dbo.Shelf shelf On sample.shelfid = shelf.shelfid
                                Left Join dbo.container container On sample.containerid = container.containerid
                                Left Join dbo.project project On sample.projectid = project.projectid
                                Where sample.sampleid = @SampleId";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@SampleId",id),
            };

            Sample sample = null;
            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    sample = new Sample();
                    sample.SampleId = Convert.ToInt32(rdr["sampleid"]);
                    sample.Name = rdr["name"].ToString();
                    sample.ShelfId = Convert.ToInt32(rdr["shelfid"]);
                    sample.SampleCode = rdr["samplecode"].ToString();
                    sample.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    sample.SampleClass = Convert.ToInt32(rdr["sampleclass"]);
                    sample.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    sample.ShelfIdName = rdr["ShelfIdName"].ToString();
                    sample.ContainerIdName = rdr["ContainerIdName"].ToString();
                    sample.ProjectIdName = rdr["ProjectIdName"].ToString();
                    sample.Description = rdr["description"].ToString();
                    sample.ProjectStatusCode = Convert.ToInt32(rdr["statuscode"]);
                }
            }
            return sample;
        }


        public IList<Sample> GetSampleList()
        {
            string sqlStr = @"SELECT sampleid, sample.name, COALESCE(sample.shelfid,-1) shelfid, shelf.Name ShelfIdName,
				                COALESCE(sample.containerid,-1) containerid,container.Name ContainerIdName, samplecode, sampleclass,
				                COALESCE(sample.projectid,-1) projectid,project.Name ProjectIdName
                                FROM dbo.sample sample
                                Left Join dbo.Shelf shelf On sample.shelfid = shelf.shelfid
                                Left Join dbo.container container On sample.containerid = container.containerid
                                Left Join dbo.project project On sample.projectid = project.projectid";


            IList<Sample> sampleList = new List<Sample>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr))
            {
                while (rdr.Read())
                {
                    Sample sample = new Sample();
                    sample.SampleId = Convert.ToInt32(rdr["sampleid"]);
                    sample.Name = rdr["name"].ToString();
                    sample.ShelfId = Convert.ToInt32(rdr["shelfid"]);
                    sample.SampleCode = rdr["samplecode"].ToString();
                    sample.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    sample.SampleClass = Convert.ToInt32(rdr["sampleclass"]);
                    sample.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    sample.ShelfIdName = rdr["ShelfIdName"].ToString();
                    sample.ContainerIdName = rdr["ContainerIdName"].ToString();
                    sample.ProjectIdName = rdr["ProjectIdName"].ToString();
                    sampleList.Add(sample);
                }
            }
            return sampleList;
        }

        public IList<Sample> GetSampleListByContainerId(int containerId)
        {
            string sqlStr = @"SELECT sampleid, sample.name, COALESCE(sample.shelfid,-1) shelfid, shelf.Name ShelfIdName,
				                COALESCE(sample.containerid,-1) containerid,container.Name ContainerIdName, samplecode, sampleclass,
				                COALESCE(sample.projectid,-1) projectid,project.Name ProjectIdName
                                FROM dbo.sample sample
                                Left Join dbo.Shelf shelf On sample.shelfid = shelf.shelfid
                                Left Join dbo.container container On sample.containerid = container.containerid
                                Left Join dbo.project project On sample.projectid = project.projectid
                                Where sample.containerid = @ContainerId";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ContainerId",containerId),
            };

            IList<Sample> sampleList = new List<Sample>();

            using (NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr,par))
            {
                while (rdr.Read())
                {
                    Sample sample = new Sample();
                    sample.SampleId = Convert.ToInt32(rdr["sampleid"]);
                    sample.Name = rdr["name"].ToString();
                    sample.ShelfId = Convert.ToInt32(rdr["shelfid"]);
                    sample.SampleCode = rdr["samplecode"].ToString();
                    sample.ContainerId = Convert.ToInt32(rdr["containerid"]);
                    sample.SampleClass = Convert.ToInt32(rdr["sampleclass"]);
                    sample.ProjectId = Convert.ToInt32(rdr["projectid"]);
                    sample.ShelfIdName = rdr["ShelfIdName"].ToString();
                    sample.ContainerIdName = rdr["ContainerIdName"].ToString();
                    sample.ProjectIdName = rdr["ProjectIdName"].ToString();
                    sampleList.Add(sample);
                }
            }
            return sampleList;
        }

        public bool Create(Model.Sample sample)
        {
            string sqlStr = "insert into dbo.sample (name,samplecode,sampleclass,description) values(@name,@samplecode,@sampleclass,@description)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@name",sample.Name),
                new NpgsqlParameter("@samplecode",sample.SampleCode),
                new NpgsqlParameter("@sampleclass",sample.SampleClass),
                new NpgsqlParameter("@description",sample.Description)
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
        public bool Delete(int sampleId)
        {
            string sqlStr = @"Delete from dbo.Sample
                                Where sampleid = @sampleid";

            NpgsqlParameter commandParameters = new NpgsqlParameter("@sampleid", sampleId);

            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, commandParameters) > 0)
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
        public bool Update(Model.Sample sample)
        {
            string sqlStr = @"UPDATE dbo.sample
                                Set name = @Name,
                                    projectid = @ProjectId,
                                    shelfid = @ShelfId,
                                    containerid = @ContainerId,
                                    samplecode = @SampleCode,
                                    sampleclass = @SampleClass,
                                    description = @Description
                                Where sampleid = @SampleId";

            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@SampleId",sample.SampleId),
                new NpgsqlParameter("@Name",sample.Name),
                new NpgsqlParameter("@SampleCode",sample.SampleCode),
                new NpgsqlParameter("@SampleClass",sample.SampleClass),
                new NpgsqlParameter("@ShelfId",(object)sample.ShelfId??DBNull.Value),
                new NpgsqlParameter("@ContainerId",(object)sample.ContainerId??DBNull.Value),
                new NpgsqlParameter("@ProjectId",(object)sample.ProjectId??DBNull.Value),
                new NpgsqlParameter("@Description",sample.Description)
            };

            if (NpgSqlHelper.ExecuteNonQuery(NpgSqlHelper.ConnectionString, CommandType.Text, sqlStr, par) > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
