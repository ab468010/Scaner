using Model;
using System.Collections.Generic;

namespace IDataAccess
{
    public interface IProjectAccess:IBaseAccess<Model.Project>
    {
        /// <summary>
        /// 获得所有指定用户的Project列表
        /// </summary>
        /// <returns></returns>
        IList<Project> GetProjectListByEngineerId(int userId);
        IList<Project> GetProjectListByEngineerId(int userId,int statusCode);
        IList<Project> GetProjectListByTesterId(int userId);
        IList<Project> GetProjectListByTesterId(int userId,int statusCode);
        IList<Project> GetAllProjectList();
        IList<Project> GetAllProjectList(int statusCode);
        IList<Project> GetUProjectListByUserId();
        IList<Project> GetProjectList();
      
        bool UpdateStatus(int projectid,int status);
        bool ExistTask(int projectId);
        int CreateProject(Project project);
        bool UpdateProjectStatusCode(int projectId);
        IList<Project> GetProjectListByStatusCode();
 
    }
}
