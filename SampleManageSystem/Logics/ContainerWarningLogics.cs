using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Logics
{
   public class ContainerWarningLogics:BaseLogics<Model.ContainerWarning>
    {
        private const string _Type = "ContainerWarningAccess";
        private IDataAccess.IContainerWarningAccess _Dal;

        public ContainerWarningLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IContainerWarningAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public int GetContainerWarning(int id)
        {
            if (_Dal.UseBigContainer()>=(_Dal.GetBigContainerWarning(id)) && _Dal.UseSmallContainer()>=(_Dal.GetSmallContaierWarning(id)) )
            {
                return 1;
            }else if (_Dal.UseBigContainer() <(_Dal.GetBigContainerWarning(id)) && _Dal.UseSmallContainer() >= (_Dal.GetSmallContaierWarning(id)))
            {
                return 2;
            }else if (_Dal.UseBigContainer() >= (_Dal.GetBigContainerWarning(id)) && _Dal.UseSmallContainer() < (_Dal.GetSmallContaierWarning(id)) )
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }
}
