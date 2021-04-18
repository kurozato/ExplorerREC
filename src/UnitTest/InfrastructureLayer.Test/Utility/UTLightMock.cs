using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace BlackSugar.UnitTests
{
    public class UTLightMock<TService>
    {
        protected Dictionary<Type, Mock> LightMockContainer = new Dictionary<Type, Mock>();

        protected TService TestTarget;

        protected void AddMockContainer<T>()
            where T : class
        {
            LightMockContainer.Add(typeof(T), new Mock<T>());
        }

        protected Mock<T> GetMock<T>()
            where T : class
        {
            return LightMockContainer[typeof(T)] as Mock<T>;
        }

        protected PrivateObject GetPrivateTestTarget()
        {
            return new PrivateObject(TestTarget);
        }

    }
}
