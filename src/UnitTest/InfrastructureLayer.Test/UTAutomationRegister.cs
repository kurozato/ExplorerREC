using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackSugar.UnitTests;
using BlackSugar.Entity;
using BlackSugar.Repository;
using Moq;
using System.Windows.Automation;
using System.Windows.Forms;

namespace InfrastructureLayer.Test
{
    [TestClass]
    public class UTAutomationRegister : UTLightMock<AutomationRegister>
    {
        [TestInitialize]
        public void UTInit()
        {
            LightMockContainer.Clear();
            AddMockContainer<ILogWriter>();

            TestTarget = new AutomationRegister(
                GetMock<ILogWriter>().Object);
        }

        [TestMethod]
        public void UTRegistWindowCloesd()
        {
            var register = TestTarget;
            var counter = 0;
            using (var form = new Form())
            {
                register.RegistWindowCloesd(form.Handle, () => {
                    counter++;
                });
          
                form.Show();
                System.Threading.Thread.Sleep(1000);
                form.Close();
                System.Threading.Thread.Sleep(1000);
            }

            Assert.AreEqual(1, counter);
        }
    }
}
