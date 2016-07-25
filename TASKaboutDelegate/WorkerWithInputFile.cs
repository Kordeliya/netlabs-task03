using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASKaboutDelegate
{
    /// <summary>
    /// Класс обработки входящего файла
    /// </summary>
    public class WorkerWithInputFile
    {
        private int _sumIntegers;
        private int _sumChars;

        public delegate int ReadLine(string line);
        public delegate void MessageEventHandler(string message = null);
        public delegate void MethodAdd(int value);

        public event MessageEventHandler onEndRead;
        public event MessageEventHandler onEndCount;
        public event MessageEventHandler onEndWrite;
        public event MessageEventHandler onException;
        
        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="txt">файл, который будет считываться</param>
        /// <param name="split">разделитель строк</param>
        public string[] ReadFile(string file, String split)
        {
            string text = String.Empty;
            //чтение
            if (File.Exists(file))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    text = sr.ReadToEnd();
                }
                if(onEndRead != null)
                    onEndRead();
            }
            else
            {
                return null;
            }
            return text.Split(split.ToCharArray());           
        }

        /// <summary>
        /// Получение сумм: арифметической и суммы символов
        /// </summary>
        /// <param name="lines">строки</param>
        /// <param name="arithmeticSum"></param>
        /// <param name="sumChar"></param>
        public void GetSumLines(string[] lines, out int arithmeticSum, out int sumChar)
        {
            if (lines != null)
            {
                foreach (var item in lines)
                {
                    int value;
                    if (Int32.TryParse(item, out value))
                        AddValue(AddArithmeticSum, value);
                    else
                        AddValue(AddValueChars, item.Length);
                }
            }
            arithmeticSum = _sumIntegers;
            sumChar = _sumChars;
            if (onEndCount != null)
                onEndCount();
        }

        /// <summary>
        /// Добавление нового значения в сумму
        /// </summary>
        /// <param name="func">функция добавления</param>
        /// <param name="value">значение</param>
        public void AddValue(MethodAdd func, int value)
        {
            if(func != null)
                func(value);
        }

        /// <summary>
        /// Добавление значения к сумме арифмитической
        /// </summary>
        /// <param name="value"></param>
        public void AddArithmeticSum(int value)
        {
            _sumIntegers += value;
        }

        /// <summary>
        /// Добавление значения к сумме символов
        /// </summary>
        /// <param name="value"></param>
        public void AddValueChars(int value)
        {
            _sumChars += value;
        }


        /// <summary>
        /// Вывод результатов в файл
        /// </summary>
        /// <param name="file">имя файоа</param>
        /// <param name="sumInt">арифметическая сумма</param>
        /// <param name="SumChars">сумма символов</param>
        public void WriteResult(string file, int sumInt, int SumChars)
        {
            try
            {
                string text = String.Format("Aрифметическая сумма чисел равняется {0}. {1}Сумма символов в строках равняется: {2}",
                                            sumInt, Environment.NewLine, SumChars);

                //чтение
                using (StreamWriter sr = new StreamWriter(file))
                {
                    sr.Write(text);
                }
            }
            catch (IOException ex)
            {
                if (onException!= null)
                    onException(String.Format("IOException: {0}",ex.Message));
            }
            if(onEndWrite != null)
                onEndWrite();
        }
    }
}
