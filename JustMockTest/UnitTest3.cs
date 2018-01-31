using System;
using JustMockDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace JustMockTest
{
    [TestClass]
    public class UnitTest3
    {
        /// <summary>
        /// 从构造函数参数中自动排列虚拟属性集
        /// 正如你在上面第一节看到的那样，当你使用时Mock.Create，你可以指定初始化参数传递给创建的对象的构造函数。当构造函数设置包含在你正在模拟的类型中的虚拟属性的值时，可以用Mock.Create同样的方法。结果将是虚拟属性的值将被自动排列。我们来看一个演示这个特性的例子
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var expected = "name";
            var item = Mock.Create<Item>(() => new Item(expected));

            Assert.AreEqual(expected, item.Name);
        }
    }
}
