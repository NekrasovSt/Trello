using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Broad.Models;
using Broad.Repositories;

namespace Broad.Controllers
{
    //[RoutePrefix("api/{controller}/{action}")]
    [Authorize]
    public class BoardsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Board> GetList(int last = -1)
        {
            var identity = User.Identity as ClaimsIdentity;
            BoardsRepository repository = new BoardsRepository();
            return repository.List();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}