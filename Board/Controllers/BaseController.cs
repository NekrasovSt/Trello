using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Microsoft.AspNet.Identity;

namespace Board.Controllers
{
    [Authorize]
    public class BaseController<T> : ApiController  where T: IBaseObject, new()
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly ICheck<T> _belongToUser;
        public BaseController(IBaseRepository<T> repository, ICheck<T> belongToUser)
        {
            _repository = repository;
            _belongToUser = belongToUser;
        }

        public virtual IHttpActionResult Post([FromBody]T value)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Insert(value);
            return Ok(value);
        }

        // PUT api/<controller>/5
        public virtual IHttpActionResult Put([FromBody]T value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (!_belongToUser.Check(new Guid(userId), value))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            _repository.Update(value);
            return Ok(value);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            if (!_belongToUser.Check(new Guid(userId), new T { Id = id }))
            {
                ModelState.AddModelError("ParentId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            _repository.Delete(new T() { Id = id });
            return Ok();
        }
    }
}