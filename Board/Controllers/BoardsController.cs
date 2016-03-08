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
    //[RoutePrefix("api/{controller}/{action}")]
    [Authorize]
    public class BoardsController : ApiController
    {
        private readonly IBoardsRepository _repository;
        private BelongToUser _belongToUser;

        public BoardsController(IBoardsRepository repository, BelongToUser belongToUser)
        {
            _repository = repository;
            _belongToUser = belongToUser;
        }

        // GET api/<controller>
        public IEnumerable<Models.Board> GetList(bool showeAcrhive)
        {
            var id = User.Identity.GetUserId();
            
            return _repository.List(new Guid(id), showeAcrhive);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]NewBoard obj)
        {
            Models.Board value = new Models.Board
            {
                Name = obj.Name,
                UserId = new Guid(User.Identity.GetUserId()),
                CreationDate = DateTime.Now
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Insert(value);

            return Ok(value);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]Models.Board value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = User.Identity.GetUserId();
            if (!_belongToUser.Check(new Guid(id), value))
            {
                ModelState.AddModelError("UserId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            _repository.Update(value);

            return Ok(value);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var obj = _repository.Get(id);
            if (obj == null)
            {
                ModelState.AddModelError("Id", "Объект не найден");
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (!_belongToUser.Check(new Guid(userId), obj))
            {
                ModelState.AddModelError("UserId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            if (!obj.Archived)
            {
                ModelState.AddModelError("Archived", "Удалять можно только архивированные записи");
                return BadRequest(ModelState);
            }
            _repository.Delete(obj);
            return Ok();
        }
    }
}