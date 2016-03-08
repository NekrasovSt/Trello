using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Board.Dal;
using Board.Interfaces;
using Board.Models;

namespace Board.Repositories
{
    public class CommentsRepository:IBaseRepository<Models.Comment>
    {
        readonly AppDbContext _context;

        public CommentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public Comment Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Models.Comment model)
        {
            _context.Comments.Add(model);
            _context.SaveChanges();
        }

        public void Delete(Comment obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Comment obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> List(int parentId, bool showeAcrhive)
        {
            throw new NotImplementedException();
        }
    }
}