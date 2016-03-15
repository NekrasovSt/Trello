using System;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;
using Microsoft.AspNet.Identity;

namespace Board.Controllers
{
    [Authorize]
    public class CardsController : ParentController<Card, List>
    {
        private readonly IBaseRepository<List> _listRepository;

        public CardsController(IBaseRepository<Card> repository, ICheck<Card> belongToUser, ICheck<List> belongToUserParent, IBaseRepository<List> listRepository) : base(repository, belongToUser, belongToUserParent)
        {
            _listRepository = listRepository;
        }

        public override IHttpActionResult ValidatePost(Card value)
        {
            var result = base.ValidatePost(value);
            if (result != null)
                return result;
            var obj = _listRepository.Get(value.ListId);
            if (obj == null)
            {
                ModelState.AddModelError("ParentId",
                    "Объет не найден");
                return BadRequest(ModelState);
            }
            if (obj.Cards.Count >= obj.MaxCardsCount)
            {
                ModelState.AddModelError("MaxCardsCount",
                    "Нельзя добавлять задачи более ограничения " + obj.MaxCardsCount);
                return BadRequest(ModelState);
            }
            return null;
        }
    }
}