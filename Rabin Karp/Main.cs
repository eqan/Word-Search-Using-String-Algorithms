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
namespace Word_Search
{
    public partial class Main : Form
    {
        string fileString;
        bool checker = false;
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String textFile = "exp1.txt";
            String pattern = textBox1.Text;
            int ptnLength = pattern.Length;
            int readLength;
            int row = 1;
            textBox2.Text = "---RESULT BY BRUTE FORCE---\r\n\r\n";
            using (StreamReader file = new StreamReader(textFile))
            {
                while ((fileString = file.ReadLine()) != null)
                {
                    readLength = fileString.Length;
                    if(RabinKarp(fileString, pattern) > 0)
                    {
                        textBox2.Text += "\r\nRow Number: " + row.ToString()+"\r\n\r\n";
                    }
                    row++;
                }
                file.Close();
            }
        }
        public int RabinKarp(string txt, string pat)
        {
            String tmp;
            int d = 256;
            int prime = 7;
            int M = pat.Length;
            int N = txt.Length;
            int i, j;
            int p = 0;
            int t = 0; 
            int h = 1;
            for (i = 0; i < M - 1; i++)
            {
                h = (h * d) % prime;
            }
            for (i = 0; i < M; i++)
            {
                p = (d * p + pat[i]) % prime;
                t = (d * t + txt[i]) % prime;
            }
            for (i = 0; i <= N - M; i++)
            {
                tmp = "";
                if (p == t)
                {
                    for (j = 0; j < M; j++)
                    {
                        if (txt[i + j] != pat[j])
                        break;
                    }
                    if (j == M)
                    {
                        int x = i;
                        while (txt[x] != ' ')
                        {
                            x--;
                            if (x < 0)
                            {
                                break;
                            }
                        }
                        x++;
                        while (txt[x] != ' ')
                        {
                            tmp += txt[x];
                            x++;
                            if (x == N)
                            {
                                break;
                            }
                        }
                        if (checkBox1.Checked)
                        {
                            if (tmp != pat && tmp.ToLower() != pat.ToLower())
                            {
                                return -1;
                            }
                        }
                        textBox2.Text += "Word Found : " + tmp;
                        textBox2.Text += "\r\nColumn Number : " + i.ToString() + "\r\n";
                        checker = true;
                    }
                }
                if (i < N - M)
                {
                    t = (d * (t - txt[i] * h) + txt[i + M]) % prime;
                    if (t < 0)
                    t = (t + prime);
                }
            }
            if (checker)
            {
                checker = false;
                return 1;
            }
            return -1;
        }
    }
}
