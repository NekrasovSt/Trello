using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Broad.Models
{
    public class List
    {
        public List()
        {
            Cards = new List<Card>();
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Archived { get; set; }
        public int Id { get; set; }
        public ICollection<Card> Cards { get; set; } 
    }
}