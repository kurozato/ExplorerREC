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
    public class UTExplorerRecService : UTLightMock<ExplorerRecService>
    {
        [TestInitialize]
        public void UTInit()
        {
            LightMockContainer.Clear();
            AddMockContainer<IWindowChecker>();
            AddMockContainer<IWindowGetter>();
            AddMockContainer<IAutomationRegister>();
            AddMockContainer<IDbCommander>();
            AddMockContainer<IGeneralSetting>();

            TestTarget = new ExplorerRecService(
                GetMock<IWindowChecker>().Object,
                GetMock<IWindowGetter>().Object,
                GetMock<IAutomationRegister>().Object,
                GetMock<IDbCommander>().Object,
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
        public void UTGetExplorerWindows()
        {
            var service = TestTarget;
            var mockDbCommander = GetMock<IDbCommander>();
            var command = TargetResources.Query_ExplorerRecodes;

            service.GetExplorerWindows();

            mockDbCommander.Verify(m => m.Get<ExplorerWindow>(command, It.IsAny<object>()), Times.Once);

        }

        [TestMethod]
        public void UTOpenWindow()
        {
            var service = TestTarget;
            var mockDbCommander = GetMock<IDbCommander>();
            var window = new ExplorerWindow() { Name = "X", Path = "Xxx" };
            var command = TargetResources.DeleteExplorerRecodes;
            command = command.Replace("@Path", window.Path.Replace("'", "''"));

            service.OpenWindow(window);

            mockDbCommander.Verify(m => m.Execute(command), Times.Once);

        }
    }
}
