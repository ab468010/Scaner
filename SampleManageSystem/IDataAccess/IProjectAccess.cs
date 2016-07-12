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
        IList<Project> GetProjectListByEngineerId(int userId,int Page);

        IList<Project> GetProjectListByTesterId(int userId,int Page);
   
        IList<Project> GetAllProjectList(int Page);
     
        IList<Project> GetUProjectListByUserId();
        IList<Project> GetProjectList();
        IList<Project> GetUProjectList(int page);
        IList<Project> GetUProjectListByEnginnerId(int systemuserId, int page);
        IList<Project> GetUProjectListByTesterId(int systemuserId, int page);
        IList<Project> GetALLDelayProject(int Page);
        IList<Project> GetDelayProjectByEngineerId(int systemuserId,int Page);
        IList<Project> GetDelayProjectByTester(int systemuserId,int Page);
        IList<Project> GetNotFinishedProjectList();
        long GetAllUProjectCount();
        long GetAllProjectCount();
        long GetUProjectCountByEngineer(int systemuserId);
        long GetProjectCountByEngineer(int userId);
        long GetUProjectByTaksTester(int systemuserId);
        long GetProjectByTaksTester(int userId);
        long GetDelayProjectCount();
        long GetDelayProjectCountByEngineerId(int systemuserId);
        long GetDelayProjectCountByTesterId(int systemuserId);
        bool UpdateStatus(int projectid,int status);
        bool ExistTask(int projectId);
        int CreateProject(Project project);
        bool UpdateProjectStatusCode(int projectId);
        IList<Project> GetProjectListByStatus();
        bool UpdateContainerProjectId(int projectId);
        bool UpdateSampleProjectId(int projectId);
  


    }
}
