using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using Pente;

namespace PenteTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MainWindow win = new MainWindow();
            Assert.AreEqual(1, win.X());
        }
    }
}
