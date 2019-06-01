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
    public partial class Form4 : Form
    {
        private List<List<double>> LS;
        private List<List<double>>A, B, C;
        private List<List<double>>rc;
        int n;
        public Form4()
        {
            InitializeComponent();
        }
        private double cal_len(List<double>a, List<double>b) {
            double res = 0;
            for(int i = 0; i < 4; i++)
            {
                res += (a[i]-b[i])*(a[i]-b[i]);
            }
            return res;
        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }

        private void cal() {
            rc = new List<List<double>>();
            List<double> temp = new List<double>();
            for (int i = 1; i < 5; i++) temp.Add(LS[0][i]);
            rc.Add(temp);
            temp = new List<double>();
            for (int i = 1; i < 5; i++) temp.Add(LS[1][i]);
            rc.Add(temp);
            temp = new List<double>();
            for (int i = 1; i < 5; i++) temp.Add(LS[2][i]);
            rc.Add(temp);
            for (int times = 1; times <= 100; times++) {
                A = new List<List<double>>();
                B = new List<List<double>>();
                C = new List<List<double>>();
                for (int i  = 0; i < n; i++)
                {
                    List<double> vs = new List<double>();
                    for (int j = 1; j < 5; j++) vs.Add(LS[i][j]);
                    double mx = 1e9 + 9;
                    int pos = -1;
                    for (int j = 0; j < 3; j++)
                    {
                        double ss = cal_len(vs, rc[j]);
                        if(ss < mx)
                        {
                            mx = ss;
                            pos = j;
                        }
                    }
                    vs.Insert(0, LS[i][0]);
                    if (pos == 0) A.Add(vs);
                    else if (pos == 1) B.Add(vs);
                    else if (pos == 2) C.Add(vs);
                }

                rc = new List<List<double>>();
                List<double> cent_A = new List<double>();
                for (int j = 0; j < 4; j++) cent_A.Add(0);
                foreach (List<double> vs in A)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        cent_A[j] += vs[j+1];
                    }
                }
                for (int j = 0; j < 4; j++) cent_A[j] /= A.Count;
                rc.Add(cent_A);

                List<double> cent_B = new List<double>();
                for (int j = 0; j < 4; j++) cent_B.Add(0);
                foreach (List<double> vs in B)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        cent_B[j] += vs[j + 1];
                    }
                }
                for (int j = 0; j < 4; j++) cent_B[j] /= B.Count;
                rc.Add(cent_B);

                List<double> cent_C = new List<double>();
                for (int j = 0; j < 4; j++) cent_C.Add(0);
                foreach (List<double> vs in C)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        cent_C[j] += vs[j+1];
                    }
                }
                for (int j = 0; j < 4; j++) cent_C[j] /= C.Count;
                rc.Add(cent_C);
            }
            String res = "";

            Console.WriteLine("A类:");
            res += "A类:\r\n";
            foreach (List<double> s in A)
            {
                Console.WriteLine("编号 " + s[0].ToString() + " 萼片长 =" + s[1].ToString() + " 萼片宽 =" + s[2].ToString() + " 花瓣长度 =" + s[3].ToString() + "花瓣宽度 = "+s[4].ToString());
                res += "编号 " + s[0].ToString() + " 萼片长 =" + s[1].ToString() + " 萼片宽 =" + s[2].ToString() + " 花瓣长度 =" + s[3].ToString() + "花瓣宽度 = "+s[4].ToString()+"\r\n";
            }
            Console.WriteLine("B类:");
            res += "B类:\r\n";
            foreach (List<double> s in B)
            {
                Console.WriteLine("编号 " + s[0].ToString() + " 萼片长 =" + s[1].ToString() + " 萼片宽 =" + s[2].ToString() + " 花瓣长度 =" + s[3].ToString() + "花瓣宽度 = " + s[4].ToString());
                res += "编号 " + s[0].ToString() + " 萼片长 =" + s[1].ToString() + " 萼片宽 =" + s[2].ToString() + " 花瓣长度 =" + s[3].ToString() + "花瓣宽度 = " + s[4].ToString() + "\r\n";
            }
            Console.WriteLine("C类:");
            res += "C类:\r\n";
            foreach (List<double> s in C)
            {
                Console.WriteLine("编号 " + s[0].ToString() + " 萼片长 =" + s[1].ToString() + " 萼片宽 =" + s[2].ToString() + " 花瓣长度 =" + s[3].ToString() + "花瓣宽度 = " + s[4].ToString());
                res += "编号 " + s[0].ToString() + " 萼片长 =" + s[1].ToString() + " 萼片宽 =" + s[2].ToString() + " 花瓣长度 =" + s[3].ToString() + "花瓣宽度 = " + s[4].ToString() + "\r\n";
            }
            output.Text = res;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LS = new List<List<double>>();
            StreamReader fs = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\date.txt");
            n = int.Parse(fs.ReadLine());
            Console.WriteLine(n);
            for(int i = 0; i < n; i++)
            {
                String temp = fs.ReadLine();
                int p = 0, len = temp.Length;
                List<double> ts = new List<double>();
                for (int j = 0; j < 5; j++) {
                    String t = ""; 
                    while (p < len && temp[p] != ' ') t += temp[p++];
                    p++;
                    ts.Add(double.Parse(t));
                    Console.WriteLine(t);
                }
                LS.Add(ts);
            }
            cal();
        }
    }
}