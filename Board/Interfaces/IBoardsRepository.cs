using System;
using System.Linq;

namespace Board.Interfaces
{
    public interface IBoardsRepository:IBaseRepository<Models.Board>
    {
        IQueryable<Models.Board> List(Guid userId, bool showeAcrhive);
    }
}