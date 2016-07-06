using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using Npgsql;
using SqlHelper;
namespace DataAccess
{
    public class ProjectTemplateAccess:IDataAccess.IProjectTemplateAccess
    {
        public IList<ProjectTemplate> GetProjectTemplateList()
        {
            IList<ProjectTemplate> pro = new List<ProjectTemplate>();
            string st = @"select projecttemplateid,taskname,p.roomid,description,r.name
                        from dbo.projecttemplate p
                         left join dbo.room r on p.roomid=r.roomid";
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st))
            {
                while (rdr.Read())
                {
                    ProjectTemplate projecttemplate = new ProjectTemplate();
                    projecttemplate.ProjectTemplateId = Convert.ToInt32(rdr["projecttemplateid"]);
                    projecttemplate.TaskName = rdr["taskname"].ToString();
                    projecttemplate.Description = rdr["description"].ToString();
                    projecttemplate.RoomName = rdr["name"].ToString();
                    projecttemplate.RoomId = Convert.ToInt32(rdr["roomid"]);
                    pro.Add(projecttemplate);

                }
               
            }
            return pro;
        }
       public bool Create(ProjectTemplate projecttemplate)
        {
            string st = "insert into dbo.projecttemplate (taskname,roomid,description) values(@taskname,@roomid,@description)";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskname",projecttemplate.TaskName),
                new NpgsqlParameter("@roomid",projecttemplate.RoomId),
                new NpgsqlParameter("@description",projecttemplate.Description)
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
          public  bool Delete(int projectTemplateId)
        {
            string st = "delete from dbo.projecttemplate where projecttemplateid=@projecttemplateid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projecttemplateid",projectTemplateId)
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
       public bool Disable(int id)
        {
            return true;
        }
         public bool Update(ProjectTemplate projecttemplate)
        {
            string st = "update dbo.projecttemplate set taskname=@taskname,roomid=@roomid,description=@description where projecttemplateid=@projecttemplateid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@taskname",projecttemplate.TaskName),
                new NpgsqlParameter("@roomid",projecttemplate.RoomId),
                new NpgsqlParameter("@description",projecttemplate.Description),
                new NpgsqlParameter("@projecttemplateid",projecttemplate.ProjectTemplateId)
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
          public ProjectTemplate GetModel(int projectTemplateId)
        {
            ProjectTemplate projecttemplate = null;
            string st = "select projecttemplateid,taskname,roomid,description from dbo.projecttemplate where projecttemplateid=@projecttemplateid";
            NpgsqlParameter[] par = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@projecttemplateid",projectTemplateId)
            };
            using(NpgsqlDataReader rdr = NpgSqlHelper.ExecuteReader(NpgSqlHelper.ConnectionString, CommandType.Text, st, par))
            {
                while (rdr.Read())
                {
                    projecttemplate = new ProjectTemplate();
                    projecttemplate.ProjectTemplateId = Convert.ToInt32(rdr["projecttemplateid"]);
                    projecttemplate.TaskName = rdr["taskname"].ToString();
                    projecttemplate.RoomId = Convert.ToInt32(rdr["roomid"]);
                    projecttemplate.Description = rdr["description"].ToString();

                }
            }
            return projecttemplate;
        }
    }
}
