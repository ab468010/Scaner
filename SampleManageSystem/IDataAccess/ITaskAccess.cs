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
        IList<Task> GetTaskList();
        long GetDelayTaskCount();
        long GetDelayProjectCount();
        long GetGoingProjectCount();
        long GetFinishProjectCount();
       IList<Task> SelectTaskByEngineerId(int number, int systemuserId);
       IList<Task> SelectTaskByTseterId(int number,int systemuserId);
    }
}
