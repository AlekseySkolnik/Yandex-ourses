using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Подсчёт префикс-функции
namespace Task_L
{
    /*
    В этой задаче вам необходимо посчитать префикс-функцию для заданной строки.
    Если длина входной строки L, то выведите через пробел L целых неотрицательных чисел —
    массив значений префикс-функции исходной строки.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var text = _reader?.ReadLine() ?? throw new InvalidDataException();

            var prefix = OptimizedPrefixFunc(text);

            Console.WriteLine(string.Join(' ', prefix));
        }

        public static int[] OptimizedPrefixFunc(string s)
        {
            var N = s.Length;
            var p = new int[N];

            for (int i = 1; i < N; i++)
            {
                int k = p[i - 1];

                while (k > 0 && s[k] != s[i])
                {
                    k = p[k - 1];
                }

                if (s[k] == s[i])
                {
                    k++;
                }

                p[i] = k;
            }

            return p;
        }
    }
}