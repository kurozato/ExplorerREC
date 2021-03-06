using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackSugar.UnitTests;
using BlackSugar.Entity;
using BlackSugar.Repository;
using BlackSugar.Service;
using TargetResources = BlackSugar.Properties.Resources;
using Moq;

namespace DomainLayer.Test
{
    [TestClass]
    public class UTNotifyService : UTLightMock<NotifyService>
    {
        [TestInitialize]
        public void UTInit()
        {
            LightMockContainer.Clear();
            AddMockContainer<IWindowChecker>();
            AddMockContainer<IWindowGetter>();
            AddMockContainer<IAutomationRegister>();
            AddMockContainer<IDbCommander>();
            AddMockContainer<IHotKeyNotifier>();
            AddMockContainer<IGeneralSetting>();


            TestTarget = new NotifyService(
                GetMock<IWindowChecker>().Object,
                GetMock<IWindowGetter>().Object,
                GetMock<IAutomationRegister>().Object,
                GetMock<IDbCommander>().Object,
                GetMock<IHotKeyNotifier>().Object,
                GetMock<IGeneralSetting>().Object);
        }

        [TestMethod]
        public void UTCheckWindow()
        {
            var service = TestTarget;
            var mockWindowGetter = GetMock<IWindowGetter>();
            var mockWindowChecker = GetMock<IWindowChecker>();
            var mockAutomationRegister = GetMock<IAutomationRegister>();

            var handle = IntPtr.Zero;
            mockWindowGetter.Setup(x => x.GetActiveWindowHandle())
                .Returns(handle);

            mockWindowChecker.Setup(x => x.IsExplorer(handle))
                .Returns(true);

            service.CheckWindow();

            mockWindowGetter.Verify(x => x.GetExplorerWindow(It.IsAny<IntPtr>()), Times.Once);

            mockAutomationRegister.Verify(x => x.RegistWindowCloesd(It.IsAny<IntPtr>(), It.IsAny<Action>()), Times.Once);
        }


        [TestMethod]
        public void UTInitialize()
        {
            var service = TestTarget;
            var mockDbCommander = GetMock<IDbCommander>();
            var command = TargetResources.CreateTable_ExplorerRecodes;

            service.Initialize();

            mockDbCommander.Verify(m => m.Execute(command), Times.Once);
        }

        [TestMethod]
        public void UTUpdateRecode()
        {
            var service = GetPrivateTestTarget();
            var mockDbCommander = GetMock<IDbCommander>();
            var window = new ExplorerWindow() { Name = "X", Path = "Xxx" };
            var command = TargetResources.RegistExplorerRecodes;
            command = command.Replace("@Name", window.Name.Replace("'", "''"));
            command = command.Replace("@Path", window.Path.Replace("'", "''"));

            service.Invoke("UpdateRecode", window);

            mockDbCommander.Verify(m => m.Execute(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void UTToggleHotKey()
        {
            var service = TestTarget;
            var mockNotifier = GetMock<IHotKeyNotifier>();
            //
            mockNotifier.Setup(m => m.RegistKeys).Returns(true);

            service.ToggleHotKey(IntPtr.Zero, () => { });

            mockNotifier.Verify(m => m.Release(), Times.Once);

            //
            mockNotifier.Setup(m => m.RegistKeys).Returns(false);

            service.ToggleHotKey(IntPtr.Zero, () => { });

            mockNotifier.Verify(m => m.Initialize(It.IsAny<IntPtr>()), Times.Once);
            mockNotifier.Verify(m => m.Regist(It.IsAny<Action>()), Times.Once);
        }

      
    }
}
