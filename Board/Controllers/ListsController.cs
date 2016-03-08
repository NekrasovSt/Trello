using System;
using System.Collections.Generic;
using System.Web.Http;
using Board.Models;
using Board.Repositories;
using Microsoft.AspNet.Identity;
using Board = Board.Models.Board;

namespace Board.Controllers
{
    [Authorize]
    public class ListsController : ApiController
    {
        private readonly ListsRepository _rep;
        private readonly BelongToUser _belongToUser;
        public ListsController(ListsRepository rep, BelongToUser belongToUser)
        {
            _rep = rep;
            _belongToUser = belongToUser;
        }

        public IHttpActionResult GetList(int boardId, bool showeAcrhive)
        {
            var id = User.Identity.GetUserId();
            if (!_belongToUser.Check(new Guid(id), new Models.Board(){ Id = boardId }))
            {
                ModelState.AddModelError("BoardId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            return Ok(_rep.List(boardId, showeAcrhive));
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]List value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = User.Identity.GetUserId();
            _rep.Insert(value);
            return Ok(value);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]List value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (!_belongToUser.Check(new Guid(userId), value))
            {
                ModelState.AddModelError("BoardId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            _rep.Update(value);
            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            if (!_belongToUser.Check(new Guid(userId), new Models.List() { Id = id }))
            {
                ModelState.AddModelError("BoardId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            _rep.Delete(id);
            return Ok();
        }
    }
}