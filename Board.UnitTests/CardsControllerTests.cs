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
        [SetUp]
        public void Init()
        {
            Mock<IBaseRepository<Card>> rep = new Mock<IBaseRepository<Card>>();
             _check = new Mock<ICheck<Card>>();
            //Guid right = new Guid();
            //check.Setup(i=>i.Check())
            //Mock<IPrincipal> principial = new Mock<IPrincipal>();
            //Mock<IIdentity> identity = new Mock<IIdentity>();

            var identity = new GenericIdentity("dominik.ernst@xyz123.de");
            //Почему так http://forums.asp.net/t/2028867.aspx?UnitTest+How+to+Mock+User+Identity+GetUserId+
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "ee8a8b37-ce10-40f5-a26c-e6302d9a3ceb"));
            var principal = new GenericPrincipal(identity, new[] { "user" });


            //identity.Setup(x => x.Name).Returns("test_name");

            //principial.SetupGet(i => i.Identity).Returns(identity.Object);

            _obj = new CardsController(rep.Object, _check.Object);
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
            Card card = new Card();
            IHttpActionResult result = _obj.Post(card);
            var status = result as OkNegotiatedContentResult<Card>;
            Assert.IsNotNull(status);
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
