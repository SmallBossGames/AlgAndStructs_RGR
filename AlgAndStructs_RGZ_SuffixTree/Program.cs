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
            var testString =
                @"Дон ли, Волга ли течёт, котомку на плечо
Боль в груди — там тайничок, открытый фомкой, не ключом
Сколько миль еще, перелет короткий был не в счет
Долгий пыльный чёс, фургон набит коробками с мерчём
Верим — подфартит, наши постели портативны
Менестре$";

            var arr = testString.ToCharArray();

            SuffixTree tree = new SuffixTree();

            tree.AddRange(testString);

            for (int i = 1; i < arr.Length; i++)
            {
                var tempArr = new char[i];

                for (int j = 0; j < tempArr.Length; j++)
                {
                    tempArr[j] = arr[arr.Length - i + j];
                }

                var result = tree.IsSuffixExist(new string(tempArr));

                if(!result)
                {
                    throw new Exception();
                }
            }


            SuffixTreeSpan span = new SuffixTreeSpan("пидорас".ToCharArray(), 0, null);

            var (head, tail) = span.Split(5);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
