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
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using List = Board.Models.List;

namespace Board.UnitTests
{
    /// <summary>
    /// Тестирование базового контролера
    /// </summary>
    [TestFixture]
    public class CardsControllerTests
    {
        private CardsController _obj;
        private Mock<ICheck<Card>> _check;
        private Mock<ICheck<List>> _checkList;
        private Mock<IBaseRepository<List>> _repList;

        [SetUp]
        public void Init()
        {
            Mock<IBaseRepository<Card>> rep = new Mock<IBaseRepository<Card>>();
             _repList = new Mock<IBaseRepository<List>>();
            _check = new Mock<ICheck<Card>>();
            _checkList = new Mock<ICheck<List>>();

            var identity = new GenericIdentity("dominik.ernst@xyz123.de");
            //Почему так http://forums.asp.net/t/2028867.aspx?UnitTest+How+to+Mock+User+Identity+GetUserId+
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "ee8a8b37-ce10-40f5-a26c-e6302d9a3ceb"));
            var principal = new GenericPrincipal(identity, new[] { "user" });

            _obj = new CardsController(rep.Object, _check.Object, _repList.Object, _checkList.Object);
            _obj.User = principal;

        }
        [Test(Description = "Не валидная модель")]
        public void PostInvalidModelTest()
        {
            Card card = new Card();
            _obj.ModelState.AddModelError("Test", "Error");
            IHttpActionResult result = _obj.Post(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNotNull(invalid);
        }
        [Test]
        public void PostOkTest()
        {
            _repList.Setup(i => i.Get(It.IsAny<int>())).Returns(() =>
            {
                var obj = new List();
                obj.MaxCardsCount = 3;
                return obj;
            });
            Card card = new Card();
            IHttpActionResult result = _obj.Post(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNotNull(status);
        }
        [Test(Description = "Превыщение MaxCardsCount")]
        public void PostWrongMaxCount()
        {
            _repList.Setup(i => i.Get(It.IsAny<int>())).Returns(() =>
            {
                var obj = new List();
                obj.Cards.Add(new Card());
                obj.MaxCardsCount = 1;
                return obj;
            });
            _check.Setup(i => i.Check(It.IsAny<Guid>(), It.IsAny<Card>())).Returns(true);
            Card card = new Card();
            IHttpActionResult result = _obj.Post(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNotNull(invalid);
            Assert.AreEqual(invalid.ModelState.Values.Count, 1);
            Assert.AreEqual(invalid.ModelState.Values.First().Errors[0].ErrorMessage, "Нельзя добавлять задачи более ограничения 1");
        }
        [Test(Description = "Не валидная модель")]
        public void PutInvalidModelTest()
        {
            Card card = new Card();
            _obj.ModelState.AddModelError("Test", "Error");
            IHttpActionResult result = _obj.Put(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNotNull(invalid);
        }
        [Test(Description = "Не принадлежит пользователю")]
        public void PutWrongUserModelTest()
        {
            Card card = new Card();
            _check.Setup(i => i.Check(It.IsAny<Guid>(), It.IsAny<Card>())).Returns(false);

            IHttpActionResult result = _obj.Put(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNotNull(invalid);
            Assert.AreEqual(invalid.ModelState.Values.Count, 1);
            Assert.AreEqual(invalid.ModelState.Values.First().Errors[0].ErrorMessage, "Объект не принадлежит пользователю");
        }
        

        [Test]
        public void PutOkTest()
        {
            Card card = new Card();
            _check.Setup(i => i.Check(It.IsAny<Guid>(), It.IsAny<Card>())).Returns(true);

            IHttpActionResult result = _obj.Put(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNotNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNull(invalid);

        }
        [Test(Description = "Не валидная модель")]
        public void DeleteInvalidModelTest()
        {
            _obj.ModelState.AddModelError("Test", "Error");
            IHttpActionResult result = _obj.Delete(10);
            var status = result as OkResult;
            Assert.IsNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNotNull(invalid);
        }
        [Test(Description = "Не принадлежит пользователю")]
        public void DeleteWrongUserModelTest()
        {
            _check.Setup(i => i.Check(It.IsAny<Guid>(), It.IsAny<Card>())).Returns(false);

            IHttpActionResult result = _obj.Delete(10);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNotNull(invalid);
            Assert.AreEqual(invalid.ModelState.Values.Count, 1);
            Assert.AreEqual(invalid.ModelState.Values.First().Errors[0].ErrorMessage, "Объект не принадлежит пользователю");
        }
        [Test]
        public void DeleteOkTest()
        {
            _check.Setup(i => i.Check(It.IsAny<Guid>(), It.IsAny<Card>())).Returns(true);

            IHttpActionResult result = _obj.Delete(10);
            var status = result as OkResult;
            Assert.IsNotNull(status);
            var invalid = result as InvalidModelStateResult;
            Assert.IsNull(invalid);

        }
    }
}
