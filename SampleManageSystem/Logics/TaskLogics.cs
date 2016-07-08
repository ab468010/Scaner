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
        public IList<Task> SelectTask(int number,int systemuserId,int roleId)
        {
            if (roleId == 6)
            {
                return _Dal.SelectTask(number);
            }else if (roleId == 2)
            {
                return _Dal.SelectTaskByEngineerId(number, systemuserId);
            }else if (roleId == 3)
            {
                return _Dal.SelectTaskByTseterId(number, systemuserId);
            }
            else
            {
                return null;
            }
           
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
        public IList<Task> GetDelayTaskList(int systemuserId,int roleId)
        {
            if (roleId == 6)
            {
                return _Dal. GetAllDelayTask();
            }else if (roleId == 2)
            {
                return _Dal. GetDelayTaskByEngineer(systemuserId);
            }else if (roleId == 3)
            {
                return _Dal. GetDelayTaskByTester(systemuserId);
            }
            else
            {
                IList<Task> taskList = new List<Task>();
                return taskList;
            }
        }


    }
}
