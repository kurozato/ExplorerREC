using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Repository
{
    public interface ILogWriter
    {
        void Write(string message);

        void Write(Exception ex, string message);
    }

    public class LogWriter : ILogWriter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LogWriter()
        {

        }
        public void Write(string message)
        {
            logger.Info(message);
        }

        public void Write(Exception ex, string message)
        {
            logger.Error(ex, message);
        }
    }
}
