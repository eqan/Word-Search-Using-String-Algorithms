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
            textBox2.Text = "---RESULT BY KMP---\r\n\r\n";
            using (StreamReader file = new StreamReader(textFile))
            {
                while ((fileString = file.ReadLine()) != null)
                {
                    readLength = fileString.Length;
                    if(KMP(fileString, pattern, readLength, ptnLength) > 0)
                    {
                        textBox2.Text += "\r\nRow Number: " + row.ToString()+"\r\n\r\n";
                    }
                    row++;
                }
                file.Close();
            }
        }
        public int KMP(string Text, string Pattern, int N, int M)
        {
            String tmp = "";
            int[] prefixArray = new int[M];
            int matches = 0;
            prefixArray[0] = matches;
            for (int i = 1; i < M; i++)
            {
                if (Pattern[i] == Pattern[matches])
                {
                    prefixArray[i] = ++matches;
                }
                else
                {
                    matches = 0;
                    prefixArray[i] = matches;
                }
            }
            int j = 0;
            for(int i = 0; i <= N - M; i++)
            {
                tmp = "";
                if (Char.ToLower(Text[i]) == Char.ToLower(Pattern[0]))
                {
                    for(j = 0; j < M; j++)
                    {
                        if (checkBox2.Checked)
                        {
                            if (Text[i + j] != Pattern[j])
                            {
                                i = i + j - prefixArray[j]-1;
                                break;
                            }
                        }
                        else
                        {
                            if (Char.ToLower(Text[i + j]) != Pattern[j] && Char.ToUpper(Text[i + j]) != Pattern[j])
                            {
                                i = i + j - prefixArray[j]-1;
                                break;
                            }
                        }
                    }
                    if (j == M)
                    {
                        int x = i;
                        while (Text[x] != ' ')
                        {
                            x--;
                            if (x < 0)
                            {
                                break;
                            }
                        }
                        x++;
                        while (Text[x] != ' ')
                        {
                            tmp += Text[x];
                            x++;
                            if (x == N)
                            {
                                break;
                            }
                        }
                        if (checkBox1.Checked)
                        {
                            if (tmp != Pattern && tmp.ToLower()!=Pattern.ToLower())
                            {
                                return -1;
                            }
                        }
                        textBox2.Text += "Word Found : " + tmp;
                        textBox2.Text += "\r\nColumn Number : " + i.ToString() + "\r\n";
                        checker = true;
                        i++;
                    }
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
