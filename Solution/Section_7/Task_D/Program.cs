using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
// Числа Фибоначчи для взрослых
namespace Task_D
{
    /*
    Гоша практикуется в динамическом программировании — он хочет быстро считать числа Фибоначчи. 
    Напомним, что числа Фибоначчи определены как последовательность
    Формат ввода
    В единственной строке дано целое число n (0 ≤ n ≤ 106).
    Формат вывода
    Вычислите значение Fn по модулю 109 + 7 и выведите его.
     */

    // https://habr.com/ru/articles/83303/
    // https://habr.com/ru/articles/261159/
    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static int Mod = 1000000007;

        static void Main(string[] args)
        {
            var n = int.Parse(_reader?.ReadLine());

            if (n <= 1)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(GetFib(n));
            }
        }

        private static int GetFib(int n)
        {
            int a = 1;
            int b = 1;

            for (int i = 2; i <= n; i++)
            {
                a %= Mod;
                b %= Mod;
                (a, b) = (b, (a + b) % Mod);
            }

            return b;
        }
    }
}