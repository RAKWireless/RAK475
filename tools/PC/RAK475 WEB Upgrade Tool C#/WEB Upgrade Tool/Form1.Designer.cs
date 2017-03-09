namespace WEB_Upgrade_Tool
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBoxscan = new System.Windows.Forms.GroupBox();
            this.dataGVscan = new System.Windows.Forms.DataGridView();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSSI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonScan = new System.Windows.Forms.Button();
            this.timersearch = new System.Windows.Forms.Timer(this.components);
            this.groupBoxUpgrade = new System.Windows.Forms.GroupBox();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonimport = new System.Windows.Forms.Button();
            this.textBoximport = new System.Windows.Forms.TextBox();
            this.buttonupgrade = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxpsk = new System.Windows.Forms.TextBox();
            this.textBoxadmin = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxscan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVscan)).BeginInit();
            this.groupBoxUpgrade.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxscan
            // 
            this.groupBoxscan.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxscan.Controls.Add(this.dataGVscan);
            this.groupBoxscan.Location = new System.Drawing.Point(12, 143);
            this.groupBoxscan.Name = "groupBoxscan";
            this.groupBoxscan.Size = new System.Drawing.Size(874, 423);
            this.groupBoxscan.TabIndex = 1;
            this.groupBoxscan.TabStop = false;
            this.groupBoxscan.Text = "Scan List";
            // 
            // dataGVscan
            // 
            this.dataGVscan.AllowDrop = true;
            this.dataGVscan.AllowUserToResizeRows = false;
            this.dataGVscan.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGVscan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGVscan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGVscan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.num,
            this.ModuleName,
            this.GroupName,
            this.ModuleIP,
            this.MAC,
            this.RSSI,
            this.version,
            this.progress,
            this.select});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGVscan.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGVscan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGVscan.Enabled = false;
            this.dataGVscan.Location = new System.Drawing.Point(8, 23);
            this.dataGVscan.Name = "dataGVscan";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.NullValue = null;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGVscan.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGVscan.RowHeadersVisible = false;
            this.dataGVscan.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dataGVscan.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGVscan.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGVscan.RowTemplate.Height = 23;
            this.dataGVscan.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGVscan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGVscan.Size = new System.Drawing.Size(858, 390);
            this.dataGVscan.TabIndex = 47;
            this.dataGVscan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVscan_CellContentClick);
            // 
            // num
            // 
            this.num.HeaderText = "SID";
            this.num.Name = "num";
            this.num.Width = 50;
            // 
            // ModuleName
            // 
            this.ModuleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ModuleName.DefaultCellStyle = dataGridViewCellStyle9;
            this.ModuleName.FillWeight = 2.475583F;
            this.ModuleName.HeaderText = "ModuleName";
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ModuleName.Width = 102;
            // 
            // GroupName
            // 
            this.GroupName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GroupName.FillWeight = 265.2164F;
            this.GroupName.HeaderText = "GroupName";
            this.GroupName.Name = "GroupName";
            this.GroupName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GroupName.Width = 102;
            // 
            // ModuleIP
            // 
            this.ModuleIP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ModuleIP.DefaultCellStyle = dataGridViewCellStyle10;
            this.ModuleIP.FillWeight = 18.7647F;
            this.ModuleIP.HeaderText = " Module IP";
            this.ModuleIP.Name = "ModuleIP";
            this.ModuleIP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ModuleIP.Width = 90;
            // 
            // MAC
            // 
            this.MAC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MAC.DefaultCellStyle = dataGridViewCellStyle11;
            this.MAC.FillWeight = 142.3099F;
            this.MAC.HeaderText = "   MAC Add";
            this.MAC.Name = "MAC";
            this.MAC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MAC.Width = 110;
            // 
            // RSSI
            // 
            this.RSSI.HeaderText = "RSSI";
            this.RSSI.Name = "RSSI";
            this.RSSI.Width = 50;
            // 
            // version
            // 
            this.version.HeaderText = "Version";
            this.version.Name = "version";
            this.version.Width = 150;
            // 
            // progress
            // 
            this.progress.HeaderText = "Status";
            this.progress.Name = "progress";
            this.progress.Width = 150;
            // 
            // select
            // 
            this.select.HeaderText = "Select";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.select.Width = 50;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(589, -18);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(157, 17);
            this.progressBar1.TabIndex = 66;
            this.progressBar1.Visible = false;
            // 
            // buttonScan
            // 
            this.buttonScan.BackColor = System.Drawing.Color.Transparent;
            this.buttonScan.Location = new System.Drawing.Point(791, 89);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(95, 35);
            this.buttonScan.TabIndex = 63;
            this.buttonScan.Text = "Scan";
            this.buttonScan.UseVisualStyleBackColor = false;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // timersearch
            // 
            this.timersearch.Interval = 200;
            this.timersearch.Tick += new System.EventHandler(this.timersearch_Tick);
            // 
            // groupBoxUpgrade
            // 
            this.groupBoxUpgrade.Controls.Add(this.textBoxVersion);
            this.groupBoxUpgrade.Controls.Add(this.label5);
            this.groupBoxUpgrade.Controls.Add(this.label3);
            this.groupBoxUpgrade.Controls.Add(this.buttonimport);
            this.groupBoxUpgrade.Controls.Add(this.textBoximport);
            this.groupBoxUpgrade.Controls.Add(this.buttonupgrade);
            this.groupBoxUpgrade.Location = new System.Drawing.Point(12, 570);
            this.groupBoxUpgrade.Name = "groupBoxUpgrade";
            this.groupBoxUpgrade.Size = new System.Drawing.Size(874, 67);
            this.groupBoxUpgrade.TabIndex = 64;
            this.groupBoxUpgrade.TabStop = false;
            this.groupBoxUpgrade.Text = "Upgrade Firmware";
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(589, 28);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(150, 21);
            this.textBoxVersion.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(470, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 70;
            this.label5.Text = "Firmware version：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 66;
            this.label3.Text = "File path：";
            // 
            // buttonimport
            // 
            this.buttonimport.Location = new System.Drawing.Point(363, 20);
            this.buttonimport.Name = "buttonimport";
            this.buttonimport.Size = new System.Drawing.Size(95, 35);
            this.buttonimport.TabIndex = 0;
            this.buttonimport.Text = "Choose";
            this.buttonimport.UseVisualStyleBackColor = true;
            this.buttonimport.Click += new System.EventHandler(this.buttonimport_Click);
            // 
            // textBoximport
            // 
            this.textBoximport.Location = new System.Drawing.Point(77, 28);
            this.textBoximport.Name = "textBoximport";
            this.textBoximport.Size = new System.Drawing.Size(267, 21);
            this.textBoximport.TabIndex = 65;
            // 
            // buttonupgrade
            // 
            this.buttonupgrade.Location = new System.Drawing.Point(759, 20);
            this.buttonupgrade.Name = "buttonupgrade";
            this.buttonupgrade.Size = new System.Drawing.Size(95, 35);
            this.buttonupgrade.TabIndex = 1;
            this.buttonupgrade.Text = "Upgrade";
            this.buttonupgrade.UseVisualStyleBackColor = true;
            this.buttonupgrade.Click += new System.EventHandler(this.buttonupgrade_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 67;
            this.label1.Text = "User Name：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 68;
            this.label2.Text = "Password：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxpsk);
            this.groupBox1.Controls.Add(this.textBoxadmin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 136);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
            // 
            // textBoxpsk
            // 
            this.textBoxpsk.Location = new System.Drawing.Point(77, 91);
            this.textBoxpsk.Name = "textBoxpsk";
            this.textBoxpsk.Size = new System.Drawing.Size(150, 21);
            this.textBoxpsk.TabIndex = 70;
            this.textBoxpsk.Text = "admin";
            // 
            // textBoxadmin
            // 
            this.textBoxadmin.Location = new System.Drawing.Point(77, 36);
            this.textBoxadmin.Name = "textBoxadmin";
            this.textBoxadmin.Size = new System.Drawing.Size(150, 21);
            this.textBoxadmin.TabIndex = 69;
            this.textBoxadmin.Text = "admin";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit0;
            this.pictureBox2.Location = new System.Drawing.Point(680, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(92, 118);
            this.pictureBox2.TabIndex = 71;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit4;
            this.pictureBox1.Location = new System.Drawing.Point(584, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 115);
            this.pictureBox1.TabIndex = 70;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(307, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(252, 112);
            this.label4.TabIndex = 72;
            this.label4.Text = "固件加载\r\n倒计时：";
            this.label4.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(295, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(473, 136);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Instructions";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(33, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(245, 12);
            this.label10.TabIndex = 72;
            this.label10.Text = "Please do not power off during the load!";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(359, 12);
            this.label9.TabIndex = 71;
            this.label9.Text = "4. Click \"Upgrade\" and wait for loading after upgrade over,";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(293, 12);
            this.label8.TabIndex = 70;
            this.label8.Text = "2. Select the modules to upgrade from Scan List.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(299, 12);
            this.label7.TabIndex = 69;
            this.label7.Text = "3. Click \"Choose\" to select the upgrade firmware.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(431, 12);
            this.label6.TabIndex = 68;
            this.label6.Text = "1. Input certification information, click \"scan\" button to find module.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 643);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBoxUpgrade);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonScan);
            this.Controls.Add(this.groupBoxscan);
            this.Name = "Form1";
            this.Text = "RAK475/477 Upgrade Tool V1.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxscan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVscan)).EndInit();
            this.groupBoxUpgrade.ResumeLayout(false);
            this.groupBoxUpgrade.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxscan;
        public System.Windows.Forms.DataGridView dataGVscan;
        private System.Windows.Forms.Button buttonScan;
        private System.Windows.Forms.Timer timersearch;
        private System.Windows.Forms.GroupBox groupBoxUpgrade;
        private System.Windows.Forms.Button buttonupgrade;
        private System.Windows.Forms.Button buttonimport;
        private System.Windows.Forms.TextBox textBoximport;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxpsk;
        private System.Windows.Forms.TextBox textBoxadmin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSSI;
        private System.Windows.Forms.DataGridViewTextBoxColumn version;
        private System.Windows.Forms.DataGridViewTextBoxColumn progress;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
    }
}

