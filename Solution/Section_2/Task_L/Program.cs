using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Section_2;

// https://www.codeproject.com/Tips/837822/Large-Integer-Fibonacci-Numbers
// https://habr.com/ru/articles/83303/

public class Task_L
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
    public static void Main(string[] args)
    {
        var numbers = ReadList();
        var n = numbers[0];
        var k = (byte)numbers[1];

        var mas = new BigInteger[n + 1];
        mas[0] = mas[1] = 1;

        for (int i = 2; i < n + 1; i++)
        {
            mas[i] = mas[i - 2] + mas[i - 1];
        }

        BigInteger delitel = 10;

        for (int i = 0; i < k; i++)
        {
            delitel *= 10;
        }

        var f_n = mas[n];
        var count = f_n % delitel;

        Console.WriteLine(count);
    }


    private static List<int> ReadList() =>
    _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
}
