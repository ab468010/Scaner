using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class ProjectLogics : BaseLogics<Project>
    {
        private const string _Type = "ProjectAccess";
        private IDataAccess.IProjectAccess _Dal;

        public ProjectLogics(): base(_Type)
        {
            _Dal = base.dal as IDataAccess.IProjectAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public IList<Project> GetUProjectListByUser(int systemuserId, int roleId, int page)
        {
            if (roleId == 6)
            {
               return _Dal.GetUProjectList(page);
            }else if(roleId==2){
                return _Dal.GetUProjectListByEnginnerId(systemuserId, page);
            }
            else if(roleId==3)
            {
                return _Dal.GetUProjectListByTesterId(systemuserId,page);
            }
            else
            {
                IList<Project> projectList = null;
                return projectList;
            }
           
        }

        public IList<Project> GetNotFinishedProjectList()
        {
            return _Dal.GetNotFinishedProjectList();
        }

        public IList<Project> GetProjectListByUserId(int userId,int roleId,int Page)
        {
            if (roleId == 6)
            {
                return _Dal.GetAllProjectList(Page);
            }
           else if (roleId == 2)
            {
                return _Dal.GetProjectListByEngineerId(userId,Page);
            }
            else if(roleId==3)
            {
                return _Dal.GetProjectListByTesterId(userId,Page);
            }
            return new List<Project>();
        }
  
        public IList<Project> GetProjectList()
        {
            return _Dal.GetProjectList();
        }
        public Project GetProject(int projectId)
        {
            return _Dal.GetModel(projectId);
        }
        public bool UpdateStatus(int projectId, int status)
        {
            return _Dal.UpdateStatus(projectId, status);
        }
        public bool DeleteProject(int projectId)
        {
            if (_Dal.ExistTask(projectId))
            {
                return false;
            }
            else
            {
                return _Dal.Delete(projectId);
            }
         
        }
        public int CreateProject(Project project)
        {
            int projectId = _Dal.CreateProject(project);
            ProjectTemplateLogics projectTemplateLogics = new ProjectTemplateLogics();
            TaskLogics taskLogics = new TaskLogics();

            IList<ProjectTemplate> projectTemplateList = projectTemplateLogics.GetProjectTemplateList();

            foreach (ProjectTemplate template in projectTemplateList)
            {
                Model.Task task = new Model.Task();
                task.Name = template.TaskName;
                task.RoomId = template.RoomId;
                task.ProjectId = projectId;
                task.Description = template.Description;
                taskLogics.CreateTask(task);
            }
            return projectId;
        }
        public bool UpdateProjectStatusCode(int projectId)
        {
            _Dal.UpdateContainerProjectId(projectId);
            _Dal.UpdateSampleProjectId(projectId);
            return _Dal.UpdateProjectStatusCode(projectId);
        }
        public IList<Project> GetProjectListByStatus()
        {
            return _Dal.GetProjectListByStatus();
        }
        public long GetUProjectCount(int systemuserId,int roleId)
        {
            if (roleId == 6)
            {
                return _Dal.GetAllUProjectCount();
            }else if(roleId==2){
                return _Dal.GetUProjectCountByEngineer(systemuserId);
            }
            else
            {
                return _Dal.GetUProjectByTaksTester(systemuserId);
            }
        }
         public IList<Project> GetDelayProjectList(int systemuserId,int roleId,int Page)
        {
            if (roleId == 6)
            {
                return _Dal.GetALLDelayProject(Page);
            }else if (roleId == 2)
            {
                return _Dal.GetDelayProjectByEngineerId(systemuserId,Page);
            }else if (roleId == 3)
            {
                return _Dal.GetDelayProjectByTester(systemuserId,Page);
            }
            else
            {
                IList<Project> projectList = new List<Project>();
                return projectList;
            }
        }
        public long GetDelayProjectCount(int systemuserId, int roleId)
        {
            if (roleId == 6)
            {
                return _Dal.GetDelayProjectCount();
            }
            else if (roleId == 2)
            {
                return _Dal.GetDelayProjectCountByEngineerId(systemuserId);
            }
            else if (roleId == 3)
            {
                return _Dal.GetDelayProjectCountByTesterId(systemuserId);
            }
            else
            {
                var a = 0;
                return a;
            }

        }
        public long GetProjectCount(int userId,int roleId)
        {
            if (roleId == 6)
            {
                return _Dal.GetAllProjectCount();
            }
            else if (roleId == 2)
            {
                return _Dal.GetProjectCountByEngineer(userId);
            }
            else if (roleId == 3)
            {
                return _Dal.GetProjectByTaksTester(userId);
            }
            else
            {
                var a = 0;
                return a;
            }
        }

    }
}
