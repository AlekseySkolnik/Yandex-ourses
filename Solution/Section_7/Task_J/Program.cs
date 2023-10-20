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
// Путешествие - Наибольшая возрастающая подпоследовательность (НВП)
namespace Task_J
{
    /*
    Гоша решил отправиться в турне по островам Алгосского архипелага.
    Туристическая программа состоит из последовательного посещения n достопримечательностей. 
    У i-й достопримечательности есть свой рейтинг ri.
    Впечатление от i-й достопримечательности равно её рейтингу ri. 
    Гоша хочет, чтобы его впечатление от каждой новой посещённой достопримечательности было сильнее, чем от предыдущей.
    Ради этого он даже готов пропустить некоторые места в маршруте – в случае, если они нарушают этот порядок плавного возрастания.
    Помогите Гоше и найдите наибольшую возрастающую подпоследовательность в массиве рейтингов ri.

    Формат вывода
    Сначала в отдельной строке выведите длину найденной подпоследовательности. 
    В следующей строке выведите номера достопримечательностей, которые образуют эту подпоследовательность.
    Ввод
    5
    4 2 9 1 13
    Вывод
    3
    1 3 5 (4->9->13))
     */
    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var N = int.Parse(_reader?.ReadLine()); // сколько различных туристических мест есть в программе.
            var ratings = ReadList(); // рейтинги этих достопримечательностей 
            var dp = new int[N + 1, N + 1];

            for (int i = 1; i <= N; i++)
            {
                var max = 0;

                for (int j = 1; j <= N; j++)
                {
                    dp[i, j] = dp[i - 1, j];

                    if (ratings[i - 1] == ratings[j - 1])
                    {
                        dp[i, j] = Math.Max(dp[i, j], max + 1);
                    }
                    else if (ratings[j - 1] < ratings[i - 1])
                    {
                        max = Math.Max(max, dp[i - 1, j]);
                    }
                }
            }

            // Ответ находится в последней строке таблицы, но необязательно в клетке dp[n][m].
            int max_el = 0;
            var iMax = N;
            var jMax = 0;

            for (int j = 0; j <= N; j++)
            {
                if (dp[N, j] > max_el)
                {
                    max_el = dp[N, j];
                    jMax = j;
                }
            }

            Console.WriteLine(max_el);

            var answer = new Stack<int>();
            answer.Push(jMax);
            max_el--;

            for (int i = N - 1; i > 0; i--)
            {
                for (int j = jMax; j >= 0; j--)
                {
                    if (i == j && dp[i, j] == max_el)
                    {
                        answer.Push(j);
                        jMax = j;
                        max_el--;
                        break;
                    }
                }

                if (max_el == 0)
                {
                    break;
                }
            }

            var sequence = "";

            while (answer.Count > 0)
            {
                sequence += $"{answer.Pop()} ";
            }


            Console.WriteLine(sequence);
        }

        private static int[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();

    }
}