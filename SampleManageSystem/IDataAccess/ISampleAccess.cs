using Model;
using System.Collections.Generic;

namespace IDataAccess
{
    public interface ISampleAccess : IBaseAccess<Model.Sample>
    {

        /// <summary>
        /// 获得所有可用周转箱
        /// </summary>
        /// <returns></returns>
        IList<Sample> GetSampleList();

        IList<Sample> GetSampleListByTaskId(int taskid);
        IList<Sample> GetSampleListByContainerId(int containerId);
        IList<Sample> ExceptSampleList(int taskid, int projectid);
        bool DeleteTaskSample(int sampleId);
        bool UpdateContainerId(int sampleId,int containerId);
        long GetSampleCount();
        IList<Sample> GetSamplePageList(int pageNo);
        bool ExistsSampleCode(Sample sample);

        bool UpdateContainerStatusCode(Sample sample);
        bool UpdateContainerStatus(int containerId);
        bool ExistsSample(int containerId);
    }
}
