using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgAndStructs_RGZ_SuffixTree
{
    public partial class Form1 : Form
    {
        SuffixTree _suffixTree;

        public Form1()
        {
            InitializeComponent();
        }

        private void create_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            var sw = new Stopwatch();
            var blockLength = 500;
            var counter = 0;
            var i = 0;
            var fullTime = 0L;
            _suffixTree = new SuffixTree();
            foreach (var item in richTextBox1.Text)
            {
                sw.Start();
                _suffixTree.NewAdd(item);
                sw.Stop();
                chart1.Series[0].Points.Add(sw.ElapsedTicks);
                fullTime += sw.ElapsedTicks;
                sw.Reset();

                counter++;
            }
            textBox2.Text = fullTime.ToString();
            memUsetextBox.Text = _suffixTree.GetTreeSize().ToString();
        }

        private void check_Click(object sender, EventArgs e)
        {
            var arr = richTextBox1.Text.ToCharArray();

            for (int i = 1; i < arr.Length; i++)
            {
                var tempArr = new char[i];

                for (int j = 0; j < tempArr.Length; j++)
                {
                    tempArr[j] = arr[arr.Length - i + j];
                }

                var result = _suffixTree.IsSuffixExist(new string(tempArr));

                if (!result)
                {
                    textBox1.Text = "Неудача";
                    return;
                }
            }
            textBox1.Text = "Успешно";
        }

        private string CreateTestData(char from, char to, int length)
        {
            var rand = new Random();
            var sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                var value = rand.Next(from, to + 1);
                sb.Append(Convert.ToChar(value));
            }
            sb.Append('$');
            return sb.ToString();
        }

        private void seqCreateButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = CreateTestData(
                char.Parse(seqFromTextBox.Text),
                char.Parse(seqToTextBox4.Text), 
                int.Parse(seqLengthTextBox.Text)
                );
        }
    }
}
