using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Board.Interfaces;

namespace Board.Models
{
    public class Comment:IBaseObject, IParent
    {
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CardId { get; set; }
        [NotMapped]
        public int ParentId { get { return CardId; }}
    }
}