using PrancaBeauty.Infrastructure.Logger.Contracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Infrastructure.Logger.Serilogger
{
    public class Serilogger : PrancaBeauty.Infrastructure.Logger.Contracts.ILogger
    {
        public Serilogger()
        {

        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Debug(string message, params object[] Parameters)
        {
            Log.Debug(message, Parameters);
        }

        public void Debug(Exception exception)
        {
            Log.Debug(exception, exception.Message);
        }

        public void Debug(Exception exception, string message)
        {
            Log.Debug(exception, message);
        }

        public void Debug(Exception exception, string message, params object[] Parameters)
        {
            Log.Debug(exception, message, Parameters);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(Exception exception)
        {
            Log.Error(exception, exception.Message);
        }

        public void Error(Exception exception, string message)
        {
            Log.Error(exception, message);
        }

        public void Error(string message, params object[] Parameters)
        {
            Log.Error(message, Parameters);
        }

        public void Error(Exception exception, string message, params object[] Parameters)
        {
            Log.Error(exception, message, Parameters);
        }

        public void Fatal(string message)
        {
            Log.Fatal(message);
        }

        public void Fatal(Exception exception)
        {
            Log.Fatal(exception, exception.Message);
        }

        public void Fatal(Exception exception, string message)
        {
            Log.Fatal(exception, message);
        }

        public void Fatal(string message, params object[] Parameters)
        {
            Log.Fatal(message, Parameters);
        }

        public void Fatal(Exception exception, string message, params object[] Parameters)
        {
            Log.Fatal(exception, message, Parameters);
        }

        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Information(Exception exception)
        {
            Log.Information(exception, exception.Message);
        }

        public void Information(Exception exception, string message)
        {
            Log.Information(exception, message);
        }

        public void Information(string message, params object[] Parameters)
        {
            Log.Information(message, Parameters);
        }

        public void Information(Exception exception, string message, params object[] Parameters)
        {
            Log.Information(exception, message, Parameters);
        }

        public void Warning(string message)
        {
            Log.Warning(message);
        }

        public void Warning(Exception exception)
        {
            Log.Warning(exception, exception.Message);
        }

        public void Warning(Exception exception, string message)
        {
            Log.Warning(exception, message);
        }

        public void Warning(string message, params object[] Parameters)
        {
            Log.Warning(message, Parameters);
        }

        public void Warning(Exception exception, string message, params object[] Parameters)
        {
            Log.Warning(exception, message, Parameters);
        }
    }
}
