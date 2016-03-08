using System.Dynamic;

namespace Board.Interfaces
{
    public interface IBaseRepository<T>
    {
        T Get(int id);
        void Insert(T obj);
        void Delete(T obj);
        void Update(T obj);
    }
}