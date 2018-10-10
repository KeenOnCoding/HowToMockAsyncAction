using HowToMockAsyncAction.Controllers;
using HowToMockAsyncAction.Infrastructure;
using HowToMockAsyncAction.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HowToMockAsyncAction.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Index_Action()
        {
            // Arrange 
            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();

            mock.Setup(m => m.Persons).Returns(new Person[]
            {
                new Person {Id = 1, Name = "P1"},
                new Person {Id = 2, Name = "P2"},
                new Person {Id = 3, Name = "P3"},
                new Person {Id = 4, Name = "P4"},
                new Person {Id = 5, Name = "P5"}
            }.AsQueryable());

            // Arrange 
            var controller = new PeopleController(mock.Object);

            // Act 
            var request = ((controller.Index() as ViewResult).ViewData.Model) as IEnumerable<Person>;
            var result = request.ToArray();

            // Assert 
            Assert.AreEqual(1, result[0].Id);
        }

        [TestMethod]
        public async Task Test_Async_List_Action()
        {
            var _userRepository = new Mock<IPersonRepository>();
            var _service = new PeopleController(_userRepository.Object);

            var users = new List<Person>()
            {
                new Person {Id = 1, Name = "P1"},
                new Person {Id = 2, Name = "P2"},
                new Person {Id = 3, Name = "P3"},
                new Person {Id = 4, Name = "P4"},
                new Person {Id = 5, Name = "P5"}
            };

            var mock = users.AsQueryable().BuildMock();

            _userRepository.Setup(x => x.Persons).Returns(mock.Object);

            //var result = (((await _service.List() as ViewResult).ViewData.Model) as IEnumerable<Person>).ToList();

            var result = GetViewModel<IEnumerable<Person>>(await _service.List())?.ToList();
            
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
            Assert.AreEqual("P4", result[3].Name);
            Assert.AreEqual("P5", result[4].Name);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
