using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace TMSMainWindow
{
    /// 
    /// \class Logger
    ///
    /// \brief The purpose of this class is to be able to log messages 
    /// 
    /// \author <i>Ashley Ingle + Andrew Tudor + Will Schwetz + Taylor Trainor</i>
    ///
    class Logger
    {
        ///
        /// \brief Called to insert a new log into the log file
        /// \details <b>Details</b>
        /// 
        /// 
        /// Method logs a message to the log file
        /// 
        /// \param string logMsg 
        /// 
        /// \return Nothing
        public static void Log(string logMsg)
        {
            string logFile = ConfigurationManager.AppSettings["logPath"] + "\\activityLog.txt";
            string content = DateTime.Now.ToString("ddd, d MMM yyyy HH:mm:ss K") + " " + logMsg + "\n";
            if (!File.Exists(logFile))
            {
                // Create a file to write to.
                File.WriteAllText(logFile, content);
            }
            else
            {
                File.AppendAllText(logFile, content + "\n");
            }
        }

        ///
        /// \brief Called to insert a new log into the log file
        /// \details <b>Details</b>
        /// 
        /// 
        /// Overloaded method of the logMsg function that takes an exception and logs it to the logfile
        /// 
        /// \param Exception e
        /// 
        /// \return Nothing
        public static void Log(Exception e)
        {
            string logFile = ConfigurationManager.AppSettings["logPath"] + "\\errorLog.txt";
            string content = DateTime.Now.ToString("ddd, d MMM yyyy HH:mm:ss K") + " " + e.Message + "\n";
            if (!File.Exists(logFile))
            {
                // Create a file to write to.
                File.WriteAllText(logFile, content);
            }
            else
            {
                File.AppendAllText(logFile, content + "\n");
            }
        }
    }
}
