using System;
using System.ComponentModel.DataAnnotations;

namespace Board.Models
{
    public class Card
    {
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PlaneDate { get; set; }
        public bool Archived { get; set; }
        public int ListId { get; set; }
        public CardLevel Level { get; set; }
    }
}