using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Broad.Models;
using Broad.Repositories;

namespace Broad.Controllers
{
    public class CardsController:ApiController
    {
        public void Put(int id, [FromBody]Card value)
        {
            ListsRepository rep = new ListsRepository();
        }
    }
}