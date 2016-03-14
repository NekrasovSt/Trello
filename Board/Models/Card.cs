using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Board.Interfaces;

namespace Board.Models
{
    public class Card : IBaseObject, IParent
    {
        public Card()
        {
            Comments = new List<Comment>();
        }

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
        [NotMapped]
        public int ParentId { get { return ListId; } }
        public CardLevel Level { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}