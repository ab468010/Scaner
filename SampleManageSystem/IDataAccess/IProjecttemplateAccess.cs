using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace IDataAccess
{
      public  interface IProjectTemplateAccess:IBaseAccess<Model.ProjectTemplate>
    {
        IList<ProjectTemplate> GetProjectTemplateList();
    }
}
