using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace data_1
{
    public partial class Form2 : Form
    {
        private int n, pos;
        private double minx = 0.3;
        private String In;
        private Dictionary<List<String>, int>date;
        private Dictionary<String, int>num;
        private Dictionary<List<String>, bool> fre_set;
        private List<List<List<String>>> kth_fre;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Dictionary<List<String>, bool> getkth(Dictionary<List<String>, bool> x, int times)
        {
            Dictionary<List<String>, bool> res = new Dictionary<List<String>, bool>();
            Dictionary<List<String>, bool>.KeyCollection kco = x.Keys;
            List<List<String>>ls = new List<List<String>>();
            foreach (List<String> ss in kco)
            {
                ls.Add(ss);
                foreach(String test in ss)
                {
                    Console.Write(" "+test);
                }
                Console.WriteLine("end");
            }
            for(int i = 0; i < ls.Count; i++)
            {
                for(int j = i+1; j < ls.Count; j++)
                {
                    if(ls[i].Count == 1)
                    {
                        List<String> temp = new List<String>();
                        temp.Add(ls[i][0]);
                        temp.Add(ls[j][0]);
                        temp.Sort();
                        if (!res.ContainsKey(temp))
                        {
                            res.Add(temp, true);
                        }
                    }
                    else
                    {
                        int flag = 1;
                        for(int k = 0; k < ls[i].Count; k++)
                        {
                            if(k < ls[i].Count-1)
                            {
                                if(ls[i][k] != ls[j][k])
                                {
                                    if (i == 0 && j == 2) Console.WriteLine("pre!!!!!!");
                                    Console.WriteLine(ls[i][k] + " " + ls[j][k]+"k = "+k+" len" + ls[i].Count);
                                    for(int cc = 0; cc < ls[i].Count; cc++)
                                    {
                                        Console.WriteLine(ls[i][cc]);
                                    }
                                    flag = 0;
                                    break;
                                }
                            }
                            else
                            {
                                if(ls[i][k] == ls[j][k])
                                {
                                    if (i == 0 && j == 2) Console.WriteLine("last!!!!!!");
                                    flag = 0;
                                    break;
                                }
                            }
                        }
                        if (i == 0 && j == 2) Console.WriteLine("flag = {0}", flag);
                        if(flag == 1)
                        {
                            List<String> temp = new List<string>();
                            foreach (String cc in ls[i]) temp.Add(cc);
                            temp.Add(ls[j][ls[j].Count - 1]);
                            temp.Sort();
                            if (!res.ContainsKey(temp)) res.Add(temp, true);
                        }
                    }
                }
            }
            //Dictionary<List<String>, bool>.KeyCollection test = res.Keys;
            //foreach(List<String> s in test)
            //{
             //   foreach(String xx in s)
             //   {
             //       Console.Write("{0},", xx);
             //   }
              //  Console.WriteLine();
            //}
            return res;
        }

        private int Getnum()
        {
            int temp = 0;
            int len = In.Length;
            while (pos < len && In[pos] >= '0' && In[pos] <= '9')
            {
                temp = temp * 10 + In[pos] - '0';
                pos++;
            }
            pos++;
            return temp;
        }
        private String GetString()
        {
            String res = "";
            while(pos < In.Length && !(In[pos] == ' '))
            {
                res += In[pos];
                pos++;
            }
            pos++;
            return res;
        }
        private int Get_kthnum(List<String>ls)
        {
            int res = 0;
            Dictionary<List<String>, int>.KeyCollection temp = date.Keys;
            //Console.Write("list = ");
            //foreach (String x in ls)
            //{
            //    Console.Write("{0},", x);
           // }
            if(ls.Count == 0)
            {
                Console.WriteLine("没有元素");
                return 0;
            }
            foreach(List<String>s in temp)
            {
                int flag = 1;
                foreach(String x in ls)
                {
                    if(!s.Contains(x))
                    {
                        flag = 0;
                        break;
                    }
                }
                if (flag == 1) res += date[s];
            }
            //Console.WriteLine("yes?{0}", res>=minx);
            return res;
        }
        private void get_relation(int up)
        {
            String res = "";
            int cnt = 0;
            for(int i = 0; i < up; i++)
            {
                for(int j = i+1; j < up; j++)
                {
                    foreach(List<String> ss in kth_fre[i])
                    {
                        foreach(List<String> sr in kth_fre[j])
                        {
                            int flag = 1;
                            foreach(String s in ss)
                            {
                                if(!sr.Contains(s))
                                {
                                    flag = 0;
                                    break;
                                }
                            }
                            if(flag == 1)
                            {
                                int x = Get_kthnum(ss), y = Get_kthnum(sr);
                                if(1.0*y/x >= 0.7)
                                {
                                    cnt++;
                                    Console.WriteLine("关联规则：");
                                    res += "关联规则：" + (1.0*y/x).ToString() + "\r\n";
                                    foreach (String s in ss)
                                    {
                                        Console.Write("{0} ",s);
                                        res += s + " ";
                                    }
                                    Console.Write("--->");
                                    res += "--->";
                                    foreach(String s in sr)
                                    {
                                        Console.Write("{0} ", s);
                                        res += s + " ";
                                    }
                                    res += "\r\n";
                                }
                            }
                        }
                    }
                }
            }
            output.Text += res;
            Console.WriteLine("cnt = {0}", cnt);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String res = "";
            date = new Dictionary<List<String>, int>();
            num = new Dictionary<String, int>();
            fre_set = new Dictionary<List<String>, bool>();
            In = input.Text.ToString();
            In = In.Replace(System.Environment.NewLine, " ");
            In = In.Replace(",", " ");
            pos = 0;
            n = Getnum();
            Console.WriteLine(In);
            Console.WriteLine("n = " + n + "x = " + minx);
            for (int i = 1; i <= n; i++)
            {
                int k = Getnum();
                List<String> ls = new List<String>();
                Console.WriteLine(k);
                for (int j = 0; j < k; j++)
                {
                    String x = GetString();
                    ls.Add(x);
                    Console.WriteLine(x);
                }
                ls.Sort();
                if (!date.ContainsKey(ls)) date.Add(ls, 1);
                else date[ls] = date[ls] + 1;
            }

            Dictionary<List<String>, int>.KeyCollection temp = date.Keys;
             foreach(List<String> x in temp)
             {
                    foreach(String i in x)
                    {
                        if (!num.ContainsKey(i)) num.Add(i, 1);
                        else num[i] = num[i] + 1;
                     
                    }                
             }
            Dictionary<String, int>.KeyCollection ss = num.Keys;
            Dictionary<List<String>, bool> kth = new Dictionary<List<String>, bool>();
            kth_fre = new List<List<List<string>>>();
            foreach (String x in ss)
            {
                if (1.0*num[x]/n >= minx)
                {
                    List<String> tls = new List<String>();
                    tls.Add(x);
                    //Console.WriteLine("频繁1项集：" + x);
                    res += "频繁1项集：" + x +"\r\n";
                    fre_set.Add(tls, true);
                    kth.Add(tls, true);
                    if (kth_fre.Count == 0)
                    {
                        List<List<String>> tx = new List<List<string>>();
                        tx.Add(tls);
                        kth_fre.Add(tx);
                    }
                    else kth_fre[0].Add(tls);
                }
            }
            if (fre_set.Count() == 0) { res += "无！！"; return; }
            int times = 1;
            while(true)
            {
                times++;
                //kth_fre[times] = new List<List<string>>();
                kth = getkth(kth, times);
                if (kth.Count == 0) { output.Text = res; Console.WriteLine("退出"); get_relation(kth_fre.Count); return; }
                Dictionary<List<String>, bool>.KeyCollection tt = kth.Keys;
                Dictionary<List<String>, bool> ttemp = new Dictionary<List<string>, bool>();
                Console.WriteLine("候选集");
               // res += "候选" + times + "项集\r\n";
                //foreach (List<String> x in tt)
               // {
               //     foreach(String k in x)
               //     {
                //        Console.Write(" " + k);
                //        res += " "+k;
                //    }
                //    res += "\r\n";
                  //  Console.WriteLine();
               // }
                int flag = 0;
                foreach (List<String> x in tt)
                {
                    if(1.0*Get_kthnum(x)/n >= minx)
                    {
                        flag = 1;
                        Console.WriteLine("频繁{0}项集:", times);
                        res += "频繁" + times + "项集:";
                        foreach(String k in x)
                        {
                            Console.WriteLine("{0},", k);
                            res += " " + k;
                        }
                        res += "\r\n";
                        if(!fre_set.ContainsKey(x)) fre_set.Add(x, true);
                        if (!ttemp.ContainsKey(x)) ttemp.Add(x, true);
                        if(kth_fre.Count == times -1)
                        {
                            List<List<String>> tx = new List<List<string>>();
                            tx.Add(x);
                            kth_fre.Add(tx);
                        }
                        else if (!kth_fre[times-1].Contains(x)) kth_fre[times-1].Add(x);
                    }
                }
                if (flag == 0) { output.Text = res;Console.WriteLine("退出");get_relation(kth_fre.Count); return; }
                kth = ttemp;
            }
        }
    }
}
