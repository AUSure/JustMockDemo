using System;
using System.Collections.Generic;
using JustMockDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace JustMockTest
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void TestMethod1()
        {
            var repository = Mock.Create<IFooRepository>();

            List<Foo> list=new List<Foo>()
            {
                new Foo(1),
                new Foo(2),
                new Foo(3),
                new Foo(4),
                new Foo(5)
            };

            Mock.Arrange(() => repository.GetFoos).Returns(list).MustBeCalled();

            IList<Foo> foos = repository.GetFoos;
            var expected = 5;
            var actual = foos.Count;

            Assert.AreEqual(expected,actual);
            Mock.Assert(repository);
        }
    }
}
