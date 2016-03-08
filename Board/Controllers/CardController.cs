using System;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;

namespace Board.Controllers
{
    [Authorize]
    public class CardsController:BaseController<Card>
    {
        public CardsController(IBaseRepository<Card> repository, ICheck<Card> belongToUser) : base(repository, belongToUser)
        {
        }
    }
}