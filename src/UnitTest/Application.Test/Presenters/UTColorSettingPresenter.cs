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
    public class UTColorSettingPresenter : UTLightMockContainer<ColorSettingPresenter>
    {
        [TestInitialize]
        public void UTInit()
        {
            ClearContainer();

            //
            AddMockContainer<IColorService>();
            //
            AddMockContainer<IColorSettingView>();
            //
            Container.RegisterSingleton<IPresenter<IColorSettingView>, ColorSettingPresenter>();
            Container.Verify();

            var resolver = new DependencyResolver();
            resolver.Set(Container);

            Router.Configure(resolver);


            Router.NavigateTo<IColorSettingView>();

            TestTarget = resolver.Resolve<IPresenter<IColorSettingView>>() as ColorSettingPresenter;
        }

        [TestMethod]
        public void UTInitialize()
        {
            var presenter = TestTarget;

            presenter.Initialize();

            var mockService = GetMock<IColorService>();
            mockService.Verify(s => s.GetColorInfo(), Times.Once);
            mockService.Verify(s => s.GetGradationPatterns(), Times.Once);
            mockService.Verify(s => s.GetColorThemes(), Times.Once);
        }


        [TestMethod]
        public void UTSettingResult()
        {
            var presenter = GetPrivateTestTarget();
            var theme = ColorTheme.Light;
            var pattern = GradationPattern.Monotone;

            presenter.Invoke("SettingResult", new { theme, pattern });

            var mockService = GetMock<IColorService>();
            mockService.Verify(s => s.GetColorInfo(theme, pattern), Times.Once);
            mockService.Verify(s => s.Save(It.IsAny<ColorInfo>()), Times.Once);

        }
    }
}
