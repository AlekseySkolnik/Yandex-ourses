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
// Золото лепреконов (Задача о рюкзаке. Динамическое решение)
namespace Task_L
{
    /*
    Лепреконы в данной задаче появились по соображениям общей морали, так как грабить банки — нехорошо.
    Вам удалось заключить неплохую сделку с лепреконами, поэтому они пустили вас в своё хранилище золотых слитков. 
    Все слитки имеют единую пробу, то есть стоимость 1 грамма золота в двух разных слитках одинакова.
    В хранилище есть n слитков, вес i-го слитка равен wi кг. 
    У вас есть рюкзак, вместимость которого M килограмм.
    Выясните максимальную суммарную массу золотых слитков, которую вы сможете унести.

    Формат вывода
    Выведите единственное число — максимальную массу, которую можно забрать с собой.
    */
    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var read = ReadList();
            var N = read[0]; // число слитков
            var M = read[1]; // вместимость рюкзака

            var weight = ReadList();
            var cost = new int[N];

            for (int i = 0; i < N; i++)
            {
                cost[i] = weight[i] * 1; // в данном случаем у всех 1 (у крупп было иначе)
            }

            var dp = new int[N + 1, M + 1];

            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= M; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else
                    {
                        //если вес текущей вещи больше размера рюкзака
                        if (weight[i - 1] > j)
                        {
                            // При этом если j−weight[i]≤0, то калорийность такого рюкзака должна приравниваться к нулю,
                            // так как формально мы получили рюкзак отрицательной вместимости, то есть ни один предмет не может поместиться в такой рюкзак.
                            dp[i, j] = dp[i - 1, j];
                        }
                        else
                        {
                            var prev = dp[i - 1, j];
                            // Сумма калорийности i - й крупы, которую мы кладём в рюкзак, и наилучшей калорийности рюкзака,
                            // размер которого соответствует оставшемуся свободному месту в нашем рюкзаке:
                            var next = cost[i - 1] + dp[i - 1, j - weight[i - 1]];
                            dp[i, j] = Math.Max(prev, next);
                        }
                    }
                }
            }

            Console.WriteLine(dp[N, M]);
        }

        private static int[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();

    }
}