using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Board.Interfaces;
using Microsoft.AspNet.Identity;

namespace Board.Controllers
{
    public class ParentController<T, TParent> : BaseController<T> where T : IBaseObject, IParent, new() where TParent : IBaseObject, new()
    {
        protected readonly ICheck<TParent> _belongToUserParent;
        public ParentController(IBaseRepository<T> repository, ICheck<T> belongToUser, ICheck<TParent> belongToUserParent) : base(repository, belongToUser)
        {
            _belongToUserParent = belongToUserParent;
        }

        public override IHttpActionResult Put(T value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (!_belongToUserParent.Check(new Guid(userId), new TParent() { Id = value.ParentId }))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            if (!_belongToUser.Check(new Guid(userId), value))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            _repository.Update(value);
            return Ok(value);
        }

        public override IHttpActionResult Post(T value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (!_belongToUserParent.Check(new Guid(userId), new TParent() { Id = value.ParentId }))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            _repository.Insert(value);
            return Ok(value);
        }
    }
}