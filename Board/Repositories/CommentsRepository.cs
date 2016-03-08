using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Board.Dal;

namespace Board.Repositories
{
    public class CommentsRepository
    {
        readonly AppDbContext _context;

        public CommentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Insert(Models.Comment model)
        {
            _context.Comments.Add(model);
            _context.SaveChanges();
        }
    }
}