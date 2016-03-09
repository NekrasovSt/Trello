using System;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;

namespace Board.Controllers
{
    [Authorize]
    public class CardsController:BaseController<Card>
    {
        private readonly IBaseRepository<List> _listRepository;

        public CardsController(IBaseRepository<Card> repository, ICheck<Card> belongToUser, IBaseRepository<List> listRepository) : base(repository, belongToUser)
        {
            _listRepository = listRepository;
        }

        public override IHttpActionResult Post(Card value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var obj = _listRepository.Get(value.ListId);
            if (obj.Cards.Count >= obj.MaxCardsCount)
            {
                ModelState.AddModelError("MaxCardsCount",
                    "Нельзя добавлять задачи более ограничения " + obj.MaxCardsCount);
                return BadRequest(ModelState);
            }
            _repository.Insert(value);
            return Ok(value);
        }
    }
}