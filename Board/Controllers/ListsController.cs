using System;
using System.Collections.Generic;
using System.Web.Http;
using Board.Interfaces;
using Board.Models;
using Board.Repositories;
using Microsoft.AspNet.Identity;
using Board = Board.Models.Board;

namespace Board.Controllers
{
    [Authorize]
    public class ListsController : BaseController<List>
    {
        private readonly ICheck<Models.Board> _checkBoard; 
        public ListsController(IBaseRepository<List> repository, ICheck<List> belongToUser, ICheck<Models.Board> checkBoard) : base(repository, belongToUser)
        {
            _checkBoard = checkBoard;
        }

        public IHttpActionResult GetList(int boardId, bool showeAcrhive)
        {
            var id = User.Identity.GetUserId();
            if (!_checkBoard.Check(new Guid(id), new Models.Board(){ Id = boardId }))
            {
                ModelState.AddModelError("BoardId", "Объект не принадлежит пользователю");
                return BadRequest(ModelState);
            }
            return Ok(_repository.List(boardId, showeAcrhive));
        }
    }
}