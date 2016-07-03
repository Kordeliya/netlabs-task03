using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TASKaboutDelegate
{
    class Program
    {
        private static string _fileInput;
        private static string _fileOutput;
        private static string _skipSign;

        static void Main(string[] args)
        {
            Console.WriteLine("Подсчет суммы чисел и символах в строках!");

            _fileInput = ConfigurationManager.AppSettings["InputFile"];
            _fileOutput = ConfigurationManager.AppSettings["OutputFile"];
            _skipSign = ConfigurationManager.AppSettings["SkipSign"];
            
            WorkerWithInputFile worker = new WorkerWithInputFile();
            int sumInt = 0;
            int sumChar = 0;

            worker.onEndRead += Messages.MessageAboutEndRead;
            worker.onEndCount += Messages.MessageAboutEndCount;
            worker.onEndWrite += Messages.MessageAboutEndWrite;
            worker.onException += Messages.MessageException;

            string[] lines = worker.ReadFile(_fileInput, _skipSign);
            if (lines.Length > 0)
            {
                worker.GetSumLines(lines, out sumInt, out sumChar);
            }

            Console.WriteLine("Арифмитическая сумма равняется:{0}", sumInt);
            Console.WriteLine("Сумма символов в символьных строках равняется:{0}", sumChar);

            worker.WriteResult(_fileOutput, sumInt, sumChar);
            Console.ReadKey();

        }
    }
}
