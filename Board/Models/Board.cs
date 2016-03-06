using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Board.Models
{
    /// <summary>
    /// Доска
    /// </summary>
    public class Board
    {
        public Board()
        {
            Lists = new List<List>();
        }
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
        public bool Archived { get; set; }
        public int Id { get; set; }
        public ICollection<List> Lists { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}