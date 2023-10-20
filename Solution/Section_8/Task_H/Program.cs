using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Глобальная замена
namespace Task_H
{
    /*
    Напишите программу, которая будет заменять в тексте все вхождения строки s на строку t. 
    Гарантируется, что никакие два вхождения шаблона s не пересекаются друг с другом.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var text = _reader?.ReadLine() ?? throw new InvalidDataException();
            var pattern = _reader?.ReadLine() ?? throw new InvalidDataException();
            var template = _reader?.ReadLine() ?? throw new InvalidDataException();

            var startPoints = MemoryFindSubstring(pattern, text);

            var result = new List<char>();
            var start = 0;
            var stop = text.Length;

            foreach (var point in startPoints)
            {
                if (start < point)
                {
                    result.AddRange(text[start..point]);
                }

                result.AddRange(template);
                start = point + pattern.Length;
            }

            if (start < stop)
            {
                result.AddRange(text[start..stop]);
            }

            Console.WriteLine(string.Join("", result));
        }

        // не оптимизированный поиск по памяти - O(∣p∣+∣t∣) дополнительной памяти 
        public static List<int> FindSubstring(string pattern, string text)
        {
            var result = new List<int>();
            string s = pattern + "#" + text;
            var prefix = new int[s.Length];

            for (int i = 1; i < s.Length; i++)
            {
                int k = prefix[i - 1];

                while (k > 0 && s[k] != s[i])
                {
                    k = prefix[k - 1];
                }

                if (s[k] == s[i])
                {
                    k++;
                }

                prefix[i] = k;
            }

            for (int i = 0; i < prefix.Length; i++)
            {
                if (prefix[i] == pattern.Length)
                {
                    result.Add(i - 2 * pattern.Length);
                }
            }

            return result;
        }

        // Оптимизация по памяти - O(∣p∣) памяти
        // Промежуточные значения префикс-функции можно не запоминать:
        // dp всегда обращается только к последнему вычисленному значению префикс-функции и первым ∣p∣ значениям.
        public static List<int> MemoryFindSubstring(string pattern, string text)
        {
            var result = new List<int>();
            string s = pattern + "#" + text;
            var prefix = new int[pattern.Length + 1]; // хранить префикс-функцию только от первых ∣p∣ символов комбинированной строки
            prefix[0] = 0;

            int prefix_prev = 0; // и  префикс-функцию для последнего обрабатываемого элемента. 

            // В таком случае построение префикс-функции и поиск её максимального значения будут совмещены.
            for (int i = 1; i < s.Length; i++)
            {
                var k = prefix_prev;

                while (k > 0 && s[k] != s[i])
                {
                    k = prefix[k - 1];
                }

                if (s[k] == s[i])
                {
                    k++;
                }

                if (i <= pattern.Length)
                {
                    prefix[i] = k;
                }

                prefix_prev = k;

                if (k == pattern.Length)
                {
                    result.Add(i - 2 * pattern.Length);
                }
            }

            return result;
        }
    }
}