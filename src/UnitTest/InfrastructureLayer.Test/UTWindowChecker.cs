using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackSugar.UnitTests;
using BlackSugar.Entity;
using BlackSugar.Repository;
using Moq;
using System.Windows.Forms;

namespace InfrastructureLayer.Test
{
    [TestClass]
    public class UTWindowChecker: UTLightMock<WindowChecker>
    {
        [TestInitialize]
        public void UTInit()
        {
            LightMockContainer.Clear();
            AddMockContainer<ILogWriter>();

            TestTarget = new WindowChecker(
                GetMock<ILogWriter>().Object);
        }

        [TestMethod]
        public void UTIsExplorerFalse()
        {
            var checker = TestTarget;

            using (var form = new Form())
            {
                var isExplorer = checker.IsExplorer(form.Handle);

                Assert.IsFalse(isExplorer);
            }
        }

        [TestMethod]
        public void UTIsExplorer()
        {
            var checker = TestTarget;

            System.Diagnostics.Process.Start(@"C:\Work");

            System.Threading.Thread.Sleep(3000);

            var getter = new WindowGetter();
            var handle = getter.GetActiveWindowHandle();

            var isExplorer = checker.IsExplorer(handle);

            Assert.IsTrue(isExplorer);

        }
    }
}
