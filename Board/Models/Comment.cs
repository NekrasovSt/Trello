using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Board.Interfaces;

namespace Board.Models
{
    public class Comment:IBaseObject
    {
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CardId { get; set; }
    }
}