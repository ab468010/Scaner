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
        public IList<Task> SelectTask(int number,int systemuserId,int roleId, int projectId)
        {
            if (roleId == 6||roleId==18)
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
                IList<Task> tasklist = new List<Task>();
                return tasklist;
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
        public IList<Task> GetTaskListByProjectId(int projectId,int roleId,int systemuserId)
        {
            if (roleId == 6 || roleId == 18 || roleId == 2)
            {
                return _Dal.GetTaskListByProjectId(projectId);
            }else if (roleId == 3)
            {
                return _Dal.GetTaskListByProjectIdTesterId(projectId, systemuserId);
            }
            else
            {
                IList<Task> taskList = new List<Task>();
                return taskList;
            }
           
        }
        public IList<Task> GetTaskList()
        {
            return _Dal.GetTaskList();
        }
        public long GetDelayTaskCount(int systemuserId, int roleId)
        {
            if (roleId == 6||roleId==18)
            {
                return _Dal.GetDelayTaskCount();
            }else if (roleId == 2)
            {
                return _Dal.GetDelayTaskCountByEngineerId(systemuserId);
            }else if (roleId == 3)
            {
                return _Dal.GetDelayTaskCountByTesterId(systemuserId);
            }
            else
            {
                return 0;
            }
            
        }
    
        public long GetGoingProjectCount()
        {
            return _Dal.GetGoingProjectCount();
        }
    
        public IList<Task> GetDelayTaskList(int systemuserId,int roleId,int Page)
        {
            if (roleId == 6||roleId==18)
            {
                return _Dal. GetAllDelayTask(Page);
            }else if (roleId == 2)
            {
                return _Dal. GetDelayTaskByEngineer(systemuserId,Page);
            }else if (roleId == 3)
            {
                return _Dal. GetDelayTaskByTester(systemuserId,Page);
            }
            else
            {
                IList<Task> taskList = new List<Task>();
                return taskList;
            }
        }
        public long GetUTaskCount(int systemuserId,int roleId)
        {
            if (roleId == 6||roleId==18)
            {
                return _Dal.GetAllUTaskCount();
            }else if (roleId == 2)
            {
                return _Dal.GetUTaskByEngineerId(systemuserId);
            }else if (roleId == 3)
            {
                return _Dal.GetUTaskByTesterId(systemuserId);
            }
            else
            {
                return 0;
            }
        }
        public bool UpdateTaskActualEnd(int taskid, int systemuserId)
        {
            return _Dal.UpdateTaskActualEnd(taskid, systemuserId);
        }
        public IList<Task> GetTaskListByPId(int number, int systemuserId, int roleId, int projectId)
        {
            if (roleId == 6 || roleId == 18 || roleId == 2)
            {
                return _Dal.GetAllTaskListByPId(number, projectId);
            }else if (roleId == 3)
            {
                return _Dal.GetTaskListByPId(number,systemuserId,projectId);
            }
            else
            {
                IList<Task> taskList = new List<Task>();
                return taskList;
            }
        }
    }
}
