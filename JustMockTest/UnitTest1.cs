using System;
using JustMockDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace JustMockTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var foo = Mock.Create(()=>new Foo(1));
            Assert.IsNotNull(foo);
        }

        [TestMethod]
        public void ArrangingAMethodCallToReturnACustomValue()
        {
            var foo = Mock.Create<IFoo>();
            Mock.Arrange(() => foo.Bar).Returns(10);
        }


    }
}
