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
        IList<Project> GetProjectListByEngineerId(int systemuserId,int Page);

        IList<Project> GetProjectListByTesterId(int systemuserId,int Page);
   
        IList<Project> GetAllProjectList(int Page);
     
        IList<Project> GetUProjectListByUserId();
        IList<Project> GetProjectList();
        IList<Project> GetUProjectList(int page);
        IList<Project> GetUProjectListByEnginnerId(int systemuserId, int page);
        IList<Project> GetUProjectListByTesterId(int systemuserId, int page);
        IList<Project> GetALLDelayProject(int Page,int Limit);
        IList<Project> GetDelayProjectByEngineerId(int systemuserId,int Page,int Limit);
        IList<Project> GetDelayProjectByTester(int systemuserId,int Page,int Limit);
        IList<Project> GetNotFinishedProjectList();
        long GetAllUProjectCount();
        long GetAllProjectCount();
        long GetUProjectCountByEngineer(int systemuserId);
        long GetProjectCountByEngineer(int systemuserId);
        long GetUProjectByTaksTester(int systemuserId);
        long GetProjectByTaksTester(int systemuserId);
        long GetDelayProjectCount();
        long GetDelayProjectCountByEngineerId(int systemuserId);
        long GetDelayProjectCountByTesterId(int systemuserId);
        bool UpdateStatus(int projectid,int status);
        bool ExistTask(int projectId);
        int CreateProject(Project project);
        bool UpdateProjectStatusCode(int projectId,int ModifiedBy);
        IList<Project> GetProjectListByStatus();
        IList<Container> UpdateContainerProjectId(int projectId,int ModifiedBy);
        bool UpdateSampleContainer(int projectId,int ModifiedBy);
        IList<Project> GetAllFinishProjectList(int Page);
        IList<Project> GetFinishProjectListByEngineerId(int Page,int systemuserId);
        IList<Project> GetFinishProjectListByTseterId(int Page,int systemuserId);
        long GetFinishProjectCount();
        long GetFinishProjectCountByEngineerId(int systemuserId);
        long GetFinishProjectCountByTesterId(int systemuserId);
        IList<Project> GetProjectStatusCodeList( int Page,int statusCode);
        IList<Project> GetProjectStatusCodeListByEngineerId(int systemuserId, int Page, int statusCode);
        IList<Project> GetProjectStatusCodeListByTesterId(int systemuserId, int Page, int statusCode);
        long GetProjectStatusCodeCount(int statusCode);
        long GetProjectStatusCodeCountByEngineerId(int systemuserId, int statusCode);
        long GetProjectStatusCodeCountByTesterId(int systemuserId, int statusCode);
          bool   ExistsSample(int ContainerId);
        bool UpdateContainerCode(int ContainerId,int ModifiedBy);
    }
}
