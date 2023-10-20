using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class D
{
    private static TextReader reader;
    private static TextWriter writer;

    // Если ответ существует, верните список из двух элементов
    // Если нет - то верните пустой список
    public static List<int> TwoSum(List<int> array, int targetSum)
    {
        for (int i = 0; i < array.Count; i++)
        {
            for (int j = i + 1; j < array.Count; j++)
            {
                if (array[i] + array[j] == targetSum)
                {
                    return new List<int>(2) { array[i], array[j] };
                }
            }
        }

        return new List<int>(0);
    }

    public static void MainD(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        _ = ReadInt();
        var array = ReadList();
        var targetSum = ReadInt();
        var result = TwoSum(array, targetSum);
        writer.WriteLine(!result.Any() ? "None" : string.Join(" ", result));

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static List<int> ReadList()
    {
        return reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}