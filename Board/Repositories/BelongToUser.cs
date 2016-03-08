using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Board.Dal;

namespace Board.Repositories
{
    public class BelongToUser
    {
        private readonly AppDbContext _context;

        public BelongToUser(AppDbContext context)
        {
            _context = context;
        }

        public bool Check(Guid userId, Models.Board obj)
        {
            return _context.Boards.Any(i => i.UserId == userId && i.Id == obj.Id);
        }
        public bool Check(Guid userId, Models.List obj)
        {
            var boards = _context.Boards.Where(i => i.UserId == userId).Select(i => i.Id);
            return _context.Lists.Any(i => i.Id == obj.Id && boards.Contains(i.BoardId));
        }
        public bool Check(Guid userId, Models.Card obj)
        {
            var boards = _context.Boards.Where(i => i.UserId == userId).Select(i => i.Id);
            var lists = _context.Lists.Where(i => boards.Contains(i.BoardId)).Select(i => i.Id);
            return _context.Cards.Any(i => i.Id == obj.Id && lists.Contains(i.ListId));
        }
    }
}