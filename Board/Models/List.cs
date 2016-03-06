using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Board.Models
{
    public class List
    {
        public List()
        {
            Cards = new List<Card>();
        }
        public int BoardId { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Archived { get; set; }
        public int Id { get; set; }
        public ICollection<Card> Cards { get; set; } 
    }
}