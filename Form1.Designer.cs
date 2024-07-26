namespace PttCrawler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBoxCustomUrls = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            dataGridView = new DataGridView();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            btnSaveUrl = new Button();
            menuStrip1 = new MenuStrip();
            檔案FileToolStripMenuItem = new ToolStripMenuItem();
            模式ModelToolStripMenuItem = new ToolStripMenuItem();
            tsmi_model_Normal = new ToolStripMenuItem();
            tsmi_model_Panel = new ToolStripMenuItem();
            結束ExitToolStripMenuItem = new ToolStripMenuItem();
            功能ToolsToolStripMenuItem = new ToolStripMenuItem();
            項目數量CountToolStripMenuItem = new ToolStripMenuItem();
            tsmi_count2 = new ToolStripMenuItem();
            tsmi_count3 = new ToolStripMenuItem();
            tsmi_count5 = new ToolStripMenuItem();
            截取版面ToolStripMenuItem = new ToolStripMenuItem();
            自訂SelfToolStripMenuItem = new ToolStripMenuItem();
            熱門HotToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            tsmi_Update_Sec10 = new ToolStripMenuItem();
            tsmi_Update_Sec20 = new ToolStripMenuItem();
            tsmi_Update_Sec30 = new ToolStripMenuItem();
            關於AboutToolStripMenuItem = new ToolStripMenuItem();
            版本VersionToolStripMenuItem = new ToolStripMenuItem();
            plFlatMode = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxCustomUrls
            // 
            textBoxCustomUrls.Location = new Point(-2, 586);
            textBoxCustomUrls.Multiline = true;
            textBoxCustomUrls.Name = "textBoxCustomUrls";
            textBoxCustomUrls.ScrollBars = ScrollBars.Vertical;
            textBoxCustomUrls.Size = new Size(507, 119);
            textBoxCustomUrls.TabIndex = 1;
            textBoxCustomUrls.Text = resources.GetString("textBoxCustomUrls.Text");
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 2000;
            timer1.Tick += Timer1_Tick;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(0, 27);
            dataGridView.Name = "dataGridView";
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.Size = new Size(662, 559);
            dataGridView.TabIndex = 3;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 708);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(661, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(128, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnSaveUrl
            // 
            btnSaveUrl.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnSaveUrl.Location = new Point(519, 589);
            btnSaveUrl.Name = "btnSaveUrl";
            btnSaveUrl.Size = new Size(129, 112);
            btnSaveUrl.TabIndex = 5;
            btnSaveUrl.Text = "Save Url";
            btnSaveUrl.UseVisualStyleBackColor = true;
            btnSaveUrl.Click += btnSaveUrl_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 檔案FileToolStripMenuItem, 功能ToolsToolStripMenuItem, 關於AboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(661, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // 檔案FileToolStripMenuItem
            // 
            檔案FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 模式ModelToolStripMenuItem, 結束ExitToolStripMenuItem });
            檔案FileToolStripMenuItem.Name = "檔案FileToolStripMenuItem";
            檔案FileToolStripMenuItem.Size = new Size(70, 20);
            檔案FileToolStripMenuItem.Text = "檔案(&File)";
            // 
            // 模式ModelToolStripMenuItem
            // 
            模式ModelToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmi_model_Normal, tsmi_model_Panel });
            模式ModelToolStripMenuItem.Name = "模式ModelToolStripMenuItem";
            模式ModelToolStripMenuItem.Size = new Size(126, 22);
            模式ModelToolStripMenuItem.Text = "模式";
            // 
            // tsmi_model_Normal
            // 
            tsmi_model_Normal.Name = "tsmi_model_Normal";
            tsmi_model_Normal.Size = new Size(98, 22);
            tsmi_model_Normal.Text = "正常";
            tsmi_model_Normal.Click += tsmi_model_Click;
            // 
            // tsmi_model_Panel
            // 
            tsmi_model_Panel.Name = "tsmi_model_Panel";
            tsmi_model_Panel.Size = new Size(98, 22);
            tsmi_model_Panel.Text = "扁平";
            tsmi_model_Panel.Click += tsmi_model_Click;
            // 
            // 結束ExitToolStripMenuItem
            // 
            結束ExitToolStripMenuItem.Name = "結束ExitToolStripMenuItem";
            結束ExitToolStripMenuItem.Size = new Size(126, 22);
            結束ExitToolStripMenuItem.Text = "結束(Exit)";
            // 
            // 功能ToolsToolStripMenuItem
            // 
            功能ToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 項目數量CountToolStripMenuItem, 截取版面ToolStripMenuItem, toolStripMenuItem1 });
            功能ToolsToolStripMenuItem.Name = "功能ToolsToolStripMenuItem";
            功能ToolsToolStripMenuItem.Size = new Size(82, 20);
            功能ToolsToolStripMenuItem.Text = "功能(Tools)";
            // 
            // 項目數量CountToolStripMenuItem
            // 
            項目數量CountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmi_count2, tsmi_count3, tsmi_count5 });
            項目數量CountToolStripMenuItem.Name = "項目數量CountToolStripMenuItem";
            項目數量CountToolStripMenuItem.Size = new Size(197, 22);
            項目數量CountToolStripMenuItem.Text = "項目數量(Count)";
            // 
            // tsmi_count2
            // 
            tsmi_count2.CheckOnClick = true;
            tsmi_count2.Name = "tsmi_count2";
            tsmi_count2.Size = new Size(81, 22);
            tsmi_count2.Text = "2";
            tsmi_count2.Click += tsmi_count_Click;
            // 
            // tsmi_count3
            // 
            tsmi_count3.CheckOnClick = true;
            tsmi_count3.Name = "tsmi_count3";
            tsmi_count3.Size = new Size(81, 22);
            tsmi_count3.Text = "3";
            tsmi_count3.Click += tsmi_count_Click;
            // 
            // tsmi_count5
            // 
            tsmi_count5.Checked = true;
            tsmi_count5.CheckOnClick = true;
            tsmi_count5.CheckState = CheckState.Checked;
            tsmi_count5.Name = "tsmi_count5";
            tsmi_count5.Size = new Size(81, 22);
            tsmi_count5.Text = "5";
            tsmi_count5.Click += tsmi_count_Click;
            // 
            // 截取版面ToolStripMenuItem
            // 
            截取版面ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 自訂SelfToolStripMenuItem, 熱門HotToolStripMenuItem });
            截取版面ToolStripMenuItem.Name = "截取版面ToolStripMenuItem";
            截取版面ToolStripMenuItem.Size = new Size(197, 22);
            截取版面ToolStripMenuItem.Text = "截取版面(Board)";
            // 
            // 自訂SelfToolStripMenuItem
            // 
            自訂SelfToolStripMenuItem.Name = "自訂SelfToolStripMenuItem";
            自訂SelfToolStripMenuItem.Size = new Size(180, 22);
            自訂SelfToolStripMenuItem.Text = "自訂(Self)";
            自訂SelfToolStripMenuItem.Click += tsmi_board_Click;
            // 
            // 熱門HotToolStripMenuItem
            // 
            熱門HotToolStripMenuItem.Name = "熱門HotToolStripMenuItem";
            熱門HotToolStripMenuItem.Size = new Size(180, 22);
            熱門HotToolStripMenuItem.Text = "熱門(Hot)";
            熱門HotToolStripMenuItem.Click += tsmi_board_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { tsmi_Update_Sec10, tsmi_Update_Sec20, tsmi_Update_Sec30 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(197, 22);
            toolStripMenuItem1.Text = "更新截取秒數(Update)";
            // 
            // tsmi_Update_Sec10
            // 
            tsmi_Update_Sec10.CheckOnClick = true;
            tsmi_Update_Sec10.Name = "tsmi_Update_Sec10";
            tsmi_Update_Sec10.Size = new Size(180, 22);
            tsmi_Update_Sec10.Text = "10";
            tsmi_Update_Sec10.Click += UpdataContentMenu_Click;
            // 
            // tsmi_Update_Sec20
            // 
            tsmi_Update_Sec20.Checked = true;
            tsmi_Update_Sec20.CheckOnClick = true;
            tsmi_Update_Sec20.CheckState = CheckState.Checked;
            tsmi_Update_Sec20.Name = "tsmi_Update_Sec20";
            tsmi_Update_Sec20.Size = new Size(180, 22);
            tsmi_Update_Sec20.Text = "20";
            tsmi_Update_Sec20.Click += UpdataContentMenu_Click;
            // 
            // tsmi_Update_Sec30
            // 
            tsmi_Update_Sec30.CheckOnClick = true;
            tsmi_Update_Sec30.Name = "tsmi_Update_Sec30";
            tsmi_Update_Sec30.Size = new Size(180, 22);
            tsmi_Update_Sec30.Text = "30";
            tsmi_Update_Sec30.Click += UpdataContentMenu_Click;
            // 
            // 關於AboutToolStripMenuItem
            // 
            關於AboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 版本VersionToolStripMenuItem });
            關於AboutToolStripMenuItem.Name = "關於AboutToolStripMenuItem";
            關於AboutToolStripMenuItem.Size = new Size(86, 20);
            關於AboutToolStripMenuItem.Text = "關於(About)";
            // 
            // 版本VersionToolStripMenuItem
            // 
            版本VersionToolStripMenuItem.Name = "版本VersionToolStripMenuItem";
            版本VersionToolStripMenuItem.Size = new Size(148, 22);
            版本VersionToolStripMenuItem.Text = "版本(Version)";
            // 
            // plFlatMode
            // 
            plFlatMode.AutoScroll = true;
            plFlatMode.Dock = DockStyle.Fill;
            plFlatMode.Location = new Point(0, 24);
            plFlatMode.Name = "plFlatMode";
            plFlatMode.Size = new Size(661, 684);
            plFlatMode.TabIndex = 7;
            plFlatMode.MouseDown += plFlatMode_MouseDown;
            plFlatMode.MouseMove += plFlatMode_MouseMove;
            plFlatMode.MouseUp += plFlatMode_MouseUp;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 730);
            Controls.Add(plFlatMode);
            Controls.Add(btnSaveUrl);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(dataGridView);
            Controls.Add(textBoxCustomUrls);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBoxCustomUrls;
        private System.Windows.Forms.Timer timer1;
        private DataGridView dataGridView;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button btnSaveUrl;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 檔案FileToolStripMenuItem;
        private ToolStripMenuItem 結束ExitToolStripMenuItem;
        private ToolStripMenuItem 功能ToolsToolStripMenuItem;
        private ToolStripMenuItem 項目數量CountToolStripMenuItem;
        private ToolStripMenuItem tsmi_count2;
        private ToolStripMenuItem tsmi_count3;
        private ToolStripMenuItem tsmi_count5;
        private ToolStripMenuItem 關於AboutToolStripMenuItem;
        private ToolStripMenuItem 版本VersionToolStripMenuItem;
        private ToolStripMenuItem 截取版面ToolStripMenuItem;
        private ToolStripMenuItem 自訂SelfToolStripMenuItem;
        private ToolStripMenuItem 熱門HotToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem tsmi_Update_Sec10;
        private ToolStripMenuItem tsmi_Update_Sec20;
        private ToolStripMenuItem tsmi_Update_Sec30;
        private ToolStripMenuItem 模式ModelToolStripMenuItem;
        private ToolStripMenuItem tsmi_model_Normal;
        private ToolStripMenuItem tsmi_model_Panel;
        private Panel plFlatMode;
    }
}
