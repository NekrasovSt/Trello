using System;
using System.Collections.Generic;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;
using Microsoft.AspNet.Identity;
using Board = Board.Models.Board;

namespace Board.Controllers
{
    [Authorize]
    public class ListsController : ParentController<List, Models.Board>
    {
        public ListsController(IBaseRepository<List> repository, ICheck<List> belongToUser, ICheck<Models.Board> belongToUserParent) : base(repository, belongToUser, belongToUserParent)
        {
        }

        public override IHttpActionResult Put(List value)
        {
            var obj = _repository.Get(value.Id);
            if (obj.Cards.Count > value.MaxCardsCount)
            {
                ModelState.AddModelError("MaxCardsCount", "Максимальное количество не может быть меньше общего количества уже созданных задач "+ obj.Cards.Count);
                return BadRequest(ModelState);
            }

            return base.Put(value);
        }

        public IHttpActionResult GetList(int boardId, bool showeAcrhive)
        {
            var id = User.Identity.GetUserId();
            if (!_belongToUserParent.Check(new Guid(id), new Models.Board() { Id = boardId }))
            {
                ModelState.AddModelError("BoardId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            return Ok(_repository.List(boardId, showeAcrhive));
        }
    }
}