
namespace Logics
{
    public class BaseLogics<T> where T : Model.BaseModel, new()
    {
        protected IDataAccess.IBaseAccess<T> dal;
        public BaseLogics(string type)
        {
            dal =  FactoryDataAccess.DataAccess<IDataAccess.IBaseAccess<T>>.CreateDAL(type);
        }
        public virtual bool Add(T model)
        {
            return dal.Create(model);
        }
        public virtual bool Delete(int ID)
        {
            return dal.Delete(ID);
        }
        public virtual bool Update(T model)
        {
            return dal.Update(model);
        }
        public virtual T GetModel(int ID)
        {
            return dal.GetModel(ID);
        }
    }
}
