using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;
using Microsoft.AspNet.Identity;

namespace Board.Controllers
{
    public class CommentsController : BaseController<Comment>
    {
        private readonly ICheck<Card> _checkCard; 
        public CommentsController(IBaseRepository<Comment> repository, ICheck<Comment> belongToUser, ICheck<Card> checkCard) : base(repository, belongToUser)
        {
            _checkCard = checkCard;
        }

        public override IHttpActionResult Put(Comment value)
        {
            var userId = User.Identity.GetUserId();
            if (!_checkCard.Check(new Guid(userId), new Card() { Id = value.CardId }))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            return base.Put(value);
        }

        public override IHttpActionResult Post(Comment value)
        {
            var userId = User.Identity.GetUserId();
            if (!_checkCard.Check(new Guid(userId), new Card() { Id = value.CardId }))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            return base.Post(value);
        }
    }
}