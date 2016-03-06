using System.Web.Http;
using Board.Models;
using Board.Repositories;

namespace Board.Controllers
{
    public class CardsController:ApiController
    {
        public void Put(int id, [FromBody]Card value)
        {
            ListsRepository rep = new ListsRepository();
        }
    }
}