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
        public String testname;
        /// <summary>
        /// 源文件名称
        /// </summary>
        public String sourcename;
        /// <summary>
        /// 试题选项
        /// </summary>
        public String option;
        /// <summary>
        /// 测试点数目
        /// </summary>
        public int points;
        /// <summary>
        /// 试题所在文件夹
        /// </summary>
        public String dir;
        /// <summary>
        /// 备注
        /// </summary>
        public String desc;
        /// <summary>
        /// 标准读入文件 例：dancer.in
        /// </summary>
        public String stdin;
        /// <summary>
        /// 标准输出文件 例：dancer.out
        /// </summary>
        public String stdout;
        /// <summary>
        /// 测试点读入文件 例：dancer10.in
        /// <summary>
        public TestPoint[] testpoint;
    }
    /// <summary>
    /// 题目下的测试点
    /// </summary>
    public struct TestPoint
    {
        /// <summary>
        /// 测试点读入文件 例：dancer10.in
        /// <summary>
        public String In;
        /// <summary>
        /// 测试点输出文件 例：dancer10.out
        /// </summary>
        public String Out;
        /// <summary>
        /// 题目分数
        /// </summary>
        public int score;
        /// <summary>
        /// 比较类型（忽略行尾空格，不忽略行尾空格，自定义比较器）
        /// CT_IgnoreLine CT_UnIgnoreLine CT_CustomComparator
        /// </summary>
        public int compareType;
        /// <summary>
        /// 运行类型（编译源文件(限制系统函数)，编译源文件(不限制函数)，直接运行exe） 
        /// RT_SourceLimit RT_SourceWithoutLimit RT_Bin
        /// </summary>
        public int runType;
        /// <summary>
        /// 内存限制(M)
        /// </summary>
        public int memorylimit;
        /// <summary>
        /// 时间限制(ms)
        /// </summary>
        public int timelimit;
    }
    /// <summary>
    /// 测试信息
    /// </summary>
    public struct TestInfo
    {
        /// <summary>
        /// 评测机版本
        /// </summary>
        public String Version;
        /// <summary>
        /// 测试是否有效
        /// </summary>
        public Boolean Exists;
        /// <summary>
        /// 竞赛名称
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
                    MessageBox.Show(null, "错误代码：0x0001\n错误原因：没有找到config.json!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("ErrorCode:0x0001"); Logger.log("Program exit");
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
                catch(JsonReaderException)//json读取异常
                {
                    MessageBox.Show(null, "错误代码：0x0002config.json读取失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("ErrorCode:0x0002"); Logger.log("Program exit");
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
        public Boolean setComplier(int type, String path)
        {
            if (type >= 0 && type <= 2)
            {
                compliers[type] = path;
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
            Console.WriteLine(dir);
            if(dir == null)
            {
                MessageBox.Show(null, "错误代码：0x0008\n错误原因：获取评测目录失败！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("0x0008 获取评测目录失败");
            }
            else if (!(Directory.Exists(dir))) Directory.CreateDirectory(dir);//如果不存在则创建总试题目录
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
                MessageBox.Show(null,"错误代码：0x0003\n错误信息：文件夹 " + dir + " 中testinfo" + ext + "读取失败!可能是json格式错误!","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("0x0003"); Logger.log(dir + " testinfo" + ext + "读取失败"); Logger.log("Program Continue");
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
                MessageBox.Show(null, "错误代码：0x0004\n错误信息：测试 " + dir + " 加载失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("0x0004");
                ret.Exists = false;
                return ret;
            }
            

            subject[] sub = new subject[obj["subjects"].Count()];

            for (int i = 0; i < obj["subjects"].Count(); ++i)
            {
                try
                {
                    sub[i].testname = obj["subjects"][i]["name"].ToString();
                    sub[i].sourcename = obj["subjects"][i]["sourcename"].ToString();
                    sub[i].option = obj["subjects"][i]["option"].ToString();
                    sub[i].stdin = obj["subjects"][i]["stdin"].ToString();
                    sub[i].stdout = obj["subjects"][i]["stout"].ToString();
                    sub[i].desc = obj["subjects"][i]["desc"].ToString();
                    sub[i].dir = obj["subjects"][i]["folder"].ToString();
                    sub[i].points = int.Parse(obj["subjects"][i]["pointscount"].ToString());
                    for (int j = 0; j < sub[i].points; ++j)
                    {
                        try
                        {
                            sub[i].testpoint[j].timelimit = int.Parse(obj["subjects"][i]["point"][j]["timelimit"].ToString());
                            sub[i].testpoint[j].memorylimit = int.Parse(obj["subjects"][i]["point"][j]["memorylimit"].ToString());
                            sub[i].testpoint[j].In = obj["subjects"][i]["point"][j]["in"].ToString();
                            sub[i].testpoint[j].Out = obj["subjects"][i]["point"][j]["out"].ToString();
                            sub[i].testpoint[j].runType = int.Parse(obj["subjects"][i]["point"][j]["runtypt"].ToString());
                            sub[i].testpoint[j].compareType = int.Parse(obj["subjects"][i]["point"][j]["comparetype"].ToString());
                            sub[i].testpoint[j].score = int.Parse(obj["subjects"][i]["point"][j]["score"].ToString());
                        }
                        catch (NullReferenceException)
                        {
                            MessageBox.Show(null, "错误代码：0x0004\n错误信息：竞赛 " + ret.name + " 题目" + sub[i].testname + "测试点" + j.ToString() + "加载失败，可能存在缺少项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Logger.log("0x0005 " + "竞赛 " + ret.name + " 题目" + sub[i].testname + "测试点" +  j.ToString() + "加载失败，可能存在缺少项！");
                            ret.Exists = false;
                            return ret;
                        }
                    }

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show(null, "错误代码：0x0005\n错误信息：竞赛 " + ret.name + "题目" + i.ToString() + " 加载失败，可能存在缺少项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logger.log("0x0006 " + "竞赛 " + ret.name + "题目" + i.ToString() + " 加载失败，可能存在缺少项！");
                    ret.Exists = false;
                    return ret;
                }
                catch (FormatException)
                {
                    MessageBox.Show(null, "错误代码：0x0006\n错误信息：测试 " + ret.name + " 中testinfo" + ext + "存在格式错误！","提示");
                    Logger.log("0x0007 " + "测试 " + ret.name + " 中testinfo" + ext + "存在格式错误！");
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
