using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Section_1;

//  Помогите Васе написать код функции, вычисляющей y = ax2 + bx + c. Напишите программу, которая будет по коэффициентам a, b, c и числу x выводить значение функции в точке x.
public class Task_A
{
    private static TextReader? _reader;
    private static TextWriter? _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        var sortedArray = ReadList();
        var a = sortedArray[0];
        var x = sortedArray[1];
        var b = sortedArray[2];
        var c = sortedArray[3];

        var f = a * x * x + b * x + c;

        System.Diagnostics.Debug.Write(f);
        _writer.WriteLine($"{f}");

        _reader.Close();
        _writer.Close();
    }

    private static List<int> ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList() ?? new List<int>();
}