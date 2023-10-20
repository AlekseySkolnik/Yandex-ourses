using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Section_2;

/*
Нужно реализовать класс StackMax, который поддерживает операцию определения максимума среди всех элементов в стеке.
Класс должен поддерживать операции push(x), где x – целое число, pop() и get_max().
 * */

public class Task_F
{
    const string GetMax = "get_max";

    public static void Main(string[] args)
    {
        var reader = new StreamReader(Console.OpenStandardInput());
        var writer = new StreamWriter(Console.OpenStandardOutput());

        var commandCount = int.Parse(reader.ReadLine());
        var stack = new StackMax();

        for (int i = 0; i < commandCount; i++)
        {
            var currentStr = reader.ReadLine()?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);

            if (currentStr?.Length == 2)
            {
                var val = int.Parse(currentStr[1]);
                stack.Push(val);
            }
            else if (currentStr?.Length == 1)
            {
                var message = currentStr[0] == GetMax ? stack.GetMax() : stack.Pop();

                if (!string.IsNullOrEmpty(message))
                {
                    writer.WriteLine(message);
                }
            }
        }

        reader.Close();
        writer.Close();
    }
}

public class StackMax
{
    private readonly List<int> _items = new List<int>();

    public string GetMax()
    {
        if (_items.Count == 0)
        {
            return "None";
        }

        return _items.Max().ToString();
    }

    public void Push(int item)
    {
        _items.Add(item);
    }

    public string? Pop()
    {
        if (_items.Count == 0)
        {
            return "error";
        }

        _ = _items[_items.Count - 1];
        _items.RemoveAt(_items.Count - 1);

        return null;
    }
}