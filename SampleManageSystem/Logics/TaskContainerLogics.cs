using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Logics
{
      public class TaskContainerLogics:BaseLogics<Model.TaskContainer>
    {
        private const string _Type = "TaskContainerAccess";
        private IDataAccess.ITaskContainerAccess _Dal;

        public TaskContainerLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.ITaskContainerAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public bool CreateTaskContainer(int taskid, int containerid)
        {
            TaskContainer tc = new TaskContainer();
            tc.TaskId = taskid;
            tc.ContainerId = containerid;
            return _Dal.Create(tc);
        }

        public IList<TaskContainer> GetTaskContainerList()
        {
            return _Dal.GetTaskContainerList();
        }
    }
}
