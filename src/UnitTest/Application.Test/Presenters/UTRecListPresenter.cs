using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackSugar.SimpleMvp;
using BlackSugar.SimpleMvp.WinForm;
using BlackSugar.Views;
using BlackSugar.Presenters;
using BlackSugar.Service;
using BlackSugar.Repository;
using BlackSugar.UnitTests;
using BlackSugar.Entity;

namespace Application.Test
{
    [TestClass]
    public class UTRecListPresenter : UTLightMockContainer<RecListPresenter>
    {
        [TestInitialize]
        public void UTInit()
        {
            ClearContainer();

            //
            AddMockContainer<IColorService>();
            AddMockContainer<IExplorerRecService>();
            //
            AddMockContainer<IRecListView>();
            //
            Container.RegisterSingleton<IPresenter<IRecListView>, RecListPresenter>();
            Container.Verify();

            var resolver = new DependencyResolver();
            resolver.Set(Container);

            Router.Configure(resolver);

    
            Router.NavigateTo<IRecListView>();

            TestTarget = resolver.Resolve<IPresenter<IRecListView>>() as RecListPresenter;
        }

        [TestMethod]
        public void UTInitialize()
        {
            var presenter = TestTarget;

            presenter.Initialize();

            var mockService = GetMock<IExplorerRecService>();
            var mockSubService = GetMock<IColorService>();
            mockService.Verify(s => s.GetExplorerWindows(), Times.Once);
            mockSubService.Verify(s => s.GetColorInfo(), Times.Once);

        }

        [TestMethod]
        public void UTSelectedResult()
        {
            var presenter = GetPrivateTestTarget();
            var window = new ExplorerWindow() { Name = "X", Path = "Xxx" };
            var mockService = GetMock<IExplorerRecService>();
            mockService.Setup(s => s.OpenWindow(window)).Returns(true);

            presenter.Invoke("SelectedResult", window);

            var mockView = GetMock<IRecListView>();
            mockView.Verify(v => v.Close(), Times.Once);
            mockView.Verify(v => v.ShowMessage(It.IsAny<string>()), Times.Never);

        }

        [TestMethod]
        public void UTSelectedResultError()
        {
            var presenter = GetPrivateTestTarget();
            var window = new ExplorerWindow() { Name = "X", Path = "Xxx" };
            var mockService = GetMock<IExplorerRecService>();
            mockService.Setup(s => s.OpenWindow(window)).Returns(false);

            presenter.Invoke("SelectedResult", window);

            var mockView = GetMock<IRecListView>();
            mockView.Verify(v => v.Close(), Times.Never);
            mockView.Verify(v => v.ShowMessage(It.IsAny<string>()), Times.Once);

        }
    }
}
