using Model;
using System.Collections.Generic;

namespace IDataAccess
{
    public interface IContainerAccess : IBaseAccess<Model.Container>
    {

        /// <summary>
        /// 获得所有可用周转箱
        /// </summary>
        /// <returns></returns>
        IList<Container> GetContainerList();
        bool UpdateContainerProject(int containerId, int projectId);
        IList<Container> GContainerList(int taskid);
        bool ExistsSample(int containerId);
        long GetContainerCount();
        long GetUseSmallContainer();
        long GetUseBigContainer();
        IList<Container> GetContainerPageList(int pageNo);
        long GetUseContainerCount();
        bool ExsitsContainerCode(Container container);
        long GetBigContainer();
        long GetSmallContainer();
        IList<Container> GetUseContainerList(int Page);


    }
}
