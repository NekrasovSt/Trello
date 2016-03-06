using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Broad.Models;

namespace Broad.Repositories
{
    public class BoardsRepository
    {
        public IEnumerable<Board> List()
        {
            List<Board> list = new List<Board>()
            {
                new Board() {Name = "Доска1", CreationDate = DateTime.Now, Id = 0},
                new Board() {Name = "Доска2", CreationDate = DateTime.Now, Id = 1},
                new Board() {Name = "Доска3", CreationDate = DateTime.Now, Id = 2}
            };
            return list;
        } 
    }
}