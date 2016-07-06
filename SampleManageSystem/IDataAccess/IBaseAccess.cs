
namespace IDataAccess
{
    public interface IBaseAccess<T> where T:Model.BaseModel,new()
    {
        bool Create(T model);
        bool Delete(int id);
        bool Disable(int id);
        bool Update(T model);
        T GetModel(int id);
    }
}
