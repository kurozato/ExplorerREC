using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.SimpleMvp;
using SimpleInjector;

namespace BlackSugar.SimpleMvp
{
    /// <summary>
    /// SimpleInjector:
    /// https://simpleinjector.org/
    /// </summary>
    public class DependencyResolver : IDependencyResolver
    {
        private Container container;

        public object ContainerObject => container;

        public TService Resolve<TService>() where TService : class
        {
            return container.GetInstance<TService>();
        }

        public object Resolve(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }

        public void Set(Container container)
        {
            this.container = container;
        }

        public void Set(Action<Container> register)
        {
            container = new Container();
            register(container);
        }

        public void Release()
        {
            if (container == null) return;

            container.Dispose();
            container = null;
        }

    }

    public static class Extention
    {
        public static void RegisterWindowsForm<TView, TForm>(this SimpleInjector.Container container)
        {
            RegisterWindowsForm(container, typeof(TView), typeof(TForm));
        }

        public static void RegisterWindowsForm(this SimpleInjector.Container container, Type viewType, Type formType)
        {
            var producer = SimpleInjector.Lifestyle.Transient.CreateProducer(viewType, formType, container);

            producer.Registration.SuppressDiagnosticWarning(
                    SimpleInjector.Diagnostics.DiagnosticType.DisposableTransientComponent,
                    "Forms should be disposed by app code; not by the container.");

            container.AddRegistration(viewType, producer.Registration);
        }
    }
}
