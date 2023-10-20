using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Section_2;

/*
Реализуйте класс StackMaxEffective, поддерживающий операцию определения максимума среди элементов в стеке.
Сложность операции должна быть O(1).
Для пустого стека операция должна возвращать None. 
При этом push(x) и pop() также должны выполняться за константное время.
 * */

public class Task_G
{
    const string GetMax = "get_max";

    public static void Main(string[] args)
    {
        var reader = new StreamReader(Console.OpenStandardInput());
        var writer = new StreamWriter(Console.OpenStandardOutput());

        var commandCount = int.Parse(reader.ReadLine());
        var stack = new StackMaxEffectiveWithoutAllElements();

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

public class StackMaxEffectiveWithAllElements
{
    private readonly List<int> _items = new List<int>();
    private readonly List<int> _maxs = new List<int>();

    public string GetMax()
    {
        if (_maxs.Count == 0)
        {
            return "None";
        }

        return _maxs.Last().ToString();
    }

    public void Push(int item)
    {
        _items.Add(item);

        if (_maxs.Count == 0 || (_maxs.Count > 0 && item >= _maxs.Last()))
        {
            _maxs.Add(item);
        }
    }

    public string? Pop()
    {
        if (_items.Count == 0)
        {
            return "error";
        }

        var last = _items[_items.Count - 1];
        _items.RemoveAt(_items.Count - 1);

        if (_maxs.Count > 0 && last == _maxs.Last())
        {
            _maxs.RemoveAt(_maxs.Count - 1);
        }

        return null;
    }
}

public class StackMaxEffectiveWithoutAllElements
{
    private readonly List<int> _maxs = new List<int>();

    public string GetMax()
    {
        if (_maxs.Count == 0)
        {
            return "None";
        }

        return _maxs.Last().ToString();
    }

    public void Push(int item)
    {
        if (_maxs.Count == 0)
        {
            _maxs.Add(item);
            return;
        }

        var insertItem = item > _maxs.Last() ? item : _maxs.Last();
        _maxs.Add(insertItem);
    }

    public string? Pop()
    {
        if (_maxs.Count == 0)
        {
            return "error";
        }

        _maxs.RemoveAt(_maxs.Count - 1);
        return null;
    }
}
