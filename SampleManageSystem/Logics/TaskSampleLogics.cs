using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Logics
{
      public class TaskSampleLogics:BaseLogics<Model.TaskSample>
    {
        private const string _Type = "TaskSampleAccess";
        private IDataAccess.ITaskSampleAccess _Dal;

        public TaskSampleLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.ITaskSampleAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public bool CreateTaskSample(int taskid,int sampleid)
        {
            return _Dal.CreateTaskSample(taskid, sampleid);
        }

        public IList<TaskSample> GetTaskSampleList()
        {
            return _Dal.GetTaskSampleList();
        }
    }
}
