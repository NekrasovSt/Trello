using System;
using System.Web.Http;
using Board.Models;
using Board.Repositories;

namespace Board.Controllers
{
    [Authorize]
    public class CardsController:ApiController
    {
        private readonly CardRepository _rep;
        public CardsController(CardRepository rep)
        {
            _rep = rep;
        }

        public IHttpActionResult Post([FromBody]Card value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _rep.Insert(value);
            return Ok(value);
        }
        public IHttpActionResult Put([FromBody]Models.Card value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _rep.Update(value);

            return Ok(value);
        }
    }
}