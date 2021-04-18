using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackSugar.UnitTests;
using BlackSugar.Entity;
using BlackSugar.Repository;
using Moq;

namespace InfrastructureLayer.Test
{
    [TestClass]
    public class UTFileWriter : UTLightMock<FileWriter>
    {
        [TestInitialize]
        public void UTInit()
        {
            TestTarget = new FileWriter();
        }

        [TestMethod]
        public void UTWrite()
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ut.result.txt");

            var writer = TestTarget;
            object obj = new{ message = "TEST" };

            writer.Write(path, obj);

            Assert.IsTrue(System.IO.File.Exists(path));

            System.IO.File.Delete(path);
        }
    }
}
