using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Logics
{
    public class ModuleLogics:BaseLogics<Model.Module>
    {
        private const string _Type = "ModuleAccess";
        private IDataAccess.IModuleAccess _Dal;

        public ModuleLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IModuleAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public IList<Module> GetModuleList(int roleId)
        {
            return _Dal.GetModuleList(roleId);
        }
   
    }
}
