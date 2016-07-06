using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Logics
{
     public class ProjectTemplateLogics:BaseLogics<Model.ProjectTemplate>
    {
        private const string _Type = "ProjectTemplateAccess";
        private IDataAccess.IProjectTemplateAccess _Dal;

        public ProjectTemplateLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IProjectTemplateAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public IList<ProjectTemplate> GetProjectTemplateList()
        {
            return _Dal.GetProjectTemplateList();
        }
    }
}
