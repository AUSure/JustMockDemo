using System;
using JustMockDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace JustMockTest
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestMethod1()
        {
            var foo = Mock.Create<IFoo>();
            Mock.Arrange(() => foo.ToString()).MustBeCalled();

            foo.ToString();
            Mock.Assert(foo);

        }

        [TestMethod]
        public void TestMethodShowingAssertFunctionalityOnPropGet()
        {
            var foo = Mock.Create<IFoo>();

            Mock.Arrange(() => foo.Bar).Returns(10);

            var returnValue = foo.Bar;

            Assert.AreEqual(10,returnValue);
        }

        [TestMethod]
        public void TestMethodShowingAssertFunctionalityOnPropSet()
        {
            var foo = Mock.Create<IFoo>();
            Mock.ArrangeSet(() => foo.Bar = 0).MustBeCalled();

            foo.Bar = 0;

            Mock.Assert(foo);
        }
    }
}
