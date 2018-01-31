using System;
using JustMockDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace JustMockTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var repository = Mock.Create<IBookRepository>();
            var expected=new Book{Tile = "Advantures"};
            var service=new BookService(repository);

            Mock.Arrange(() => repository.GetWhere(book => book.Id == 1)).Returns(expected).MustBeCalled();

            var actual = service.GetSingleBook(1);
            Assert.AreEqual(actual.Tile,expected.Tile);
        }
    }
}
