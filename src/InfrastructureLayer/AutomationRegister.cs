using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace BlackSugar.Repository
{
    public interface IAutomationRegister
    {
        void RegistWindowCloesd(IntPtr handle, Action handler);
    }

    public class AutomationRegister : IAutomationRegister
    {
        protected ILogWriter _logWriter;

        public AutomationRegister(ILogWriter logWriter)
        {
            _logWriter = logWriter ?? throw new ArgumentNullException(nameof(logWriter));
        }

        public void RegistWindowCloesd(IntPtr handle, Action closed)
        {
            try
            {
                var element = AutomationElement.FromHandle(handle);

                bool isFirst = true;

                var eventHandler = new AutomationEventHandler((s, e) =>
                {
                    try
                    {
                        if (e.EventId == WindowPattern.WindowClosedEvent && isFirst)
                        {
                            closed();
                            isFirst = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logWriter.Write(ex, nameof(AutomationRegister));
                        //throw;
                    }

                });

                Automation.RemoveAllEventHandlers();

                Automation.AddAutomationEventHandler(
                    WindowPattern.WindowClosedEvent,
                    element,
                    TreeScope.Element,
                    eventHandler);
            }
            catch (ElementNotAvailableException enaEx) {
                /* this exception ignore */
                //happen depending on the timing of closing folder and timer interval.
                _logWriter.Write(enaEx, nameof(AutomationRegister));
            }
            catch (Exception ex)
            {
                _logWriter.Write(ex, nameof(AutomationRegister));
                throw;
            }

        }
        
    }
}
