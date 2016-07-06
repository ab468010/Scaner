using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
namespace Logics
{
      public  class TaskLogics:BaseLogics<Model.Task>
    {

        private const string _Type = "TaskAccess";
        private IDataAccess.ITaskAccess _Dal;

        public TaskLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.ITaskAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }

        }
        public bool CreateTask(Task task)
        {
            return _Dal.Create(task);
        }
        public IList<Task> SelectTask(int number)
        {
            return _Dal.SelectTask(number);
        }
        public bool DeleteTask(int taskid)
        {
            if (_Dal.ExistsSample(taskid))
            {
                return false;
            }
            else
            {
                return _Dal.Delete(taskid);
            }
            
        }
    
        public bool UpdateTask(Task task)
        {
            return _Dal.Update(task);
        }
        public long GetTaskCount()
        {
            return _Dal.GetTaskCount();
        }
        public IList<Task> GetTaskListByProjectId(int projectId)
        {
            return _Dal.GetTaskListByProjectId(projectId);
        }
        public IList<Task> GetTaskList()
        {
            return _Dal.GetTaskList();
        }
        public long GetDelayTaskCount()
        {
            return _Dal.GetDelayTaskCount();
        }
        public long GetDelayProjectCount()
        {
            return _Dal.GetDelayProjectCount();
        }
        public long GetGoingProjectCount()
        {
            return _Dal.GetGoingProjectCount();
        }
        public long GetFinishProjectCount()
        {
            return _Dal.GetFinishProjectCount();
        }
    }
}
