using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var testString = @"Дон ли, Волга ли течет";

            SuffixTree tree = new SuffixTree();

            tree.AddRange(testString);

           /* foreach (var item in testString)
            {

                var res = tree.IsSuffixExist(sb.ToString());

                if (res != true)
                    throw new Exception();
            }*/
            

            SuffixTreeSpan span = new SuffixTreeSpan("пидорас".ToCharArray(), 0, null);

            var (head, tail) = span.Split(5);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
