using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Logics
{
    public class ShelfLogics : BaseLogics<Model.Shelf>
    {
        private const string _Type = "ShelfAccess";
        private IDataAccess.IShelfAccess _Dal;

        public ShelfLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IShelfAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public bool CreateShelf(Shelf shelf)
        {
            if (_Dal.ExsitsShelfCode(shelf))
            {
                return false;
            }
            else
            {
                return _Dal.Create(shelf);
            }
            
        }
        public IList<Shelf> GetShelf(int number)
        {
            return _Dal.GetShelf(number);
        }
        public bool DeleteShelf(int shelfid)
        {
            return _Dal.Delete(shelfid);
        }
    
        public bool UpdateShelf(Shelf shelf)
        {
           
                return _Dal.Update(shelf);
            
            
        }
        public long GetShelfCount()
        {
            return _Dal.GetShelfCount();
        }
        public IList<Shelf> GetShelfList()
        {
            return _Dal.GetShelfList();
        }
    }
}