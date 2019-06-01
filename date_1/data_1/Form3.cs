using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace data_1
{
    public partial class Form3 : Form
    {
        private List<List<String>> P;
        private List<List<String>> N;
        int num;
        public Form3()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            P = new List<List<String>>();
            N = new List<List<String>>();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            StreamReader fs = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\in.txt");
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            //Console.WriteLine(fs);
            String te = fs.ReadLine();
            int.TryParse(te, out num);
            Console.WriteLine(num);
            for (int i = 0; i < num; i++)
            {
                String s = fs.ReadLine();
                // Console.WriteLine(s);
                List<String> temp = new List<String>();
                int cnt = 0, j = 0;
                while (true)
                {
                    String temps = "";
                    while (s[j] != ' ' || (s[j] == ' ' && temps[0] == 'V' && temps.Length == 4)) temps += s[j++];
                    j++;
                    Console.WriteLine(temps);
                    cnt++; temp.Add(temps);
                    if (cnt == 4)
                    {
                        if (s[s.Length - 1] == 'P') P.Add(temp);
                        else N.Add(temp);
                        break;
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String s1 = comboBox1.Text.ToString();
            String s2 = comboBox2.Text.ToString();
            String s3 = comboBox3.Text.ToString();
            String s4 = comboBox4.Text.ToString();
            Console.WriteLine(s1 + "!" + s2 + "!" + s3 + "!" + s4);
            int num_s1_N = 0;
            int num_s1_P = 0;
            int num_s2_N = 0;
            int num_s2_P = 0;
            int num_s3_N = 0;
            int num_s3_P = 0;
            int num_s4_N = 0;
            int num_s4_P = 0;
            foreach (List<String> ls in N)
            {
                num_s1_N += ls[0] == s1 ? 1 : 0;
                num_s2_N += ls[1] == s2 ? 1 : 0;
                num_s3_N += ls[2] == s3 ? 1 : 0;
                num_s4_N += ls[3] == s4 ? 1 : 0;
            }
            foreach (List<String> ls in P)
            {
                num_s1_P += ls[0] == s1 ? 1 : 0;
                num_s2_P += ls[1] == s2 ? 1 : 0;
                num_s3_P += ls[2] == s3 ? 1 : 0;
                num_s4_P += ls[3] == s4 ? 1 : 0;
            }
            int num_N = N.Count;
            double P_N = 0;
            double xx = ((1.0 * (num_s1_N + num_s1_P) / num) * (1.0 * (num_s2_N + num_s2_P) / num) * (1.0 * (num_s3_N + num_s3_P) / num) * (1.0 * (num_s4_N + num_s4_P) / num));
            if (num_N != 0) P_N = (1.0 * num_s1_N / num_N) * (1.0 * num_s2_N / num_N) * (1.0 * num_s3_N / num_N) * (1.0 * num_s4_N / num_N) * (1.0 * num_N / num) / xx;
            Console.WriteLine("xx = " + xx + "num_N = " + num_N);
            int num_P = P.Count;
            double P_P = 0;
            if (num_P != 0) P_P = (1.0 * num_s1_P / num_P) * (1.0 * num_s2_P / num_P) * (1.0 * num_s3_P / num_P) * (1.0 * num_s4_P / num_P) * (1.0 * num_P / num) / xx;
            output.Text = "没有感冒的概率为 " + P_N.ToString() + "\r\n" + "感冒的概率为 " + P_P.ToString() + "\r\n";
            if (P_N >= P_P) output.Text += "应该没有感冒嗷!";
            else output.Text += "好像感冒了嗷!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
