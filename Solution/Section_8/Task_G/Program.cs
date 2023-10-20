using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Поиск со сдвигом
namespace Task_G
{
    /*
    Гоша измерял температуру воздуха n дней подряд. 
    В результате у него получился некоторый временной ряд. 
    Теперь он хочет посмотреть, как часто встречается некоторый шаблон в получившейся последовательности.
    Однако температура — вещь относительная, поэтому Гоша решил, что при поиске шаблона длины m (a1, a2, ..., am)
    стоит также рассматривать сдвинутые на константу вхождения.
    Это значит, что если для некоторого числа c в исходной последовательности нашёлся участок
    вида (a1 + c, a2 + c, ... , am + c), то он тоже считается вхождением шаблона (a1, a2, ..., am).
    По заданной последовательности измерений X и шаблону A=(a1, a2, ..., am) определите все вхождения A в X,
    допускающие сдвиг на константу.
    Подсказка: если вы пишете на питоне и сталкиваетесь с TL, 
    то попробуйте заменить какие-то из циклов операциями со срезами.

    Формат вывода
    Выведите через пробел в порядке возрастания все позиции, на которых начинаются вхождения
    шаблона A в последовательность X. 
    Нумерация позиций начинается с единицы.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var n = int.Parse(_reader?.ReadLine());
            var measurements = ReadList();
            var m = int.Parse(_reader?.ReadLine());
            var templates = ReadList();

            var text = measurements[0..n].AsSpan();
            var pattern = templates[0..m].AsSpan();
            int start = 0;
            var occurrences = new List<int>();
            int pos;

            while ((pos = Find(text, pattern, start)) != -1)
            {
                start = pos + 1;
                occurrences.Add(start);
            }

            Console.WriteLine(string.Join(' ', occurrences));
        }

        private static int Find(Span<int> text, Span<int> pattern, int start = 0)
        {
            if (text.Length < pattern.Length)
            {
                return -1;
            }

            for (int pos = start; pos <= text.Length - pattern.Length; pos++)
            {
                var match = true;
                var diff = text[pos] - pattern[0];

                for (int offset = 0; offset < pattern.Length; offset++)
                {
                    if (text[pos + offset] - pattern[offset] != diff)
                    {
                        match = false;
                        break;
                    }
                }

                if (match == true)
                {
                    return pos;
                }
            }

            return -1;
        }

        private static int[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();
    }
}