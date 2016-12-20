using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace IDataAccess
{
    public interface ITaskContainerAccess:IBaseAccess<Model.TaskContainer>
    {
        bool CreateTaskContainer(int taskid, int taskContainerId);

        IList<TaskContainer> GetTaskContainerList();
    }
}
