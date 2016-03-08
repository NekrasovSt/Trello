using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Board.Models;
using Board.Repositories;

namespace Board.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly CommentsRepository _rep;

        public CommentsController(CommentsRepository rep)
        {
            _rep = rep;
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Comment value)
        {
            value.CreationDate = DateTime.Now;
            if (string.IsNullOrEmpty(value.Description))
            {
                return BadRequest(ModelState);
            }
            
            _rep.Insert(value);
            return Ok(value);
        }
    }
}