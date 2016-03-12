using System;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;
using Microsoft.AspNet.Identity;

namespace Board.Controllers
{
    [Authorize]
    public class CardsController : BaseController<Card>
    {
        private readonly IBaseRepository<List> _listRepository;
        private readonly ICheck<List> _checkList;

        public CardsController(IBaseRepository<Card> repository, ICheck<Card> belongToUser, IBaseRepository<List> listRepository, ICheck<List> checkList) : base(repository, belongToUser)
        {
            _listRepository = listRepository;
            _checkList = checkList;
        }

        public override IHttpActionResult Post(Card value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (_checkList.Check(new Guid(userId), new List() { Id = value.Id }))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
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