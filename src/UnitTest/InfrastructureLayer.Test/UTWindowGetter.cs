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
    public class UTWindowGetter : UTLightMock<WindowGetter>
    {
        [TestInitialize]
        public void UTInit()
        {
            LightMockContainer.Clear();
            AddMockContainer<ILogWriter>();

            TestTarget = new WindowGetter();
        }

        [TestMethod]
        public void UTGetActiveWindowHandle()
        {
            var getter = TestTarget;
            using (var form = new Form())
            {
                form.Show();
                System.Threading.Thread.Sleep(1000);
                var handle = getter.GetActiveWindowHandle();

                Assert.AreEqual(form.Handle, handle);

                form.Close();
            }
        }

        [TestMethod]
        public void UTGetExplorerWindow()
        {
            var getter = TestTarget;

            System.Diagnostics.Process.Start(@"C:\Work");

            System.Threading.Thread.Sleep(1000);
            var handle = getter.GetActiveWindowHandle();

            var explorer = getter.GetExplorerWindow(handle);

            Assert.AreEqual(explorer.Path, @"C:\Work");

        }

        [TestMethod]
        public void UTGetExplorerWindows()
        {
            var getter = TestTarget;

            System.Diagnostics.Process.Start(@"C:\Work");
            System.Threading.Thread.Sleep(1000);
            System.Diagnostics.Process.Start(@"C:\Work\GitHub");
            System.Threading.Thread.Sleep(1000);
            System.Diagnostics.Process.Start(@"C:\Work\nuget");
            System.Threading.Thread.Sleep(1000);
            System.Diagnostics.Process.Start(@"\\desktop-40cqhqh\Shared");

            System.Threading.Thread.Sleep(1000);

            var explorers = getter.GetExplorerWindows();

            Assert.AreEqual(explorers[3].Path, @"\\desktop-40cqhqh\Shared");
        }
    }
}
