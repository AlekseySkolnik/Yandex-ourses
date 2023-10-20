using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Разворот строки
namespace Task_A
{
    /*
    В некоторых языках предложения пишутся и читаются не слева направо, а справа налево.
    Вам под руку попался странный текст –— в нём обычный (слева направо) порядок букв в словах.
    А вот сами слова идут в противоположном направлении.
    Вам надо преобразовать текст так, чтобы слова в нём были написаны слева направо.

    Формат вывода
    Выведите строку с обратным порядком слов в ней.

     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var inputStr = _reader?.ReadLine();

            var words = inputStr.Split(' ');
            var builder = new StringBuilder(inputStr.Length);

            for (int i = words.Length - 1; i >= 0; i--)
            { 
                if (i != 0)
                {
                    builder.Append($"{words[i]} ");
                }
                else
                {
                    builder.Append(words[0]);
                }
            }

            Console.WriteLine(builder.ToString());

        }
    }
}