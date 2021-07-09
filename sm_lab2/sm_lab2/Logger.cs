using System;
using System.Data;
using System.IO;

namespace logger
{
    public enum Severity
    {
        Trace, Debug, Information, Warning, Error, Critical
    }
    
    public class Logger
    {
        private DateTime _dateTime;
        private string _message;
        private string _path;

        public Logger(string path)
        {
            _path = path;
        }

        public void Log (Severity severity, string message)
        {
            if (!File.Exists(_path))
            {
                using (File.Create(_path));
            }
            
            _message = message;
            _dateTime = DateTime.Now;
            string Text = "[" + _dateTime.ToString() + "]" + "[" + severity.ToString() + "]:" + _message +
                          Environment.NewLine;
            File.AppendAllText(_path, Text);
        }
    }
    
}
