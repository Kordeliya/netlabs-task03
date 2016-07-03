using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASKaboutDelegate
{
    public static class Messages
    {
        /// <summary>
        /// Оповещение о конце чтения
        /// </summary>
        /// <param name="message"></param>
        public static void MessageAboutEndRead(string message = null)
        {
            Console.WriteLine("Окончание считывания файла");
        }

        /// <summary>
        /// Оповещание о конце подсчета
        /// </summary>
        /// <param name="message"></param>
        public static void MessageAboutEndCount(string message = null)
        {
            Console.WriteLine("Окончание подсчета сумм чисел и символов");
        }

        /// <summary>
        /// Оповещение о конце записи
        /// </summary>
        /// <param name="message"></param>
        public static void MessageAboutEndWrite(string message = null)
        {
            Console.WriteLine("Окончание записи результатов");
        }

        /// <summary>
        /// Оповещение о произошедшей ошибке
        /// </summary>
        /// <param name="message"></param>
        public static void MessageException(string message = null)
        {
            Console.WriteLine("При выполнении произошла ошибка: {0}", message);
        }
    }
}
