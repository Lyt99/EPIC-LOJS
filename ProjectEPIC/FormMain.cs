using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectEPIC
{
    public partial class FormMain : Form
    {
        ConfigureManager cmanager;
        TestInfo[] testinfolist;
        public FormMain()
        {
            InitializeComponent();//初始化窗体
            cmanager = new ConfigureManager();
            if (!cmanager.refresh())
                System.Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelTestInformation.Text = "";
            refreshList();
        }

        private void refreshList()
        {
            TestsListBox.Items.Clear();
            testinfolist = cmanager.getTests();
            for (int index = 0; index < testinfolist.Length; ++index)
                TestsListBox.Items.Add(testinfolist[index].name);

        }

        private void TestsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TestsListBox.SelectedIndex == -1) return;
            refreshTestInformation(TestsListBox.SelectedIndex);
            buttonLoadTest.Enabled = true;

        }

        private void refreshTestInformation(int testid)
        {

            string fmt = "测试名称: {0}\n测试说明: {1}\n包含: {2}";

            var test = testinfolist[testid];

            string textsubjects = String.Empty;
            foreach (subject sub in test.subjects) {
                textsubjects += sub.name + ',';
            }

            textsubjects = textsubjects.Substring(0, textsubjects.Length - 1);
            labelTestInformation.Text = String.Format(fmt, test.name, test.desc, textsubjects);

        }

        private void TestsListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && TestsListBox.SelectedItem != null)
                buttonLoadTest_Click(null, null);
        }

        private void buttonLoadTest_Click(object sender, EventArgs e)
        {
            try
            {
                var test = testinfolist[TestsListBox.SelectedIndex];

                var formtestview = new FormTestView(test);

                formtestview.ShowDialog(this);
                
                
            }
            catch (Exception) { return; }
        }

        private void TestsListBox_DoubleClick_1(object sender, EventArgs e)
        {
            buttonLoadTest_Click(null, null);
        }
    }
}
