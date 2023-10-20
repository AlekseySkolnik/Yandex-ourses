using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Поле с цветочками (Двумерная динамика)
namespace Task_H
{
    /*
    Черепаха Кондратина путешествует по клетчатому полю из n строк и m столбцов.
    В каждой клетке либо растёт цветочек, либо не растёт. 
    Кондратине надо добраться из левого нижнего в правый верхний угол
    и собрать как можно больше цветочков. Помогите ей с этой сложной задачей и определите,
    какое наибольшее число цветочков она сможет собрать при условии, 
    что Кондратина умеет передвигаться только на одну клетку вверх или на одну клетку вправо за ход.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var read = ReadList();
            var n = read[0]; // количество строк 280
            var m = read[1]; // количество столбцов 205

            var points = new int[n + 2, m + 2]; // 0.. n+1
            var dp = new long[n + 2, m + 2];

            ReadData(n, points);

            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < m + 1; j++)
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]) + points[i, j];
                }
            }

            Console.WriteLine(dp[n, m]);
        }

        private static void ReadData(int n, int[,] points)
        {
            for (var i = n; i > 0; i--)
            {
                var str = _reader?.ReadLine();
                var j = 1;

                foreach (var c in str)
                {
                    points[i, j] = c == '1' ? 1 : 0;
                    j++;
                }
            }
        }

        private static int[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();
    }
}