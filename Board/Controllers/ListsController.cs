using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Broad.Models;
using Broad.Repositories;

namespace Broad.Controllers
{
    public class ListsController : ApiController
    {
        public IEnumerable<List> GetListByParent(int parentId, int last = -1)
        {
            ListsRepository rep = new ListsRepository();

            return rep.List();
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
        public void Post([FromBody]string value)
        {
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