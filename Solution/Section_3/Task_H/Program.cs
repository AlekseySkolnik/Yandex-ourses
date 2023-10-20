using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Большое число
namespace Task_H;
/*
Вечером ребята решили поиграть в игру «Большое число».
Даны числа. Нужно определить, какое самое большое число можно из них составить.

Формат ввода
В первой строке записано n — количество чисел. Оно не превосходит 100.
Во второй строке через пробел записаны n неотрицательных чисел, каждое из которых не превосходит 1000.

Формат вывода
Нужно вывести самое большое число, которое можно составить из данных чисел
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var n = byte.Parse(_reader.ReadLine()); //количество чисел (до 100)
        var digits = ReadArray();

        Array.Sort(digits, (a, b) =>
        {
            if (a == b)
            {
                return 0;
            }

            if (a.Length == b.Length)
            {
                return string.Compare(b, a);
            }

            var x = a + b;
            var y = b + a;

            return string.Compare(y, x);
        });

        Console.WriteLine(string.Join("", digits));
    }



    private static string[] ReadArray() =>
    _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .ToArray();
}
