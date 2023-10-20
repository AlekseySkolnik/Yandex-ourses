using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Перевести список ребер в матрицу смежности
namespace Task_B
{
    /*
    Алла успешно справилась с предыдущим заданием, и теперь ей дали новое. 
    На этот раз список рёбер ориентированного графа надо переводить в матрицу смежности. 
    Конечно же, Алла попросила вас помочь написать программу для этого.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var list = ReadList();
            var n = list[0];
            var m = list[1];
            var matrix = new int[n, n];

            for (int j = 0; j < m; j++)
            {
                var edges = ReadList();
                matrix[edges[0] - 1, edges[1] - 1] = 1;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        private static List<int> ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList() ?? new List<int>();
    }
}