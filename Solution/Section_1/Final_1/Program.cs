using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Section_1;

/*
Тимофей ищет место, чтобы построить себе дом. 
Улица, на которой он хочет жить, имеет длину n, то есть состоит из n одинаковых идущих подряд участков. 
Каждый участок либо пустой, либо на нём уже построен дом.
Общительный Тимофей не хочет жить далеко от других людей на этой улице. 
Поэтому ему важно для каждого участка знать расстояние до ближайшего пустого участка. 
Если участок пустой, эта величина будет равна нулю — расстояние до самого себя.
Помогите Тимофею посчитать искомые расстояния. Для этого у вас есть карта улицы. 
Дома в городе Тимофея нумеровались в том порядке, в котором строились, поэтому их номера на карте никак не упорядочены.
Пустые участки обозначены нулями.

Формат ввода
В первой строке дана длина улицы —– n (1 ≤ n ≤ 10^6).
В следующей строке записаны n целых неотрицательных чисел — номера домов и обозначения пустых участков на карте (нули). 
Гарантируется, что в последовательности есть хотя бы один ноль.
Номера домов (положительные числа) уникальны и не превосходят 10^9.

Формат вывода
Для каждого из участков выведите расстояние до ближайшего нуля. Числа выводите в одну строку, разделяя их пробелами. 
*/
// https://contest.yandex.ru/contest/22450/run-report/88555042/
public class Task_Final_1
{
    private static TextReader _reader;
    private static TextWriter _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        var length = ReadInt();
        var houses = ReadList();
        var zeroPositions = GetZeroPoints(houses);
        var distances = GetDistances(length, zeroPositions);

        var answer = string.Join(' ', distances);
        _writer.WriteLine(answer);

        _reader.Close();
        _writer.Close();
    }

    /// <summary>
    /// Подсчет расстояний
    /// </summary>
    /// <param name="length"></param>
    /// <param name="zeroPositions"></param>
    /// <returns></returns>
    private static int[] GetDistances(int length, List<int> zeroPositions)
    {
        var distances = new int[length];

        for (int i = 0; i <= zeroPositions[0]; i++)
        {
            distances[i] = zeroPositions[0] - i;
        }

        if (zeroPositions.Count > 1)
        {
            for (int k = 1; k < zeroPositions.Count; k++)
            {
                var left = zeroPositions[k - 1];
                var right = zeroPositions[k];
                var count = 1;

                while (left + count <= right - count)
                {
                    distances[left + count] = count;
                    distances[right - count] = count;
                    count++;
                }
            }
        }

        for (int i = zeroPositions[zeroPositions.Count - 1]; i < length; i++)
        {
            distances[i] = i - zeroPositions[zeroPositions.Count - 1];
        }

        return distances;
    }

    /// <summary>
    /// Вычисляет позиции нулей
    /// </summary>
    /// <param name="houses"></param>
    /// <returns>Массив позиций нулей с минимум одним элементом</returns>
    private static List<int> GetZeroPoints(List<ulong> houses)
    {
        var zeroPositions = new List<int>();

        for (var i = 0; i < houses.Count; i++)
        {
            if (houses[i] == 0)
            {
                zeroPositions.Add(i);
                continue;
            }
        }

        return zeroPositions;
    }

    private static int ReadInt()
    {
        return int.Parse(_reader.ReadLine());
    }

    private static List<ulong> ReadList() =>
        _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(ulong.Parse)
            .ToList();
}