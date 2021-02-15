using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure
{
    public interface ILogger
    {
        public void Debug(string message);
        public void Debug(string message, params object[] Parameters);
        public void Debug(Exception exception);
        public void Debug(Exception exception, string message);
        public void Debug(Exception exception, string message, params object[] Parameters);

        public void Information(string message);
        public void Information(string message, params object[] Parameters);
        public void Information(Exception exception);
        public void Information(Exception exception, string message);
        public void Information(Exception exception, string message, params object[] Parameters);

        public void Warning(string message);
        public void Warning(string message, params object[] Parameters);
        public void Warning(Exception exception);
        public void Warning(Exception exception, string message);
        public void Warning(Exception exception, string message, params object[] Parameters);

        public void Error(string message);
        public void Error(string message, params object[] Parameters);
        public void Error(Exception exception);
        public void Error(Exception exception, string message);
        public void Error(Exception exception, string message, params object[] Parameters);

        public void Fatal(string message);
        public void Fatal(string message, params object[] Parameters);
        public void Fatal(Exception exception);
        public void Fatal(Exception exception, string message);
        public void Fatal(Exception exception, string message, params object[] Parameters);
    }
}
