using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class NewBoard
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}