using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackSugar.UnitTests;
using BlackSugar.Entity;
using BlackSugar.Repository;
using Moq;

namespace InfrastructureLayer.Test
{
    [TestClass]
    public class UTFileReader : UTLightMock<FileReader>
    {
        [TestInitialize]
        public void UTInit()
        {
            TestTarget = new FileReader();
        }

        [TestMethod]
        public void UTRead()
        {
            var reader = TestTarget;
            var writer = new FileWriter();
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ut.result.txt");

            dynamic obj = new { message = "TEST" };
            writer.Write(path, obj);

            var actual = reader.Read<dynamic>(path);

            Assert.AreEqual(actual.message.ToString(), obj.message.ToString());

            System.IO.File.Delete(path);
        }
    }
}
