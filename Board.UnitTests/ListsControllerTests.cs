using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Board.Controllers;
using Board.Interfaces;
using Board.Models;
using Moq;
using NUnit.Framework;
using List = Board.Models.List;

namespace Board.UnitTests
{
    [TestFixture]
    public class ListsControllerTests
    {
        private ListsController _obj;
        private Mock<ICheck<Board.Models.List>> _check;
        private Mock<ICheck<Board.Models.Board>> _boardCheck;
        private Mock<IBaseRepository<Board.Models.List>> _rep;

        [SetUp]
        public void Init()
        {
            _rep = new Mock<IBaseRepository<Board.Models.List>>();
            _check = new Mock<ICheck<Board.Models.List>>();
            _boardCheck = new Mock<ICheck<Board.Models.Board>>();

            var identity = new GenericIdentity("dominik.ernst@xyz123.de");
            //Почему так http://forums.asp.net/t/2028867.aspx?UnitTest+How+to+Mock+User+Identity+GetUserId+
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                "ee8a8b37-ce10-40f5-a26c-e6302d9a3ceb"));
            var principal = new GenericPrincipal(identity, new[] {"user"});

            _obj = new ListsController(_rep.Object, _check.Object, _boardCheck.Object);
            _obj.User = principal;

        }

        [Test]
        public void PutMaxCountWrongTest()
        {
            List card = new List();
            card.MaxCardsCount = 1;
            _check.Setup(i => i.Check(It.IsAny<Guid>(), It.IsAny<List>())).Returns(true);
            _rep.Setup(i => i.Get(It.IsAny<int>())).Returns(() =>
            {
                var obj = new List();
                obj.Cards.Add(new Card());
                obj.Cards.Add(new Card());
                obj.Cards.Add(new Card());
                return obj;
            });
            IHttpActionResult result = _obj.Put(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNotNull(invalid);
            Assert.AreEqual(invalid.ModelState.Values.Count, 1);
            Assert.AreEqual(invalid.ModelState.Values.First().Errors[0].ErrorMessage,
                "Максимальное количество не может быть меньше общего количества уже созданных задач 3");
        }

        [Test]
        public void PutMaxCounOkTest()
        {
            List card = new List();
            card.MaxCardsCount = 5;
            _check.Setup(i => i.Check(It.IsAny<Guid>(), It.IsAny<List>())).Returns(true);
            _rep.Setup(i => i.Get(It.IsAny<int>())).Returns(() =>
            {
                var obj = new List();
                obj.Cards.Add(new Card());
                obj.Cards.Add(new Card());
                obj.Cards.Add(new Card());
                return obj;
            });
            IHttpActionResult result = _obj.Put(card);
            var status = result as OkNegotiatedContentResult<List>;
            Assert.IsNotNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNull(invalid);
        }
    }
}
