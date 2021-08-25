using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlackSugar.UnitTests;
using BlackSugar.Entity;
using BlackSugar.Repository;
using Moq;
using System.Windows.Forms;

namespace InfrastructureLayer.Test
{
    [TestClass]
    public class UTHotKeyNotifier : UTLightMock<HotKeyNotifier>
    {
        [TestInitialize]
        public void UTInit()
        {
            LightMockContainer.Clear();
            AddMockContainer<ILogWriter>();

            TestTarget = new HotKeyNotifier();
        }

        [TestMethod]
        public void UTInitialize()
        {
            var notifier = TestTarget;
            using (var form = new Form())
            {
                notifier.Initialize(IntPtr.Zero);

                Assert.IsTrue(notifier.RegistKeys);
            }
        }

        [TestMethod]
        public void UTRegist()
        {
            var notifier = TestTarget;
            using (var form = new Form())
            {
                notifier.Initialize(form.Handle);
                notifier.Regist(() => { });

                Assert.IsTrue(notifier.RegistKeys);
            }
        }

        [TestMethod]
        public void UTRelease()
        {
            var notifier = TestTarget;
            using (var form = new Form())
            {
                notifier.Initialize(form.Handle);
                notifier.Release();

                Assert.IsFalse(notifier.RegistKeys);
            }
        }
    }
}
