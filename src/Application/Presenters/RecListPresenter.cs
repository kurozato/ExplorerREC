using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.SimpleMvp.WinForm;
using BlackSugar.Views;
using BlackSugar.Service;
using BlackSugar.Entity;

namespace BlackSugar.Presenters
{
    public class RecListPresenter : Presenter<IRecListView>
    {
        protected IExplorerRecService _service;

        protected IColorService _colorService;

        public RecListPresenter(
            IExplorerRecService service,
            IColorService colorService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _colorService = colorService ?? throw new ArgumentNullException(nameof(colorService));
        }

        protected override void InitializeView()
        {
            _view.Model = _service.GetExplorerWindows();
            _view.ColorInfo = _colorService.GetColorInfo();

            _view.SelectedAction = (window) => SelectedResult(window);
        }

        private void SelectedResult(ExplorerWindow window)
        {
            if (_service.OpenWindow(window))
                _view.Close();
            else
                _view.ShowMessage(" Not Exists Selected Folder Path. フォルダが存在しません。");
        }
    }
}
