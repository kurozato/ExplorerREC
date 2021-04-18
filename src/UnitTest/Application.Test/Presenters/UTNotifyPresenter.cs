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

namespace Application.Test
{
    [TestClass]
    public class UTNotifyPresenter : UTLightMockContainer<NotifyPresenter>
    {
        [TestInitialize]
        public void UTInit()
        {
            ClearContainer();

            //
            AddMockContainer<IGeneralSetting>();
            AddMockContainer<ILogWriter>();
            //
            AddMockContainer<IColorService>();
            AddMockContainer<IExplorerRecService>();
            //
            AddMockContainer<INotifyView>();
            AddMockContainer<IRecListView>();
            AddMockContainer<IColorSettingView>();
            //
            AddMockContainer<IPresenter<IRecListView>>();
            AddMockContainer<IPresenter<IColorSettingView>>();

            Container.RegisterSingleton<IPresenter<INotifyView>, NotifyPresenter>();
            Container.Verify();

            var resolver = new DependencyResolver();
            resolver.Set(Container);

            XApplication.Configure(resolver);

            var mockSetting = GetMock<IGeneralSetting>();
            mockSetting.Setup(m => m.Interval).Returns(1000);

            XApplication.NavigateTo<INotifyView>();

            TestTarget = resolver.Resolve<IPresenter<INotifyView>>() as NotifyPresenter;
        }

        [TestMethod]
        public void UTViewOpenResult()
        {
            var presenter = GetPrivateTestTarget();

            presenter.Invoke("OpenResult", null);

            var mockPresenter = GetMock<IPresenter<IRecListView>>();
            mockPresenter.Verify(p => p.Initialize(), Times.Once);
            mockPresenter.Verify(p => p.Show(false), Times.Once);
        }

        [TestMethod]
        public void UTViewColorSettingResult()
        {
            var presenter = GetPrivateTestTarget();

            presenter.Invoke("ColorSettingResult", null);

            var mockPresenter = GetMock<IPresenter<IColorSettingView>>();
            mockPresenter.Verify(p => p.Initialize(), Times.Once);
            mockPresenter.Verify(p => p.Show(false), Times.Once);

        }

        [TestMethod]
        public void UTViewRoopResult()
        {
            var presenter = GetPrivateTestTarget();

            presenter.Invoke("RoopResult", null);

            var mockView = GetMock<INotifyView>();
            var mockService = GetMock<IExplorerRecService>();

            mockService.Verify(m => m.CheckWindow(), Times.Once);
            mockView.Verify(v => v.TimerStop(), Times.Once);
            mockView.Verify(v => v.TimerStart(), Times.Once);
        }

    }
}
