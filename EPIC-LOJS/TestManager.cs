using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EPIC_LOJS
{
    class TestManager
    {
        const String infofile = "info.epic";
        TestInfo[] Tests;

        public TestManager(ConfigureManager CM) {
            setTestsInfo(CM);
        }

        public TestInfo[] getTests() {
            return Tests;
        }

        private void setTestsInfo(ConfigureManager CM) {
            List<TestInfo> result = new List<TestInfo>();
            string dir = CM.getTestsDir();
            if (!(Directory.Exists(dir))) Directory.CreateDirectory(dir);//如果不存在则创建总试题目录
            DirectoryInfo di = new DirectoryInfo(dir);

            DirectoryInfo[] list = di.GetDirectories();

            for (int index = 0; index < list.Length; ++index)
            {
                if ((list[index].GetFiles(infofile, SearchOption.TopDirectoryOnly)).Length != 0)
                {//如果存在testinfo文件
                    TestInfo curinfo = CM.getTestInfo(list[index].Name, infofile);
                    if (curinfo.Exists) result.Add(curinfo);//如果有效才加入
                    //else
                }
                //else
            }
            Tests = result.ToArray();
        }
    }
}
