using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
namespace IDataAccess
{
     public interface ITaskAccess:IBaseAccess<Model.Task>
    {
        IList<Task> SelectTask(int number);
        long GetTaskCount();
        bool ExistsSample(int taskid);
        IList<Task> GetTaskListByProjectId(int projectId);
        IList<Task> GetTaskListByProjectIdTesterId(int projectId, int systemuserId);
        IList<Task> GetTaskList();
        long GetDelayTaskCount();
       
        long GetGoingProjectCount();
        
       IList<Task> SelectTaskByEngineerId(int number, int systemuserId);
       IList<Task> SelectTaskByTseterId(int number,int systemuserId);
        IList<Task> GetAllDelayTask(int Page);
        IList<Task> GetDelayTaskByEngineer(int systemuserId,int Page);
        IList<Task> GetDelayTaskByTester(int systemuserId,int Page);
        long GetAllUTaskCount();
        long GetUTaskByEngineerId(int systemuserId);
        long GetUTaskByTesterId(int systemuserId);
        long GetDelayTaskCountByEngineerId(int systemuserId);
        long GetDelayTaskCountByTesterId(int systemuserId);
        bool UpdateTaskActualEnd(int taskid, int systemuserId);
        IList<Task> GetAllTaskListByPId(int number, int projectId);
        IList<Task> GetTaskListByPId(int number, int systemuserId, int projectId);

    }

}
