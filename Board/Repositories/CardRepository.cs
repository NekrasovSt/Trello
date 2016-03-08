using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Board.Dal;
using Board.Interfaces;
using Board.Models;

namespace Board.Repositories
{
    public class CardRepository: IBaseRepository<Models.Card>
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public Card Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Models.Card model)
        {
            _context.Cards.Add(model);
            _context.SaveChanges();
        }

        public void Delete(Card obj)
        {
            throw new NotImplementedException();
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

        public IQueryable<Card> List(int parentId, bool showeAcrhive)
        {
            IQueryable<Models.Card> obj;
            if (showeAcrhive)
            {
                obj = _context.Cards.Where(i => i.ListId == parentId);
            }
            else
            {
                obj = _context.Cards.Where(i => i.ListId == parentId && i.Archived == false);
            }
            return obj;
        }
    }
}