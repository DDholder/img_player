﻿namespace img_player
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.play_bar)).BeginInit();
            this.ListMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::img_player.Properties.Resources.无标题;
            this.pictureBox1.Location = new System.Drawing.Point(9, 45);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 182);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Play_Pause
            // 
            this.Play_Pause.Location = new System.Drawing.Point(113, 323);
            this.Play_Pause.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Play_Pause.Name = "Play_Pause";
            this.Play_Pause.Size = new System.Drawing.Size(51, 18);
            this.Play_Pause.TabIndex = 1;
            this.Play_Pause.Text = "Play";
            this.Play_Pause.UseVisualStyleBackColor = true;
            this.Play_Pause.Click += new System.EventHandler(this.Play_Pause_Click);
            // 
            // PgDn
            // 
            this.PgDn.Location = new System.Drawing.Point(171, 323);
            this.PgDn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PgDn.Name = "PgDn";
            this.PgDn.Size = new System.Drawing.Size(51, 18);
            this.PgDn.TabIndex = 2;
            this.PgDn.Text = "PgDn";
            this.PgDn.UseVisualStyleBackColor = true;
            this.PgDn.Click += new System.EventHandler(this.PgDn_Click);
            // 
            // PgUp
            // 
            this.PgUp.Location = new System.Drawing.Point(58, 323);
            this.PgUp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PgUp.Name = "PgUp";
            this.PgUp.Size = new System.Drawing.Size(51, 18);
            this.PgUp.TabIndex = 3;
            this.PgUp.Text = "PgUp";
            this.PgUp.UseVisualStyleBackColor = true;
            this.PgUp.Click += new System.EventHandler(this.PgUp_Click);
            // 
            // Fast
            // 
            this.Fast.Location = new System.Drawing.Point(226, 323);
            this.Fast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Fast.Name = "Fast";
            this.Fast.Size = new System.Drawing.Size(38, 18);
            this.Fast.TabIndex = 4;
            this.Fast.Text = ">>";
            this.Fast.UseVisualStyleBackColor = true;
            this.Fast.Click += new System.EventHandler(this.Fast_Click);
            // 
            // Slow
            // 
            this.Slow.Location = new System.Drawing.Point(9, 323);
            this.Slow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Slow.Name = "Slow";
            this.Slow.Size = new System.Drawing.Size(41, 18);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(572, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加文件ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 添加文件ToolStripMenuItem
            // 
            this.添加文件ToolStripMenuItem.Name = "添加文件ToolStripMenuItem";
            this.添加文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.添加文件ToolStripMenuItem.Text = "添加文件";
            this.添加文件ToolStripMenuItem.Click += new System.EventHandler(this.添加文件ToolStripMenuItem_Click);
            // 
            // play_bar
            // 
            this.play_bar.Location = new System.Drawing.Point(13, 274);
            this.play_bar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.play_bar.Name = "play_bar";
            this.play_bar.Size = new System.Drawing.Size(251, 45);
            this.play_bar.TabIndex = 8;
            this.play_bar.Scroll += new System.EventHandler(this.play_bar_Scroll);
            // 
            // Open
            // 
            this.Open.Location = new System.Drawing.Point(463, 251);
            this.Open.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(59, 18);
            this.Open.TabIndex = 9;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.ListMenuStrip1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(435, 45);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(126, 196);
            this.listBox1.TabIndex = 10;
            // 
            // ListMenuStrip1
            // 
            this.ListMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ListMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.ListMenuStrip1.Name = "ListMenuStrip1";
            this.ListMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
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
            this.play_pro.Location = new System.Drawing.Point(111, 251);
            this.play_pro.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.play_pro.Name = "play_pro";
            this.play_pro.Size = new System.Drawing.Size(41, 12);
            this.play_pro.TabIndex = 11;
            this.play_pro.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "ke";
            // 
            // imgDealEnable
            // 
            this.imgDealEnable.AutoSize = true;
            this.imgDealEnable.Location = new System.Drawing.Point(305, 323);
            this.imgDealEnable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.imgDealEnable.Name = "imgDealEnable";
            this.imgDealEnable.Size = new System.Drawing.Size(72, 16);
            this.imgDealEnable.TabIndex = 13;
            this.imgDealEnable.Text = "处理图像";
            this.imgDealEnable.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(339, 53);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(76, 21);
            this.textBox1.TabIndex = 14;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "本地",
            "串口"});
            this.comboBox1.Location = new System.Drawing.Point(444, 286);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 20);
            this.comboBox1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 294);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "数据源";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(339, 91);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(76, 21);
            this.textBox2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "赛道类型";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 351);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
    }
}

