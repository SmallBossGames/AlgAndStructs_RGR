using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgAndStructs_RGZ_SuffixTree
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SuffixTree tree = new SuffixTree();

            tree.AddRange("abcabde");

            SuffixTreeSpan span = new SuffixTreeSpan("пидорас".ToCharArray(), 0, null);

            var (head, tail) = span.Split(5);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
