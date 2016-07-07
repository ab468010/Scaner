using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class ContainerLogics : BaseLogics<Container>
    {
        private const string _Type = "ContainerAccess";
        private IDataAccess.IContainerAccess _Dal;

        public ContainerLogics()
            : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IContainerAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }

        public bool UpdateContainerProject(int containerId, int projectId)
        {
            return _Dal.UpdateContainerProject(containerId, projectId);
        }
        public IList<Container> GetContainerList()
        {
            return _Dal.GetContainerList();
        }
        public IList<Container> GContainerList(int taskid)
        {
            return _Dal.GContainerList(taskid);
        }
        public bool DeleteContainer(int containerId)
        {
            if (_Dal.ExistsSample(containerId))
            {
                return false;
            }
            else
            {
                return _Dal.Delete(containerId);
            }
        }
        public long GetContainerCount()
        {
            return _Dal.GetContainerCount();
        }
        public long GetUseSmallContainer()
        {
            return _Dal.GetUseSmallContainer();
        }
        public long GetUseBigContainer()
        {
            return _Dal.GetUseBigContainer();
        }
        public IList<Container> GetContainerPageList(int pageNo)
        {
            return _Dal.GetContainerPageList(pageNo);
        }
        public long GetUseContainerCount()
        {
            return _Dal.GetUseContainerCount();
        }
        public bool CreateContainer(Container container)
        {
            if (_Dal.ExsitsContainerCode(container))
            {
                return false;
            }
            else
            {
                return _Dal.Create(container);
            }
        }
        public bool UpdateContainer(Container container)
        {
           
                return _Dal.Update(container);
           
        }
        public long GetBigContainer()
        {
            return _Dal.GetBigContainer();
        }
        public long GetSmallContainer()
        {
            return _Dal.GetSmallContainer();
        }
    }
}
