using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace img_player
{
    public struct points
    {
        public int[] img;
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        points[] fps = new points[6000];//6000帧数据
        byte[] buff = new byte[600];//串口读出的图像
        int[,] map = new int[80, 60];//解压后图像
        public int time = 0, retime = 0;//记录时间和回放时间
        List<byte> readList = new List<byte>();//文件读取缓存链表
        string state = "stop";
        string[] filenames = new string[100];
        bool filestrflag = false;
        img_deal img_Handler = new img_deal();
        private void Play_Pause_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                Play_Pause.Text = "Play";
                state = "stop";
                timer1.Enabled = false;
            }
            else
            {
                Play_Pause.Text = "Pause";
                state = "play";
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (state == "play")
            {
                if (retime < time)
                {
                    play(retime);
                    retime++;
                }
            }
        }
        void play(int num)
        {
            play_bar.Maximum = time;
            play_bar.Value = retime;
            play_pro.Text = retime.ToString() + "/" + time.ToString();
            if (imgDealEnable.Checked)
            {
                img_Handler.imgbuff = fps[num].img;
                img_Handler.image_deal();
                textBox1.Text = img_Handler.ke.ToString();
            }
            Changemap(fps[num].img);
            Display(map);
        }
        void Changemap(int[] imgbuff)
        {
            for (int i = 0; i < 60; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if ((imgbuff[i * 10 + j] & (1 << k)) != 0)
                        {
                            map[j * 8 + 7 - k, i] = 1;
                        }
                        else
                        {
                            map[j * 8 + 7 - k, i] = 0;
                        }
                    }
                }
            }

        }
        void Display(int[,] image_buff)
        {
            Graphics g = pictureBox1.CreateGraphics();

            for (int i = 0; i < 60; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    if (image_buff[j, i] == 1)
                    {
                        Rectangle rect = new Rectangle(j * 3, i * 3, 3, 3);
                        g.FillRectangle(Brushes.Black, rect);
                    }
                    else
                    {
                        Rectangle rect = new Rectangle(j * 3, i * 3, 3, 3);
                        g.FillRectangle(Brushes.White, rect);
                    }
                }
            }
        }
        private void 添加文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opdialog = new OpenFileDialog();
            opdialog.Multiselect = true;
            opdialog.Filter = "txt文件|*.txt";
            DialogResult result = opdialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                for (int i = 0; i < opdialog.FileNames.Length; i++)
                {
                    filenames[listBox1.Items.Count] = opdialog.FileNames[i];
                    listBox1.Items.Add(opdialog.FileNames[i].Substring(opdialog.FileNames[i].LastIndexOf("\\") + 1));
                }
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            // System.Diagnostics.Process.Start(filenames[n]);
            imgdatainit();
            try
            {
                FileStream file = new FileStream(filenames[n], FileMode.Open);
                byte[] readByte = new byte[file.Length];
                file.Seek(0, SeekOrigin.Begin);
                file.Read(readByte, 0, readByte.Length); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
                Read_imgFile(readByte, readByte.Length);
                file.Close();
                play(0);
            }
            catch (IOException err)
            {
                Console.WriteLine(err.ToString());
            }
        }
        void imgdatainit()
        {
            for (int i = 0; i < time; i++)
            {
                for (int j = 0; j < 600; j++)
                {
                    fps[i].img[j] = 0;
                }
            }
            time = 0;
            retime = 0;
        }
        void Read_imgFile(byte[] str, int fpsLength)
        {
            fps[time].img = new int[600];
            for (int i = 0; i < fpsLength; i++)
            {
                if (str[i] == 0xaa && str[i + 1] == 0xbb)
                    if (str[i + 2] == '*' || str[i + 2] == '$') filestrflag = true;
                if (filestrflag)
                    readList.Add(str[i]);
                if (readList.Count > 3)
                    if (readList[readList.Count - 1] == 0xee && readList[readList.Count - 2] == 0xdd && readList[readList.Count - 3] == 0xcc)
                    {
                        filestrflag = false;
                        byte[] bytehandler = readList.ToArray();
                        Readpic(bytehandler, bytehandler.Length - 3);
                        for (int j = 0; j < 600; j++)
                        {
                            fps[time].img[j] = buff[j];
                        }
                        readList.Clear();
                        time++;
                        fps[time].img = new int[600];
                    }
            }
        }

        private void play_bar_Scroll(object sender, EventArgs e)
        {
            retime = play_bar.Value;
            if ( retime < time)
            {
                play_pro.Text = retime.ToString() + "/" + time.ToString();
                play(retime);
            }
        }

        private void PgUp_Click(object sender, EventArgs e)
        {
            if (  retime > 0)
            {
                retime--;
                play(retime);
            }
        }

        private void PgDn_Click(object sender, EventArgs e)
        {
            if ( retime < time)
            {
                retime++;
                play(retime);
            }
        }

        void Readpic(byte[] str, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (i < 600)
                    buff[i] = str[i + 3];
            }
        }
    }
}
