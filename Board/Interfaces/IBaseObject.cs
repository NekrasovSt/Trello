using System;

namespace Board.Interfaces
{
    public interface IBaseObject
    {
        /// <summary>
        /// ���� ��������
        /// </summary>
        DateTime CreationDate { get; set; }

        int Id { get; set; }
    }
}