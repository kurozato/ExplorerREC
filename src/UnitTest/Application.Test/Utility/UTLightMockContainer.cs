using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSugar.SimpleMvp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BlackSugar.UnitTests
{
    public class UTLightMockContainer<TService>
    {
        protected Dictionary<Type, Mock> LightMockContainer = new Dictionary<Type, Mock>();

        protected TService TestTarget;

        protected SimpleInjector.Container Container;

        protected void ClearContainer()
        {
            Container = new SimpleInjector.Container();
            LightMockContainer.Clear();
        }

        protected void AddMockContainer<T>()
              where T : class
        {
            LightMockContainer.Add(typeof(T), new Mock<T>());
            Container.RegisterSingleton<T>(MockObjectCreator<T>());
        }

        protected Mock<T> GetMock<T>()
            where T : class
        {
            return LightMockContainer[typeof(T)] as Mock<T>;
        }

        protected Func<T> MockObjectCreator<T>()
                 where T : class
        {
            return () => { return GetMock<T>().Object; };
        }

        protected PrivateObject GetPrivateTestTarget()
        {
            return new PrivateObject(TestTarget);
        }
    }
}
