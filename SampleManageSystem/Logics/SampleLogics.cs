using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class SampleLogics : BaseLogics<Sample>
    {
        private const string _Type = "SampleAccess";
        private IDataAccess.ISampleAccess _Dal;

        public SampleLogics()
            : base(_Type)
        {
            _Dal = base.dal as IDataAccess.ISampleAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }


        public IList<Sample> GetSampleList()
        {
            return _Dal.GetSampleList();
        }
        public IList<Sample> GetSampleListByContainerId(int containerId)
        {
            return _Dal.GetSampleListByContainerId(containerId);
        }
        public IList<Sample> GetSampleListByTaskId(int taskid)
        {
            return _Dal.GetSampleListByTaskId(taskid);
        }
        public IList<Sample> ExceptSampleList(int taskid,int projectid)
        {
            return _Dal.ExceptSampleList(taskid, projectid);
        }
         public bool DeleteSample(int sampleId)
        {
            _Dal.DeleteTaskSample(sampleId);
            return _Dal.Delete(sampleId);
        }
        public bool UpdateContainerId(int sampleId)
        {
            return _Dal.UpdateContainerId(sampleId);
        }
        public long GetSampleCount()
        {
            return _Dal.GetSampleCount();
        }
        public IList<Sample> GetSamplePageList(int pageNo)
        {
            return _Dal.GetSamplePageList(pageNo);
        }
        public bool CreateSample(Sample sample)
        {
            if (_Dal.ExistsSampleCode(sample))
            {
                return false;
            }
            else
            {
                return _Dal.Create(sample);
            }
        }
         public bool UpdateSample(Sample sample)
        {
            if (sample.ContainerId != null)
            {
                _Dal.UpdateContainerStatusCode(sample);
            }
            return Update(sample);
        }
    }
}
