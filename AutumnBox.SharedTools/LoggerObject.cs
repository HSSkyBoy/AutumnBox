﻿/* =============================================================================*\
*
* Filename: Logger
* Description: 
*
* Version: 1.0
* Created: 2017/10/18 12:06:57(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AutumnBox.Shared
{
    public sealed class LoggerObject
    {
        private string _LogFileName;
        public LoggerObject(string logFileName)
        {
            _LogFileName = logFileName;
        }
        public void InitFile() { }
        public void D(object tag, string msg)
        {
            string fullMsg = ToFullMessage(tag, msg);
            Debug.WriteLine(fullMsg);
            WriteToFile(fullMsg);
        }
        public void D(object tag, string msg, Exception e)
        {
            string fullMsg = $"{ToFullMessage(tag, msg)}\n{ToFullMessage(e, e.Message)}";
            Debug.WriteLine(fullMsg);
            WriteToFile(fullMsg);
        }
        public void T(object tag, string msg)
        {
            string fullMsg = ToFullMessage(tag, msg);
            Trace.WriteLine(fullMsg);
            WriteToFile(fullMsg);
        }
        public void T(object tag, string msg, Exception e)
        {
            string fullMsg = $"{ToFullMessage(tag, msg)}\n{ToFullMessage(e, e.Message)}";
            Trace.WriteLine(fullMsg);
            WriteToFile(fullMsg);
        }
        private void WriteToFile(string fullMsg)
        {
            try
            {
                using (FileStream fs = new FileStream(_LogFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(fullMsg);
                        sw.Flush();
                    }
                }
            }
            catch { }
        }
        private static string ParseTag(object tag)
        {
            if (tag is string) return tag.ToString();
            else return tag.GetType().Name;
        }
        private static string ToFullMessage(object tag, string msg)
        {
            string t = $"[{DateTime.Now.ToString("[yy-MM-dd hh:mm:ss]")}";
            return $"{t} { ParseTag(tag)} : {msg}";
        }
    }
}