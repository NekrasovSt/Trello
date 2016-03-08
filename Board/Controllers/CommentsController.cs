using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;

namespace Board.Controllers
{
    public class CommentsController : BaseController<Comment>
    {
        public CommentsController(IBaseRepository<Comment> repository, ICheck<Comment> belongToUser) : base(repository, belongToUser)
        {
        }
    }
}