using System;
using System.Collections.Generic;
using System.Web.Http;
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
        // GET api/<controller>
        public IEnumerable<Models.Board> GetList(bool showeAcrhive)
        {
            var id = User.Identity.GetUserId();

            BoardsRepository repository = new BoardsRepository();
            return repository.List(new Guid(id), showeAcrhive);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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
            BoardsRepository repository = new BoardsRepository();
            repository.Insert(value);

            return Ok(value);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]Models.Board value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BoardsRepository repository = new BoardsRepository();
            repository.Update(value);

            return Ok(value);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            BoardsRepository repository = new BoardsRepository();
            var obj = repository.Get(id);
            if (obj == null)
            {
                ModelState.AddModelError("Id","Объект не найден");
                return BadRequest(ModelState);
            }
            if (!obj.Archived)
            {
                ModelState.AddModelError("Archived", "Удалять можно только архивированные записи");
                return BadRequest(ModelState);
            }
            repository.Delete(obj);
            return Ok();
        }
    }
}