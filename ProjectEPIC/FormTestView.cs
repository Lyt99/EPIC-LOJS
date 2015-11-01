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
    public partial class FormTestView : Form
    {
        private TestInfo test;

        public FormTestView(TestInfo partest)
        {
            test = partest;
            InitializeComponent();
        }

        private void FormTestView_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("已打开的测试: {0}", test.name);
        }
    }
}
