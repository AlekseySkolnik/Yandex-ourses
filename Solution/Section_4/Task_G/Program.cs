using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using static Task_G.Program;
// Соревнование
namespace Task_G;
/*
Жители Алгосов любят устраивать турниры по спортивному программированию. 
Все участники разбиваются на пары и соревнуются друг с другом.
А потом два самых сильных программиста встречаются в финальной схватке, которая состоит из нескольких раундов. 
Если в очередном раунде выигрывает первый участник, в таблицу с результатами записывается 0, если второй, то 1.
Ничьей в раунде быть не может.

Нужно определить наибольший по длине непрерывный отрезок раундов, 
по результатам которого суммарно получается ничья.
Например, если дана последовательность 0 0 1 0 1 1 1 0 0 0,
то раунды с 2-го по 9-й (нумерация начинается с единицы) дают ничью.

Формат ввода
В первой строке задаётся n (0 ≤ n ≤ 105) –— количество раундов.
Во второй строке через пробел записано n чисел –— результаты раундов. Каждое число равно либо 0, либо 1.

Формат вывода
Выведите длину найденного отрезка.
 */

/*
 Здесь всё основано на идее префиксных сумм:
если 0 в исходном массиве заменить на -1, то можно сказать, что требуется, 
чтобы сумма на исходном подотрезке была равна 0.
А это значит, что нужно, чтобы префиксные суммы были равны, 
и надо найти две одинаковые префиксные суммы с наибольшим расстоянием между ними.

Со словарём верная идея, но только в нём лучше хранить индексы, 
когда такая сумма встретилась в первый и последний раз.
Тогда сумма, соответствующая наибольшей разнице, и будет ответом.
 */


internal class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var count = int.Parse(_reader.ReadLine());
        var rounds = _reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x == "1" ? 1 : -1)
                .ToList();

        if (count < 2)
        {
            Console.WriteLine(0);
        }
        else
        {
            FindMaxSequence(rounds);
        }
    }

    private static void FindMaxSequence(List<int> rounds)
    {
        var sum = new List<int>(rounds.Count + 2) { -1 }; // добавим 0 слева
        var dict = new Dictionary<int, Sum>
        {
            { sum[0], new Sum { Max = 0, Min = 0 } }
        };

        for (int i = 1; i <= rounds.Count; i++)
        {
            sum.Add(sum[i - 1] + rounds[i - 1]);

            if (!dict.ContainsKey(sum[i]))
            {
                dict.Add(sum[i], new Sum { Max = i, Min = i });
                continue;
            }

            var t = dict[sum[i]];
            t.Max = i;
        }

        sum.Add(sum[rounds.Count] + 1); // добавим 1 справа и проверим его в словаре

        if (!dict.ContainsKey(sum[rounds.Count]))
        {
            dict.Add(sum[rounds.Count], new Sum { Max = rounds.Count, Min = rounds.Count });
        }
        else
        {
            var t = dict[sum[rounds.Count]];
            t.Max = rounds.Count;
        }

        var count = dict.Values.Max(x => x.Dif);
        Console.WriteLine(count);
    }

    public class Sum
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Dif => Max - Min;
    }
}
