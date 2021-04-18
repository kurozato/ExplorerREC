using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackSugar.UnitTests;
using BlackSugar.Entity;
using BlackSugar.Repository;
using BlackSugar.Service;
using Moq;

namespace DomainLayer.Test
{
    [TestClass]
    public class UTColorService : UTLightMock<ColorService>
    {
        [TestInitialize]
        public void UTInit()
        {
            LightMockContainer.Clear();
            AddMockContainer<IGeneralSetting>();
            AddMockContainer<IFileReader>();
            AddMockContainer<IFileWriter>();

            TestTarget = new ColorService(
                GetMock<IGeneralSetting>().Object,
                GetMock<IFileWriter>().Object,
                GetMock<IFileReader>().Object);
        }

        [TestMethod]
        public void UTGetColorInfo1()
        {
            var service = TestTarget;

            var exp = service.GetColorInfo(ColorTheme.Dark, GradationPattern.Orange);

            foreach(GradationPattern pattern in Enum.GetValues(typeof(GradationPattern)))
            {
                var actual = service.GetColorInfo(ColorTheme.Dark, pattern);
                if (pattern != GradationPattern.Orange)
                    Assert.AreNotEqual(exp, actual);
            }

        }

        [TestMethod]
        public void UTGetColorInfo2()
        {
            var service = TestTarget;
            var exp = service.GetColorInfo(ColorTheme.Dark, GradationPattern.Blue);

            var mockReader = GetMock<IFileReader>();
            mockReader.Setup(m => m.Read<ColorInfo>(It.IsAny<string>())).Returns(exp);

            var actual = service.GetColorInfo();

            Assert.AreEqual(exp, actual);
        }

        [TestMethod]
        public void UTGetGradationPatterns()
        {
            var service = TestTarget;

            var patterns = service.GetGradationPatterns();

            Assert.AreEqual(patterns.Count, 5);

        }
        [TestMethod]
        public void UTGetColorThemes()
        {
            var service = TestTarget;

            var themes = service.GetColorThemes();

            Assert.AreEqual(themes.Count, 2);
        }
    }
}
