using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Board.Dal;
using Board.Models;
using Microsoft.Ajax.Utilities;

namespace Board.Repositories
{
    public class BoardsRepository
    {
        //public IEnumerable<Models.Board> List()
        //{
        //    List<Models.Board> list = new List<Models.Board>()
        //    {
        //        new Models.Board() {Name = "Доска1", CreationDate = DateTime.Now, Id = 0},
        //        new Models.Board() {Name = "Доска2", CreationDate = DateTime.Now, Id = 1},
        //        new Models.Board() {Name = "Доска3", CreationDate = DateTime.Now, Id = 2}
        //    };
        //    return list;
        //} 
        AppDbContext _context = new AppDbContext();
        public IQueryable<Models.Board> List(Guid userId, bool showeAcrhive)
        {
            IQueryable<Models.Board> obj;
            if (showeAcrhive)
            {
                obj = _context.Boards.Where(i => i.UserId == userId);
            }
            else
            {
                obj = _context.Boards.Where(i => i.UserId == userId && i.Archived == false);
            }
            return obj.Include("Lists");
        }

        public void Insert(Models.Board model)
        {
            _context.Boards.Add(model);
            _context.SaveChanges();
        }

        public Models.Board Get(int id)
        {
            return _context.Boards.Find(id);
        }

        public void Delete(Models.Board model)
        {
            _context.Cards.RemoveRange(model.Lists.SelectMany(i => i.Cards));
            _context.Lists.RemoveRange(model.Lists);
            _context.Boards.Remove(model);

            _context.SaveChanges();
        }
        public void Update(Models.Board model)
        {
            _context.Boards.Attach(model);
            var entry = _context.Entry(model);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}