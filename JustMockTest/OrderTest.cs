using System;
using System.Text;
using System.Collections.Generic;
using JustMockDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Core;

namespace JustMockTest
{
    /// <summary>
    /// OrderTest 的摘要说明
    /// </summary>
    [TestClass]
    public class OrderTest
    {

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            var warehouse = Mock.Create<Iwarehouse>();
            var order=new Order("Camera",2);
            bool called = false;
            Mock.Arrange(() => warehouse.HadInventory("Camera", 2)).DoInstead(() => called = true);

            order.Fill(warehouse);

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void CallOriginal_TestMethod()
        {
            var order = Mock.Create<Order>(Behavior.CallOriginal, "Camera", 2);
            Mock.Arrange(() => order.Receipt(DateTime.Today)).CallOriginal();
            Mock.Arrange(() => order.Receipt(Arg.Matches<DateTime>(d => d > DateTime.Today)))
                .Returns("Invalid DateTime");

            var callWithToday = order.Receipt(DateTime.Today);
            var callWithDifferentDay = order.Receipt(DateTime.Today.AddDays(1));

            Assert.AreEqual("Ordered 2 Camera on "+DateTime.Today.ToString("d"),callWithToday);
            Assert.AreEqual("Invalid DateTime",callWithDifferentDay);
        }

        [TestMethod]
        public void DoNothing_TestMethod()
        {
            var warehouse = Mock.Create<Iwarehouse>();
            Mock.ArrangeSet(() => warehouse.Manager = "John").DoNothing();
        }

        [TestMethod]
        public void Throws_TestMethod()
        {
            var order=new Order("Camera",0);
            var warehouse = Mock.Create<Iwarehouse>();
            Mock.Arrange(() => warehouse.HadInventory(Arg.IsAny<string>(), Arg.IsAny<int>())).Returns(true);

            Mock.Arrange(() => warehouse.Remove(Arg.IsAny<string>(), Arg.Matches<int>(x => x == 0)))
                .Throws(new InvalidOperationException());

            order.Fill(warehouse);


            //匹配器
            // Arg.IsInRange()范围匹配器
            //Arg.Matches()匹配表达式
            //foo.Echo(Arg.Matches<int>(x => x < 10)).Returns(true);

        }

        [TestMethod]
        public void MockingProperties_TestMethod()
        {
            var warehouse = Mock.Create<Iwarehouse>();

            Mock.Arrange(() => warehouse.Manager).Returns("John");

            string manager = string.Empty;

            manager = warehouse.Manager;

            Assert.AreEqual("John",manager);
        }

        [TestMethod]
        [ExpectedException(typeof(StrictMockException))]
        public void MockINGpROPERTIES_PropertySet_TestMethod()
        {
            var warehouse = Mock.Create<Iwarehouse>(Behavior.Strict);
            Mock.ArrangeSet(() => warehouse.Manager = "John");
            warehouse.Manager = "Scott";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MockingProperties_PropertySet_TestMethod()
        {
            var warehouse = Mock.Create<Iwarehouse>();
           
            Mock.ArrangeSet(() => warehouse.Manager = "John").Throws<ArgumentException>();

            warehouse.Manager = "Scott";

            warehouse.Manager = "John";
        }

        [TestMethod]
        public void RaisingAnEvent_TestMethod()
        {
            var warehouse = Mock.Create<Iwarehouse>();

            Mock.Arrange(() =>
                    warehouse.Remove(Arg.IsAny<string>(),
                        Arg.IsInRange(int.MinValue, int.MaxValue, RangeKind.Exclusive)))
                .Raises(() => warehouse.ProductRemoved += null
                    , "Camera", 2);
            string productName = string.Empty;
            int quantity = 0;

            warehouse.ProductRemoved += (p, q) =>
            {
                productName = p;
                quantity = q;
            };
            warehouse.Remove(Arg.AnyString,Arg.AnyInt);
            Assert.AreEqual("Camera",productName);
            Assert.AreEqual(2,quantity);

        }

    }
}
