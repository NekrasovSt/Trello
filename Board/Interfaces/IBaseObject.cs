using System;

namespace Board.Interfaces
{
    public interface IBaseObject
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        DateTime CreationDate { get; set; }

        int Id { get; set; }
    }
}