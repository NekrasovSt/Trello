using System.Dynamic;
using System.Linq;

namespace Board.Interfaces
{
    public interface IBaseRepository<T>
    {
        T Get(int id);
        void Insert(T obj);
        void Delete(T obj);
        void Update(T obj);
        IQueryable<T> List(int parentId, bool showeAcrhive);
    }
}