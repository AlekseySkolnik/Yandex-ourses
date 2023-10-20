using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Прыжки по лестнице
namespace Task_F
{
    /*
    Алла хочет доказать, что она умеет прыгать вверх по лестнице быстрее всех. 
    На этот раз соревнования будут проходить на специальной прыгательной лестнице.
    С каждой её ступеньки можно прыгнуть вверх на любое расстояние от 1 до k. 
    Число k придумывает Алла.
    Гоша не хочет проиграть, поэтому просит вас посчитать количество способов 
    допрыгать от первой ступеньки до n-й. Изначально все стоят на первой ступеньке.

    Формат ввода
    В единственной строке даны два числа — n и k (1 ≤ n ≤ 1000, 1 ≤ k ≤ n).

    Формат вывода
    Выведите количество способов по модулю 109 + 7.
     */

    // TODO: не укоаывается по времени                   
    // Решение тоже неоптимально, как минимум потому что можно обойтись одномерной динамикой, подумай)
    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static int Mod = 1000000007;
        static void Main(string[] args)
        {
            var read = ReadList();
            var n = read[0]; // количество ступенек
            var maxK = read[1]; // шаг

            var dpK = new int[maxK + 1][];

            for (var i = 0; i <= maxK; i++)
            {
                dpK[i] = new int[n + 1];
            }

            // dpK[1][1] = 1; dpK[1][2] = 1; dpK[1][3] = 1; dpK[1][4] = 1; dpK[1][5] = 1; dpK[1][6] = 1;
            // dpK[2][1] = 1; dpK[2][2] = 1; dpK[2][3] = 2; dpK[2][4] = 3; dpK[2][5] = 5; dpK[2][6] = 8; Сумма двух предыдущих
            // dpK[3][1] = 1; dpK[3][2] = 1; dpK[3][3] = 2; dpK[3][4] = 4; dpK[3][5] = 7; dpK[3][6] = 13; Сумма трех предыдущих
            // dpK[4][1] = 1; dpK[4][2] = 1; dpK[4][3] = 2; dpK[4][4] = 4; dpK[4][5] = 8; dpK[4][6] = 15; Сумма четырех предыдущих
            // dpK[k][j] =  dpK[k][j - 1] +  dpK[k][j - 2] +  dpK[k][j - 3]....  dpK[k][j - k]

            // k = 2
            //for (int j = 4; j <= n; j++)
            //{
            // dpK[2][3] = dpK[2][2] + dpK[2][1];
            // dpK[2][4] = dpK[2][3] + dpK[2][2];
            // dpK[2][j] = dpK[2][j - 1] + dpK[2][j - 2];
            //    var count = 1;
            //    while (count <= 2)
            //    {
            //        dpK[2][j] += dpK[2][j - count];
            //        count++;
            //    }
            //}

            for (int j = 1; j < n + 1; j++)
            {
                dpK[1][j] = 1;
            }

            for (int k = 2; k <= maxK; k++)
            {
                dpK[k][1] = 1;
                dpK[k][2] = 1;

                for (int j = 3; j <= n; j++)
                {
                    var count = 1;

                    while (count <= k)
                    {
                        if (j - count >= 0)
                        {
                            var add = dpK[k][j - count] % Mod;
                            dpK[k][j] = (dpK[k][j] + add) % Mod;
                        }

                        count++;
                    }
                }
            }

            Console.WriteLine(dpK[maxK][n] % Mod);
        }

        private static long[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray() ?? Array.Empty<long>();
    }
}