using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;

namespace Section_2;

/*
Любимый вариант очереди Тимофея — очередь, написанная с использованием связного списка.
Помогите ему с реализацией. Очередь должна поддерживать выполнение трёх команд:
get() — вывести элемент, находящийся в голове очереди, и удалить его. Если очередь пуста, то вывести «error».
put(x) — добавить число x в очередь
size() — вывести текущий размер очереди
Формат ввода
В первой строке записано количество команд n — целое число, не превосходящее 1000.
В каждой из следующих n строк записаны команды по одной строке.
Формат вывода
Выведите ответ на каждый запрос по одному в строке.
 */

public class Task_J
{
    public static void Main(string[] args)
    {
        var reader = new StreamReader(Console.OpenStandardInput());
        var writer = new StreamWriter(Console.OpenStandardOutput());
        var commandCount = short.Parse(reader.ReadLine());
        var queue = new LinkedListQueue();

        for (short i = 0; i < commandCount; i++)
        {
            var currentStr = reader.ReadLine()?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);

            if (currentStr?.Length == 2)
            {
                var val = int.Parse(currentStr[1]);
                queue.Push(val);
                continue;
            }

            switch (currentStr[0])
            {
                case "size":
                    writer.WriteLine(queue.GetSize());
                    break;

                case "get":
                    writer.WriteLine(queue.Pop());
                    break;
            }
        }

        reader.Close();
        writer.Close();
    }
}

public class LinkedListQueue
{
    private Node<int>? _tail;
    private Node<int>? _head;
    private int _size;

    public int GetSize() => _size;

    public LinkedListQueue()
    {
        _size = 0;
    }

    public void Push(int item)
    {
        if (_size == 0)
        {
            _head = new Node<int>(item, null);
            _tail = _head;
        }
        else if (_size == 1)
        {
            _tail = new Node<int>(item, null);
            _head.Prev = _tail;
        }
        else
        {
            _tail.Prev = new Node<int>(item, null);
            _tail = _tail.Prev;
        }

        _size++;
    }

    public string? Pop()
    {
        if (_size == 0 || _head == null)
        {
            return "error";
        }

        var val = _head.Value;
        _head = _head.Prev;
        _size--;

        return val.ToString();
    }
}

public class Node<TValue>
{
    public TValue? Value { get; private set; }
    public Node<TValue>? Prev { get; set; }
    public Node(TValue? value, Node<TValue>? prev)
    {
        Value = value;
        Prev = prev;
    }
}
