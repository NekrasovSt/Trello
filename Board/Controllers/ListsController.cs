using System;
using System.Collections.Generic;
using System.Web.Http;
using Board.Models;
using Board.Repositories;
using Microsoft.AspNet.Identity;

namespace Board.Controllers
{
    [Authorize]
    public class ListsController : ApiController
    {
        public IHttpActionResult GetList(int boardId, bool showeAcrhive)
        {
            ListsRepository rep = new ListsRepository();
            var id = User.Identity.GetUserId();
            if (!rep.CheckBelong(new Guid(id), boardId))
            {
                return BadRequest(ModelState);
            }
            return Ok(rep.List(boardId, showeAcrhive));
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]List value)
        {
            ListsRepository rep = new ListsRepository();
            value.CreationDate = DateTime.Now;
            rep.Insert(value);
            return Ok(value);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]List value)
        {
            ListsRepository rep = new ListsRepository();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}