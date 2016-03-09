using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Board.Interfaces;

namespace Board.Models
{
    public class List:IBaseObject
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
        /// <summary>
        /// Максимальное количество задач
        /// </summary>
        [Range(1,999)]
        public int MaxCardsCount { get; set; }
    }
}