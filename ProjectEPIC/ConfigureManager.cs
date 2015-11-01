using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
//using System.Collections;

namespace ProjectEPIC
{
    /// <summary>
    /// 一个题目
    /// </summary>
    public struct subject
    {
        /// <summary>
        /// 试题名称
        /// </summary>
        public String name;
        /// <summary>
        /// 试题选项
        /// </summary>
        public String option;
        /// <summary>
        /// 试题所在文件夹
        /// </summary>
        public String dir;
        /// <summary>
        /// 备注
        /// </summary>
        public String desc;
        /// <summary>
        /// 内存限制(M)
        /// </summary>
        public int memorylimit;
        /// <summary>
        /// 时间限制(ms)
        /// </summary>
        public int timelimit;
        /// <summary>
        /// 测试点数目
        /// </summary>
        public int points;
    }

    /// <summary>
    /// 测试信息
    /// </summary>
    public struct TestInfo
    {
        /// <summary>
        /// 测试是否有效
        /// </summary>
        public Boolean Exists;
        /// <summary>
        /// 测试名称
        /// </summary>
        public String name;
        /// <summary>
        /// 测试备注
        /// </summary>
        public String desc;
        /// <summary>
        /// 测试目录
        /// </summary>
        public String dir;
       // public String[] coption;
       /// <summary>
       /// 测试所包含的题目
       /// </summary>
        public subject[] subjects;
        /// <summary>
        /// 附加参数
        /// </summary>
        public JObject additional;
    }


    class ConfigureManager
    {
        private string ext = ".epic";
        static String[] compliers = new String[3];
        static JObject obj;

        /// <summary>
        /// 刷新配置
        /// </summary>
        /// <returns>是否刷新成功</returns>
        public Boolean refresh()
        {
                String config;
                if (!File.Exists("config.json"))
                {
                    /*MessageBox.Show("没有找到配置文件！将自动生成默认文件！");
                    string json = "{\n    \"Compliers\": {\n        \"Cpp\":\"bin\\compliers\\cpp\",\n        \"C\": \"bin\\compliers\\C\",\n        \"Pascal\":\"bin\\Compliers\\pascal\"\n    }\n}";
                    File.WriteAllText("config.json", json);
                    */
                    MessageBox.Show(null, "错误代码：0x00001\n错误原因：没有找到config.json!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("ErrorCode:0x00001");
                    return false;
                }

                config = File.ReadAllText("config.json");

                try
                {
                    obj = (JObject)JsonConvert.DeserializeObject(config);

                    compliers[0] = obj["Compliers"]["Cpp"].ToString();
                    compliers[1] = obj["Compliers"]["C"].ToString();
                    compliers[2] = obj["Compliers"]["Pascal"].ToString();

                    return true;
                }
                catch(JsonReaderException)
                {
                    MessageBox.Show("config.json读取失败！");
                    return false;
                }
        }

        ///<summary>
        /// 获取编译器地址
        ///</summary>
        /// <param name="type">1=Cpp,2=C,3=PASCAL</param>
        /// <returns>编译器地址</returns>
        public String getComplier(int type)
        {

            if (type >= 0 && type <= 2)
            {
                return compliers[type];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置编译器地址
        /// </summary>
        /// <param name="type">1=Cpp,2=C,3=PASCAL</param>
        /// <param name="value">编译器地址</param>
        /// <returns>是否成功</returns>
        public Boolean setComplier(int type, String value)
        {
            if (type >= 0 && type <= 2)
            {
                compliers[type] = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得目录下所有有效的测试
        /// </summary>
        /// <returns>TestInfo数组</returns>
        public TestInfo[] getTests() {
            List<TestInfo> result = new List<TestInfo>();
            string dir = this.getTestsDir();
            if (!(Directory.Exists(dir))) Directory.CreateDirectory(dir);//如果不存在则创建总试题目录
            DirectoryInfo di = new DirectoryInfo(dir);

            DirectoryInfo[] list = di.GetDirectories();

            for (int index = 0; index < list.Length; ++index)
            {
                if ((list[index].GetFiles("testinfo" + ext, SearchOption.TopDirectoryOnly)).Length != 0)
                {//如果存在testinfo文件
                    TestInfo curinfo = this.getTestInfo(list[index].Name);
                    if (curinfo.Exists) result.Add(curinfo);//如果有效才加入
                    //else
                }
                //else
            }
                return result.ToArray();

        }

        
        /// <summary>
        /// 获取JObject手动操作
        /// </summary>
        /// <returns>JObject</returns>
        public JObject getObject()
        {
            return obj;
        }


        /// <summary>
        /// 获取测试总目录，失败返回null
        /// </summary>
        /// <returns>测试目录</returns>
        public String getTestsDir()
        {
            try
            {
                return obj["Tests"]["Dir"].ToString();
            }
            catch(Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取测试信息
        /// </summary>
        /// <param name="dir">测试目录名</param>
        /// <returns>TextInfo对象</returns>
        private TestInfo getTestInfo(String dir)
        {
            TestInfo ret = new TestInfo();

            JObject obj;
            String json = File.ReadAllText(getTestsDir() + @"\" + dir + @"\testinfo" + ext);
            try
            {
                obj = (JObject)JsonConvert.DeserializeObject(json);
            }
            catch(JsonReaderException)
            {
                MessageBox.Show(null,"文件夹 " + dir + " 中testinfo" + ext + "读取失败!可能是json格式错误!","错误");
                ret.Exists = false;
                return ret;
            }

            ret.Exists = true;
            ret.dir = dir;
            try
            {
                ret.name = obj["info"]["name"].ToString();
                ret.desc = obj["info"]["desc"].ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(null, "测试 " + dir + " 加载失败！","提示");
                ret.Exists = false;
                return ret;
            }
            

            subject[] sub = new subject[obj["subjects"].Count()];

            for (int i = 0; i < obj["subjects"].Count(); ++i)
            {
                try
                {
                sub[i].name = obj["subjects"][i]["name"].ToString();
                sub[i].memorylimit = int.Parse(obj["subjects"][i]["memorylimit"].ToString());
                sub[i].desc = obj["subjects"][i]["desc"].ToString();
                sub[i].dir = obj["subjects"][i]["folder"].ToString();
                sub[i].option = obj["subjects"][i]["option"].ToString();
                sub[i].points = int.Parse(obj["subjects"][i]["pointscount"].ToString());
                sub[i].timelimit = int.Parse(obj["subjects"][i]["timelimit"].ToString());
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show(null,"测试 " + ret.name + " 加载失败，可能存在缺少项！","提示");
                    ret.Exists = false;
                    return ret;
                }
                catch (FormatException)
                {
                    MessageBox.Show(null, "测试 " + ret.name + " 中testinfo" + ext + "存在格式错误！","提示");
                }
            }

            ret.subjects = sub;

            //Additional options
            try
            {
                ret.additional = (JObject)obj["additional"];
            }
            catch (NullReferenceException) { };
            return ret;

        }
    }
}
