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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.play_pro = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.play_bar)).BeginInit();
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
            this.PgDn.Text = "PdDn";
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
            // 
            // Slow
            // 
            this.Slow.Location = new System.Drawing.Point(12, 404);
            this.Slow.Name = "Slow";
            this.Slow.Size = new System.Drawing.Size(55, 23);
            this.Slow.TabIndex = 5;
            this.Slow.Text = "<<";
            this.Slow.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(574, 28);
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
            this.添加文件ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
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
            this.Open.Location = new System.Drawing.Point(407, 332);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(79, 23);
            this.Open.TabIndex = 9;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(358, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(167, 244);
            this.listBox1.TabIndex = 10;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 439);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.play_bar)).EndInit();
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
    }
}
