namespace IISMonitor.ReleaseManagement
{
    partial class ReleaseManagerPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateWebsiteApplication = new System.Windows.Forms.Button();
            this.btnRefreshAppPoolList = new System.Windows.Forms.Button();
            this.cmbAppPoolList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtPhysicalPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVirtualPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRefreshWebsiteList = new System.Windows.Forms.Button();
            this.cmbWebsiteList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCreateApplicationPool = new System.Windows.Forms.Button();
            this.txtApplicationPool = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateWebsiteApplication
            // 
            this.btnCreateWebsiteApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWebsiteApplication.Location = new System.Drawing.Point(674, 126);
            this.btnCreateWebsiteApplication.Name = "btnCreateWebsiteApplication";
            this.btnCreateWebsiteApplication.Size = new System.Drawing.Size(75, 23);
            this.btnCreateWebsiteApplication.TabIndex = 34;
            this.btnCreateWebsiteApplication.Text = "创建";
            this.btnCreateWebsiteApplication.UseVisualStyleBackColor = true;
            // 
            // btnRefreshAppPoolList
            // 
            this.btnRefreshAppPoolList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshAppPoolList.Location = new System.Drawing.Point(674, 68);
            this.btnRefreshAppPoolList.Name = "btnRefreshAppPoolList";
            this.btnRefreshAppPoolList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshAppPoolList.TabIndex = 33;
            this.btnRefreshAppPoolList.Text = "刷新";
            this.btnRefreshAppPoolList.UseVisualStyleBackColor = true;
            // 
            // cmbAppPoolList
            // 
            this.cmbAppPoolList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAppPoolList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppPoolList.FormattingEnabled = true;
            this.cmbAppPoolList.Location = new System.Drawing.Point(110, 69);
            this.cmbAppPoolList.Name = "cmbAppPoolList";
            this.cmbAppPoolList.Size = new System.Drawing.Size(558, 20);
            this.cmbAppPoolList.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "应用程序池列表：";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(674, 97);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnBrowser.TabIndex = 30;
            this.btnBrowser.Text = "选择";
            this.btnBrowser.UseVisualStyleBackColor = true;
            // 
            // txtPhysicalPath
            // 
            this.txtPhysicalPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhysicalPath.Location = new System.Drawing.Point(110, 98);
            this.txtPhysicalPath.Name = "txtPhysicalPath";
            this.txtPhysicalPath.Size = new System.Drawing.Size(558, 21);
            this.txtPhysicalPath.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "物理路径：";
            // 
            // txtVirtualPath
            // 
            this.txtVirtualPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVirtualPath.Location = new System.Drawing.Point(110, 127);
            this.txtVirtualPath.Name = "txtVirtualPath";
            this.txtVirtualPath.Size = new System.Drawing.Size(558, 21);
            this.txtVirtualPath.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "虚拟路径：";
            // 
            // btnRefreshWebsiteList
            // 
            this.btnRefreshWebsiteList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshWebsiteList.Location = new System.Drawing.Point(674, 39);
            this.btnRefreshWebsiteList.Name = "btnRefreshWebsiteList";
            this.btnRefreshWebsiteList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshWebsiteList.TabIndex = 25;
            this.btnRefreshWebsiteList.Text = "刷新";
            this.btnRefreshWebsiteList.UseVisualStyleBackColor = true;
            // 
            // cmbWebsiteList
            // 
            this.cmbWebsiteList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWebsiteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWebsiteList.FormattingEnabled = true;
            this.cmbWebsiteList.Location = new System.Drawing.Point(110, 40);
            this.cmbWebsiteList.Name = "cmbWebsiteList";
            this.cmbWebsiteList.Size = new System.Drawing.Size(558, 20);
            this.cmbWebsiteList.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "网站列表：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(3, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(746, 1);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // btnCreateApplicationPool
            // 
            this.btnCreateApplicationPool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateApplicationPool.Location = new System.Drawing.Point(674, 3);
            this.btnCreateApplicationPool.Name = "btnCreateApplicationPool";
            this.btnCreateApplicationPool.Size = new System.Drawing.Size(75, 23);
            this.btnCreateApplicationPool.TabIndex = 21;
            this.btnCreateApplicationPool.Text = "创建";
            this.btnCreateApplicationPool.UseVisualStyleBackColor = true;
            // 
            // txtApplicationPool
            // 
            this.txtApplicationPool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplicationPool.Location = new System.Drawing.Point(110, 4);
            this.txtApplicationPool.Name = "txtApplicationPool";
            this.txtApplicationPool.Size = new System.Drawing.Size(558, 21);
            this.txtApplicationPool.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "应用程序池名称：";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(3, 155);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(746, 1);
            this.pictureBox2.TabIndex = 35;
            this.pictureBox2.TabStop = false;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(3, 162);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(746, 272);
            this.txtLog.TabIndex = 36;
            this.txtLog.WordWrap = false;
            // 
            // ReleaseManagerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnCreateWebsiteApplication);
            this.Controls.Add(this.btnRefreshAppPoolList);
            this.Controls.Add(this.cmbAppPoolList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtPhysicalPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVirtualPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRefreshWebsiteList);
            this.Controls.Add(this.cmbWebsiteList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCreateApplicationPool);
            this.Controls.Add(this.txtApplicationPool);
            this.Controls.Add(this.label1);
            this.Name = "ReleaseManagerPanel";
            this.Size = new System.Drawing.Size(752, 437);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateWebsiteApplication;
        private System.Windows.Forms.Button btnRefreshAppPoolList;
        private System.Windows.Forms.ComboBox cmbAppPoolList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtPhysicalPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVirtualPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefreshWebsiteList;
        private System.Windows.Forms.ComboBox cmbWebsiteList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCreateApplicationPool;
        private System.Windows.Forms.TextBox txtApplicationPool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtLog;
    }
}
