using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDataAccess
{
     public  interface IShelfAccess:IBaseAccess<Model.Shelf>
    {
        IList<Shelf> GetShelf(int  number);
        long GetShelfCount();
        IList<Shelf> GetShelfList();
        bool ExsitsShelfCode(Shelf shelf);
    }
}
