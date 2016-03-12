using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Board.Dal;
using Board.Interfaces;
using Board.Models;
using Newtonsoft.Json.Linq;

namespace Board.Repositories
{
    public class ListsRepository : IBaseRepository<Models.List>
    {
        readonly AppDbContext _context;

        public ListsRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckBelong(Guid userId, int boardId)
        {
            return _context.Boards.FirstOrDefault(i => i.UserId == userId && i.Id == boardId) != null;
        }
        public bool CheckBelongList(Guid userId, int listId)
        {
            var boards = _context.Boards.Where(i => i.UserId == userId).Select(i => i.Id);
            return _context.Lists.FirstOrDefault(i => i.Id == listId && boards.Contains(i.BoardId)) != null;
        }
        public IQueryable<Models.List> List(int boardId, bool showeAcrhive)
        {
            if (showeAcrhive)
            {
                return _context.Lists.Where(i => i.Id == boardId);
            }
            else
            {
                return _context.Lists.Where(i => i.Id == boardId && i.Archived == false);
            }

        }

        public void Delete(List obj)
        {
            var model = Get(obj.Id);
            _context.Comments.RemoveRange(model.Cards.SelectMany(i=>i.Comments));
            _context.Cards.RemoveRange(model.Cards);

            _context.Lists.Remove(model);

            _context.SaveChanges();
        }

        public void Update(Models.List model)
        {
            //_context.Lists.Attach(model);
            var original = _context.Lists.Find(model.Id);

            if (original != null)
            {
                _context.Entry(original).CurrentValues.SetValues(model);
                _context.SaveChanges();
            }
            _context.SaveChanges();
        }

        public List Get(int id)
        {
            return _context.Lists.Find(id);
        }

        public void Insert(Models.List model)
        {
            _context.Lists.Add(model);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var model = _context.Lists.Find(id);
            _context.Cards.RemoveRange(model.Cards);
            _context.Lists.Remove(model);

            _context.SaveChanges();
        }
        //public IEnumerable<List> List()
        //{
        //    List<List> list = new List<List>()
        //    {
        //        new List() {Name = "Список1", CreationDate = DateTime.Now, Id = 0},
        //        new List() {Name = "Список2", CreationDate = DateTime.Now, Id = 1},
        //        new List() {Name = "Список3", CreationDate = DateTime.Now, Id = 2},
        //        new List() {Name = "Список4", CreationDate = DateTime.Now, Id = 3},
        //        new List() {Name = "Список5", CreationDate = DateTime.Now, Id = 4}
        //    };
        //    list[0].Cards.Add(new Card()
        //    {
        //        Id = 0,
        //        CreationDate = DateTime.Now,
        //        PlaneDate = DateTime.Now,
        //        Name = "Задача по переносу контролов, в админке",
        //        Level = CardLevel.Danger
        //    });
        //    list[0].Cards.Add(new Card()
        //    {
        //        Id = 1,
        //        CreationDate = DateTime.Now,
        //        PlaneDate = DateTime.Now,
        //        Name = "Задача2",
        //        Level = CardLevel.Info
        //    });
        //    list[0].Cards.Add(new Card()
        //    {
        //        Id = 2,
        //        CreationDate = DateTime.Now,
        //        PlaneDate = DateTime.Now,
        //        Name = "Задача3",
        //        Level = CardLevel.Warning
        //    });
        //    return list;
        //}
    }
}