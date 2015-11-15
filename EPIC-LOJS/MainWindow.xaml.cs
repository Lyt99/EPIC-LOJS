using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EPIC_LOJS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ConfigureManager CONFIGURE_MANAGER;
        LangsManager LANGS_MANAGER;
        TestManager TEST_MANAGER;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeConfigureManager();
            InitializeLangsManager();
            InitializeTestManager();//必须在ConfigureManager初始化完后使用
            RefreshTests();
            setAllControlsLocalized();
        }

        private void InitializeTestManager()
        {
            TEST_MANAGER = new TestManager(CONFIGURE_MANAGER);
        }

        private void InitializeLangsManager()
        {
            LANGS_MANAGER = new LangsManager();
        }

        private void InitializeConfigureManager()
        {
            CONFIGURE_MANAGER = new ConfigureManager();
            if (!CONFIGURE_MANAGER.refresh())
            {
                MessageBox.Show("配置加载失败！", "EPIC");
                System.Environment.Exit(0);
            }
               
        }

        private void setAllControlsLocalized()
        {
            this.Title = LANGS_MANAGER.getTranslation("EPIC_MAINFORM_TITLE","EPIC - LOCAL OI JUDGEMENT SYSTEM - MISSING LANGUAGE FILE");
            foreach(var i in this.MainGird.Children)
            {
                Button btn;
                try
                {
                    btn = (Button)i;
                }
                catch (InvalidCastException) { continue; }

                btn.Content = LANGS_MANAGER.getTranslation((String)btn.Content);
            }
        }

        private void RefreshTests()
        {
            listBox.Items.Clear();
            var tests = TEST_MANAGER.getTests();
            foreach(var i in tests)
            {
                listBox.Items.Add(i.name);
            }

            String labeltext = LANGS_MANAGER.getTranslation("LABEL_CURRENTTESTSCOUNT");
            labelTestsCount.Content = String.Format(labeltext, tests.Length);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
