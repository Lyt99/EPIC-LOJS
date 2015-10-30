namespace ProjectEPIC
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TestsListBox = new System.Windows.Forms.ListBox();
            this.buttonLoadTest = new System.Windows.Forms.Button();
            this.labelTestInformation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TestsListBox
            // 
            this.TestsListBox.FormattingEnabled = true;
            this.TestsListBox.ItemHeight = 12;
            this.TestsListBox.Location = new System.Drawing.Point(12, 12);
            this.TestsListBox.Name = "TestsListBox";
            this.TestsListBox.Size = new System.Drawing.Size(366, 472);
            this.TestsListBox.TabIndex = 0;
            this.TestsListBox.SelectedIndexChanged += new System.EventHandler(this.TestsListBox_SelectedIndexChanged);
            this.TestsListBox.DoubleClick += new System.EventHandler(this.TestsListBox_DoubleClick_1);
            this.TestsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TestsListBox_KeyPress);
            // 
            // buttonLoadTest
            // 
            this.buttonLoadTest.Enabled = false;
            this.buttonLoadTest.Location = new System.Drawing.Point(692, 449);
            this.buttonLoadTest.Name = "buttonLoadTest";
            this.buttonLoadTest.Size = new System.Drawing.Size(100, 35);
            this.buttonLoadTest.TabIndex = 1;
            this.buttonLoadTest.Text = "载入测试";
            this.buttonLoadTest.UseVisualStyleBackColor = true;
            this.buttonLoadTest.Click += new System.EventHandler(this.buttonLoadTest_Click);
            // 
            // labelTestInformation
            // 
            this.labelTestInformation.AutoSize = true;
            this.labelTestInformation.Location = new System.Drawing.Point(400, 9);
            this.labelTestInformation.Name = "labelTestInformation";
            this.labelTestInformation.Size = new System.Drawing.Size(125, 12);
            this.labelTestInformation.TabIndex = 2;
            this.labelTestInformation.Text = "labelTestInformation";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 490);
            this.Controls.Add(this.labelTestInformation);
            this.Controls.Add(this.buttonLoadTest);
            this.Controls.Add(this.TestsListBox);
            this.Name = "FormMain";
            this.Text = "EPIC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox TestsListBox;
        private System.Windows.Forms.Button buttonLoadTest;
        private System.Windows.Forms.Label labelTestInformation;
    }
}

