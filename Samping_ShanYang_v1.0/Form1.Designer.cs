namespace Samping_ShanYang_v1._0
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
        /// <param name="disposing">如果应该释放托管资源，则为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.btnStartSampling = new System.Windows.Forms.Button();
            this.btnStopSampling = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.ListBOXData = new System.Windows.Forms.ListBox();
            this.labelSamplingRate = new System.Windows.Forms.Label();
            this.txtSamplingRate = new System.Windows.Forms.TextBox();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.plotView2 = new OxyPlot.WindowsForms.PlotView();
            this.txtTimeInput = new System.Windows.Forms.TextBox();
            this.cmbTimeUnit = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartSampling
            // 
            this.btnStartSampling.BackColor = System.Drawing.Color.LightGreen;
            this.btnStartSampling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartSampling.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartSampling.Location = new System.Drawing.Point(738, 10);
            this.btnStartSampling.Name = "btnStartSampling";
            this.btnStartSampling.Size = new System.Drawing.Size(120, 40);
            this.btnStartSampling.TabIndex = 0;
            this.btnStartSampling.Text = "开始采样";
            this.btnStartSampling.UseVisualStyleBackColor = false;
            this.btnStartSampling.Click += new System.EventHandler(this.btnStartSampling_Click);
            // 
            // btnStopSampling
            // 
            this.btnStopSampling.BackColor = System.Drawing.Color.IndianRed;
            this.btnStopSampling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopSampling.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnStopSampling.Location = new System.Drawing.Point(917, 10);
            this.btnStopSampling.Name = "btnStopSampling";
            this.btnStopSampling.Size = new System.Drawing.Size(120, 40);
            this.btnStopSampling.TabIndex = 1;
            this.btnStopSampling.Text = "停止采样";
            this.btnStopSampling.UseVisualStyleBackColor = false;
            this.btnStopSampling.Click += new System.EventHandler(this.btnStopSampling_Click);
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPort.Location = new System.Drawing.Point(129, 6);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(150, 23);
            this.txtPort.TabIndex = 14;
            this.txtPort.Text = "9001";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelPort.Location = new System.Drawing.Point(32, 9);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(50, 17);
            this.labelPort.TabIndex = 16;
            this.labelPort.Text = "端口：";
            // 
            // ListBOXData
            // 
            this.ListBOXData.AllowDrop = true;
            this.ListBOXData.BackColor = System.Drawing.SystemColors.Window;
            this.ListBOXData.Font = new System.Drawing.Font("宋体", 10.5F);
            this.ListBOXData.FormattingEnabled = true;
            this.ListBOXData.ItemHeight = 14;
            this.ListBOXData.Location = new System.Drawing.Point(12, 71);
            this.ListBOXData.Name = "ListBOXData";
            this.ListBOXData.Size = new System.Drawing.Size(620, 858);
            this.ListBOXData.TabIndex = 19;
            // 
            // labelSamplingRate
            // 
            this.labelSamplingRate.AutoSize = true;
            this.labelSamplingRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelSamplingRate.Location = new System.Drawing.Point(12, 38);
            this.labelSamplingRate.Name = "labelSamplingRate";
            this.labelSamplingRate.Size = new System.Drawing.Size(95, 17);
            this.labelSamplingRate.TabIndex = 10;
            this.labelSamplingRate.Text = "采样率 (Hz)：";
            // 
            // txtSamplingRate
            // 
            this.txtSamplingRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSamplingRate.Location = new System.Drawing.Point(129, 35);
            this.txtSamplingRate.Name = "txtSamplingRate";
            this.txtSamplingRate.Size = new System.Drawing.Size(150, 23);
            this.txtSamplingRate.TabIndex = 7;
            this.txtSamplingRate.Text = "1000";
            // 
            // plotView1
            // 
            this.plotView1.Location = new System.Drawing.Point(663, 71);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(900, 400);
            this.plotView1.TabIndex = 21;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotView2
            // 
            this.plotView2.Location = new System.Drawing.Point(663, 477);
            this.plotView2.Name = "plotView2";
            this.plotView2.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView2.Size = new System.Drawing.Size(900, 400);
            this.plotView2.TabIndex = 22;
            this.plotView2.Text = "plotView2";
            this.plotView2.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView2.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView2.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // txtTimeInput
            // 
            this.txtTimeInput.Location = new System.Drawing.Point(395, 21);
            this.txtTimeInput.Name = "txtTimeInput";
            this.txtTimeInput.Size = new System.Drawing.Size(100, 21);
            this.txtTimeInput.TabIndex = 23;
            // 
            // cmbTimeUnit
            // 
            this.cmbTimeUnit.FormattingEnabled = true;
            this.cmbTimeUnit.Items.AddRange(new object[] {
            "秒",
            "分钟",
            "小时"});
            this.cmbTimeUnit.Location = new System.Drawing.Point(511, 22);
            this.cmbTimeUnit.Name = "cmbTimeUnit";
            this.cmbTimeUnit.Size = new System.Drawing.Size(121, 20);
            this.cmbTimeUnit.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(311, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "采样间隔：";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1659, 959);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTimeUnit);
            this.Controls.Add(this.txtTimeInput);
            this.Controls.Add(this.plotView2);
            this.Controls.Add(this.plotView1);
            this.Controls.Add(this.ListBOXData);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.labelSamplingRate);
            this.Controls.Add(this.txtSamplingRate);
            this.Controls.Add(this.btnStopSampling);
            this.Controls.Add(this.btnStartSampling);
            this.Name = "Form1";
            this.Text = "数据采集与可视化";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartSampling;
        private System.Windows.Forms.Button btnStopSampling;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.ListBox ListBOXData;
        private System.Windows.Forms.Label labelSamplingRate;
        private System.Windows.Forms.TextBox txtSamplingRate;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private System.Windows.Forms.TextBox txtTimeInput;
        private System.Windows.Forms.ComboBox cmbTimeUnit;
        private System.Windows.Forms.Label label1;
    }
}