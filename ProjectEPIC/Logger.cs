using System;
using System.IO;

namespace ProjectEPIC
{
    static class Logger
    {
        static string logfile = "epic_log.txt";

        /// <summary>
        /// 写入log到文件
        /// 格式为[时间]日志
        /// </summary>
        /// <param name="str">log内容</param>
        public static void log(string str) {
            using (StreamWriter sw = new StreamWriter(logfile, true))
            {
                string logstring = "[" + DateTime.Now.ToString() + "]" + str;
                sw.WriteLine(logstring);
            }
        }

        public static void logwithNotification(string logstr, string body, string bodyExtra)
        {
            log(logstr);
            Notifier.CreateToastNotification(body, bodyExtra);
        }

    }
}
