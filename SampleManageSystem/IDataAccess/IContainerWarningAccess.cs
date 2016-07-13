using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace IDataAccess
{
     public interface IContainerWarningAccess:IBaseAccess<Model.ContainerWarning>
    {
    
        long  UseBigContainer();
        long  UseSmallContainer();
        int   GetBigContainerWarning();
        int GetSmallContaierWarning();
    }
}
