﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TMSMainWindow
{
    class Logger
    {
        public static void Log(string logMsg)
        {
            string logFile = AppDomain.CurrentDomain.BaseDirectory + "\\activityLog.txt";
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

        public static void Log(Exception e)
        {
            string logFile = AppDomain.CurrentDomain.BaseDirectory + "\\errorLog.txt";
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
