using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace guichu
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            P_kaishi_paotayidong();


            int V_Length = 0;//Length=原文本提供
            int V_PBS = 0;//  V_PBS=原文本提供
            int V_PBW = 0;//  V_PBW=((Length+Length*0.04166)-PBS)/5
            int V_CHA_PBS = 0;
            string g = "NoteNum=60";
            string[] sArray = g.Split('=');//Split后面要有单引号
            string V_Front = sArray[0];
            string V_After = sArray[1];
            int jindu = 0;
            int max = textBox1.Lines.GetUpperBound(0) + 1;
            progressBar1.Maximum = max;
            foreach (string s in textBox1.Lines)
            {


                jindu++;
                progressBar1.Value = jindu;
                if (!s.Contains("="))
                {
                    if (!s.Contains("#"))
                    {
                        textBox2.AppendText(s);
                        textBox2.AppendText(Environment.NewLine);
                    }
                }
                else
                {
                    sArray = s.Split('=');//Split后面要有单引号
                    V_Front = sArray[0];
                    V_After = sArray[1];

                    if (V_Front == "Length")
                    {
                        V_Length = int.Parse(V_After);
                        textBox2.AppendText(s);
                        textBox2.AppendText(Environment.NewLine);
                    }

                    else if (V_Front == "PBS")
                    {
                        V_CHA_PBS = 0;
                        PBS(V_After, V_PBS,s,V_CHA_PBS);
                        
                    }

                    else if (V_Front == "PBW")
                    {
                        PBW(V_Length, V_After, V_PBS, V_PBW, s);
                    }

                    else if (V_Front == "NoteNum")
                    {
                        textBox2.AppendText(s);
                        textBox2.AppendText(Environment.NewLine);
                        textBox2.AppendText("PreUtterance=");
                        textBox2.AppendText(Environment.NewLine);
                    }
                    else if (V_Front == "Moduration")
                    {
                        V_CHA_PBS = 1;
                        textBox2.AppendText(s);
                        textBox2.AppendText(Environment.NewLine);
                    }

                    else
                    {
                            textBox2.AppendText(s);
                            textBox2.AppendText(Environment.NewLine);
                        
                    }
                }

                if (s.Contains("#"))
                {
                    if (V_CHA_PBS == 1)
                    {
                        V_PBS = 0;
                        PBS(V_After, V_PBS, s, V_CHA_PBS);
                        PBW(V_Length, V_After, V_PBS, V_PBW, s);                        
                    }

                    textBox2.AppendText(s);
                    textBox2.AppendText(Environment.NewLine);
                }
            }


            P_jieshu_queren();
            Thread.Sleep(1000);
            P_jieshu_jiesu();


        }

        public void PBS(string V_After,int V_PBS, string s,int V_CHA_PBS)
        {
            if (V_CHA_PBS == 1)
            {
                textBox2.AppendText("PBS=0");
                textBox2.AppendText(Environment.NewLine);
                V_CHA_PBS = 0;
            }
            else
            {

                string PBS2 = V_After.Substring(0, 1);
                if (PBS2 == "-")
                {
                    //原文本内容string，ARCSII码赋值，如果有“-”号取5位后
                    V_PBS = int.Parse(V_After.Substring(1)) * (-1);
                }
                else
                {
                    V_PBS = int.Parse(V_After);//原文本内容string，ARCSII码赋值，如果没有“-”号取4位后
                }
                textBox2.AppendText(s);
                textBox2.AppendText(Environment.NewLine);
            }
           
        }

        public void PBW(int V_Length, string V_After, int V_PBS,int V_PBW, string s)
        {
            V_PBW = int.Parse(V_After);
            int V_PBW_Q = (int)((V_Length + V_Length * 0.04166) - V_PBS);
            int a = 5;

            if (radioButton1.Checked)
            {
                a = 1;
            }
            if (radioButton2.Checked)
            {
                a = 2;
            }
            if (radioButton3.Checked)
            {
                a = 3;
            }
            if (radioButton4.Checked)
            {
                a = 4;
            }
            if (radioButton5.Checked)
            {
                a = 5;
            }

            int V_PBW_I = V_PBW_Q / a;
            int V_PBW_F = V_PBW_Q % a;

            if (a == 1)
            {
                int V_PBW_1 = V_PBW_I;
                string V_PBW_STRING_0 = "PBW=" + V_PBW_1 ;
                textBox2.AppendText(V_PBW_STRING_0);
                textBox2.AppendText(Environment.NewLine);
            }
            if (a == 2)
            {
                int V_PBW_1 = V_PBW_I;
                int V_PBW_2 = V_PBW_I;

                switch (V_PBW_F)
                {
                    case 0:
                        string V_PBW_STRING_0 = "PBW=" + V_PBW_1 + "," + V_PBW_2 ;
                        textBox2.AppendText(V_PBW_STRING_0);
                        break;
                    case 1:
                        string V_PBW_STRING_1 = "PBW=" + (V_PBW_1 + 1) + "," + V_PBW_2 ;
                        textBox2.AppendText(V_PBW_STRING_1);
                        break;
                }
                textBox2.AppendText(Environment.NewLine);
                textBox2.AppendText("PBY=0");
                textBox2.AppendText(Environment.NewLine);
            }
            if (a == 3)
            {
                int V_PBW_1 = V_PBW_I;
                int V_PBW_2 = V_PBW_I;
                int V_PBW_3 = V_PBW_I;

                switch (V_PBW_F)
                {
                    case 0:
                        string V_PBW_STRING_0 = "PBW=" + V_PBW_1 + "," + V_PBW_2 + "," + V_PBW_3 ;
                        textBox2.AppendText(V_PBW_STRING_0);
                        break;
                    case 1:
                        string V_PBW_STRING_1 = "PBW=" + (V_PBW_1 + 1) + "," + V_PBW_2 + "," + V_PBW_3 ;
                        textBox2.AppendText(V_PBW_STRING_1);
                        break;
                    case 2:
                        string V_PBW_STRING_2 = "PBW=" + V_PBW_1 + "," + (V_PBW_2 + 1) + ","  + (V_PBW_3 + 1);
                        textBox2.AppendText(V_PBW_STRING_2);
                        break;
                }
                textBox2.AppendText(Environment.NewLine);
                textBox2.AppendText("PBY=0,0");
                textBox2.AppendText(Environment.NewLine);
            }
            if (a == 4)
            {
                int V_PBW_1 = V_PBW_I;
                int V_PBW_2 = V_PBW_I;
                int V_PBW_3 = V_PBW_I;
                int V_PBW_4 = V_PBW_I;

                switch (V_PBW_F)
                {
                    case 0:
                        string V_PBW_STRING_0 = "PBW=" + V_PBW_1 + "," + V_PBW_2 + "," + V_PBW_3 + "," + V_PBW_4;
                        textBox2.AppendText(V_PBW_STRING_0);
                        break;
                    case 1:
                        string V_PBW_STRING_1 = "PBW=" + (V_PBW_1 + 1) + "," + V_PBW_2 + "," + V_PBW_3 + "," + V_PBW_4 ;
                        textBox2.AppendText(V_PBW_STRING_1);
                        break;
                    case 2:
                        string V_PBW_STRING_2 = "PBW=" + V_PBW_1 + "," + (V_PBW_2 + 1) + "," + V_PBW_3 + "," + (V_PBW_4 + 1);
                        textBox2.AppendText(V_PBW_STRING_2);
                        break;
                    case 3:
                        string V_PBW_STRING_3 = "PBW=" + (V_PBW_1 + 1) + "," + V_PBW_2 + "," + (V_PBW_3 + 1) + "," + (V_PBW_4 +1);
                        textBox2.AppendText(V_PBW_STRING_3);
                        break;
                }
                textBox2.AppendText(Environment.NewLine);
                textBox2.AppendText("PBY=0,0,0");
                textBox2.AppendText(Environment.NewLine);
            }
            if (a==5)
            {
                int V_PBW_1 = V_PBW_I;
                int V_PBW_2 = V_PBW_I;
                int V_PBW_3 = V_PBW_I;
                int V_PBW_4 = V_PBW_I;
                int V_PBW_5 = V_PBW_I;

                switch (V_PBW_F)
                {
                    case 0:
                        string V_PBW_STRING_0 = "PBW=" + V_PBW_1 + "," + V_PBW_2 + "," + V_PBW_3 + "," + V_PBW_4 + "," + V_PBW_5;
                        textBox2.AppendText(V_PBW_STRING_0);
                        break;
                    case 1:
                        string V_PBW_STRING_1 = "PBW=" + (V_PBW_1 + 1) + "," + V_PBW_2 + "," + V_PBW_3 + "," + V_PBW_4 + "," + V_PBW_5;
                        textBox2.AppendText(V_PBW_STRING_1);
                        break;
                    case 2:
                        string V_PBW_STRING_2 = "PBW=" + V_PBW_1 + "," + (V_PBW_2 + 1) + "," + V_PBW_3 + "," + (V_PBW_4 + 1) + "," + V_PBW_5;
                        textBox2.AppendText(V_PBW_STRING_2);
                        break;
                    case 3:
                        string V_PBW_STRING_3 = "PBW=" + (V_PBW_1 + 1) + "," + V_PBW_2 + "," + (V_PBW_3 + 1) + "," + V_PBW_4 + "," + (V_PBW_5 + 1);
                        textBox2.AppendText(V_PBW_STRING_3);
                        break;
                    case 4:
                        string V_PBW_STRING_4 = "PBW=" + V_PBW_1 + "," + (V_PBW_2 + 1) + "," + (V_PBW_3 + 1) + "," + (V_PBW_4 + 1) + "," + (V_PBW_5 + 1);
                        textBox2.AppendText(V_PBW_STRING_4);
                        break;
                }
                textBox2.AppendText(Environment.NewLine);
                textBox2.AppendText("PBY=0,0,0,0");
                textBox2.AppendText(Environment.NewLine);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            P_kaishi_dianhuo();
            var save = new SaveFileDialog();
            save.Filter = "输出.ust文件 (*.ust)|*.ust";
            save.FileName = "输出_" + DateTime.Now.ToString("yyyyMMddHHmmss");//年月日时分秒
            if (save.ShowDialog() == DialogResult.OK && save.FileName != "")
            {
                var sw = new StreamWriter(save.FileName);
                for (var i = 0; i < textBox2.Lines.Length; i++)
                {
                    sw.WriteLine(textBox2.Lines.GetValue(i).ToString());
                }
                sw.Close();
            }
            P_jieshu_fashe();
            MessageBox.Show("保存成功");
            P_jieshu_jiesu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            P_kaishi_zhuangtian();
            string file = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择.ust文件";
            dialog.Filter = ".ust文件(*.*)|*.ust*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
            }
            Read(file);
            P_jieshu_zhuangtian();
            MessageBox.Show("导入成功");
             P_jieshu_jiesu();

        }

        public void Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                textBox1.AppendText(line);
                textBox1.AppendText(Environment.NewLine);
            }
        }

        public void P_kaishi_zhuangtian()
        {
            pictureBox1.Visible = true;
            this.pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\img\\kaishi.jpg");
            label6.Visible = true;
            label6.ForeColor = Color.Red; //颜色 
            label6.Text = "弹药装填中 ";

        }

        public void P_kaishi_paotayidong()
        {
            pictureBox1.Visible = true;
            this.pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\img\\kaishi.jpg");
            label6.Visible = true;
            label6.ForeColor = Color.Red; //颜色 
            label6.Text = "炮塔移动中 ";

        }

        public void P_kaishi_dianhuo()
        {
            pictureBox1.Visible = true;
            this.pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\img\\kaishi.jpg");
            label6.Visible = true;
            label6.ForeColor = Color.Red; //颜色 
            label6.Text = "  点火  ";

        }

        public void P_kaishi_jieshu()
        {
            label6.Visible = false;
            pictureBox1.Visible = false;

        }

        public void P_jieshu_zhuangtian()
        {
            pictureBox1.Visible = true;
            this.pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\img\\jieshu.jpg");
            label6.Visible = true;
            label6.ForeColor = Color.Green; //颜色 
            label6.Text = "装填完成";

        }

        public void P_jieshu_queren()
        {
            pictureBox1.Visible = true;
            this.pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\img\\jieshu.jpg");
            label6.Visible = true;
            label6.ForeColor = Color.Green; //颜色 
            label6.Text = "目标确认";

        }

        public void P_jieshu_fashe()
        {
            pictureBox1.Visible = true;
            this.pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\img\\jieshu.jpg");
            label6.Visible = true;
            label6.ForeColor = Color.Green; //颜色 
            label6.Text = "命中目标";

        }

        public void P_jieshu_jiesu()
        {
            label6.Visible = false;
            pictureBox1.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

  

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
