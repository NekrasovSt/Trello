using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board.Interfaces
{
    public interface ICheck<T>
    {
        bool Check(Guid userId, T obj);
    }
}