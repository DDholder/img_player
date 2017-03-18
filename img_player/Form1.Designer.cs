namespace img_player
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Play_Pause = new System.Windows.Forms.Button();
            this.PgDn = new System.Windows.Forms.Button();
            this.PgUp = new System.Windows.Forms.Button();
            this.Fast = new System.Windows.Forms.Button();
            this.Slow = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.play_bar = new System.Windows.Forms.TrackBar();
            this.Open = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ListMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.play_pro = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imgDealEnable = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.play_bar)).BeginInit();
            this.ListMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::img_player.Properties.Resources.无标题;
            this.pictureBox1.Location = new System.Drawing.Point(12, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(340, 244);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Play_Pause
            // 
            this.Play_Pause.Location = new System.Drawing.Point(151, 404);
            this.Play_Pause.Name = "Play_Pause";
            this.Play_Pause.Size = new System.Drawing.Size(68, 23);
            this.Play_Pause.TabIndex = 1;
            this.Play_Pause.Text = "Play";
            this.Play_Pause.UseVisualStyleBackColor = true;
            this.Play_Pause.Click += new System.EventHandler(this.Play_Pause_Click);
            // 
            // PgDn
            // 
            this.PgDn.Location = new System.Drawing.Point(228, 404);
            this.PgDn.Name = "PgDn";
            this.PgDn.Size = new System.Drawing.Size(68, 23);
            this.PgDn.TabIndex = 2;
            this.PgDn.Text = "PgDn";
            this.PgDn.UseVisualStyleBackColor = true;
            this.PgDn.Click += new System.EventHandler(this.PgDn_Click);
            // 
            // PgUp
            // 
            this.PgUp.Location = new System.Drawing.Point(77, 404);
            this.PgUp.Name = "PgUp";
            this.PgUp.Size = new System.Drawing.Size(68, 23);
            this.PgUp.TabIndex = 3;
            this.PgUp.Text = "PgUp";
            this.PgUp.UseVisualStyleBackColor = true;
            this.PgUp.Click += new System.EventHandler(this.PgUp_Click);
            // 
            // Fast
            // 
            this.Fast.Location = new System.Drawing.Point(302, 404);
            this.Fast.Name = "Fast";
            this.Fast.Size = new System.Drawing.Size(50, 23);
            this.Fast.TabIndex = 4;
            this.Fast.Text = ">>";
            this.Fast.UseVisualStyleBackColor = true;
            this.Fast.Click += new System.EventHandler(this.Fast_Click);
            // 
            // Slow
            // 
            this.Slow.Location = new System.Drawing.Point(12, 404);
            this.Slow.Name = "Slow";
            this.Slow.Size = new System.Drawing.Size(55, 23);
            this.Slow.TabIndex = 5;
            this.Slow.Text = "<<";
            this.Slow.UseVisualStyleBackColor = true;
            this.Slow.Click += new System.EventHandler(this.Slow_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(694, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加文件ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 添加文件ToolStripMenuItem
            // 
            this.添加文件ToolStripMenuItem.Name = "添加文件ToolStripMenuItem";
            this.添加文件ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.添加文件ToolStripMenuItem.Text = "添加文件";
            this.添加文件ToolStripMenuItem.Click += new System.EventHandler(this.添加文件ToolStripMenuItem_Click);
            // 
            // play_bar
            // 
            this.play_bar.Location = new System.Drawing.Point(17, 342);
            this.play_bar.Name = "play_bar";
            this.play_bar.Size = new System.Drawing.Size(335, 56);
            this.play_bar.TabIndex = 8;
            this.play_bar.Scroll += new System.EventHandler(this.play_bar_Scroll);
            // 
            // Open
            // 
            this.Open.Location = new System.Drawing.Point(564, 314);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(79, 23);
            this.Open.TabIndex = 9;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.ListMenuStrip1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(515, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(167, 244);
            this.listBox1.TabIndex = 10;
            // 
            // ListMenuStrip1
            // 
            this.ListMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ListMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.ListMenuStrip1.Name = "ListMenuStrip1";
            this.ListMenuStrip1.Size = new System.Drawing.Size(115, 56);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // play_pro
            // 
            this.play_pro.AutoSize = true;
            this.play_pro.Location = new System.Drawing.Point(148, 314);
            this.play_pro.Name = "play_pro";
            this.play_pro.Size = new System.Drawing.Size(55, 15);
            this.play_pro.TabIndex = 11;
            this.play_pro.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(358, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "ke";
            // 
            // imgDealEnable
            // 
            this.imgDealEnable.AutoSize = true;
            this.imgDealEnable.Location = new System.Drawing.Point(407, 404);
            this.imgDealEnable.Name = "imgDealEnable";
            this.imgDealEnable.Size = new System.Drawing.Size(89, 19);
            this.imgDealEnable.TabIndex = 13;
            this.imgDealEnable.Text = "处理图像";
            this.imgDealEnable.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(407, 67);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 14;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "本地",
            "串口"});
            this.comboBox1.Location = new System.Drawing.Point(515, 359);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 367);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "数据源";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 439);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.imgDealEnable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.play_pro);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.play_bar);
            this.Controls.Add(this.Slow);
            this.Controls.Add(this.Fast);
            this.Controls.Add(this.PgUp);
            this.Controls.Add(this.PgDn);
            this.Controls.Add(this.Play_Pause);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.play_bar)).EndInit();
            this.ListMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Play_Pause;
        private System.Windows.Forms.Button PgDn;
        private System.Windows.Forms.Button PgUp;
        private System.Windows.Forms.Button Fast;
        private System.Windows.Forms.Button Slow;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加文件ToolStripMenuItem;
        private System.Windows.Forms.TrackBar play_bar;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label play_pro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox imgDealEnable;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip ListMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}

