using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Board.Dal;

namespace Board.Repositories
{
    public class CardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Insert(Models.Card model)
        {
            _context.Cards.Add(model);
            _context.SaveChanges();
        }
        public void Update(Models.Card model)
        {
            var original = _context.Cards.Find(model.Id);

            if (original != null)
            {
                _context.Entry(original).CurrentValues.SetValues(model);
                _context.SaveChanges();
            }
            _context.SaveChanges();
        }
    }
}