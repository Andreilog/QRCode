using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QrCodeWebApi.Tests
{
    [TestClass]
    public class UrlItemsRepositoryTests
    {
        [TestMethod]
        public void GetAllIsNotNull()
        {
            //arrange
            var sut = new UrlItemsRepository();

            //act
            sut.GetAll();

            //assert
            Assert.AreNotEqual(sut.GetAll(), null);
        }

        [TestMethod]
        public void GetAllHas2Elements()
        {
            //arrange
            var sut = new UrlItemsRepository();

            //act
            sut.GetAll();

            //assert
            Assert.AreEqual(sut.GetAll()?.Count, 4);
        }

    }
}
