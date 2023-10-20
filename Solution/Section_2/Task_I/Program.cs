using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;

namespace Section_2;

/*
Нужно написать класс MyQueueSized, который принимает параметр max_size, 
означающий максимально допустимое количество элементов в очереди.
Помогите ему — реализуйте программу, которая будет эмулировать работу такой очереди.
Функции, которые надо поддержать, описаны в формате ввода.
 */

public class Task_I
{
    public static void Main(string[] args)
    {
        var reader = new StreamReader(Console.OpenStandardInput());
        var writer = new StreamWriter(Console.OpenStandardOutput());
        var commandCount = short.Parse(reader.ReadLine());
        var maxSize = short.Parse(reader.ReadLine());
        var queue = new CircularBufferQueue(maxSize);

        for (short i = 0; i < commandCount; i++)
        {
            var currentStr = reader.ReadLine()?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);

            if (currentStr?.Length == 2)
            {
                var val = int.Parse(currentStr[1]);

                if (!queue.Push(val))
                {
                    writer.WriteLine("error");
                }

                continue;
            }

            switch (currentStr[0])
            {
                case "peek":
                    writer.WriteLine(queue.Peek());
                    break;

                case "size":
                    writer.WriteLine(queue.GetSize());
                    break;

                case "pop":
                    writer.WriteLine(queue.Pop());
                    break;
            }
        }

        reader.Close();
        writer.Close();
    }
}

public class CircularBufferQueue
{
    private int?[] _queue;
    private short _maxSize;
    private int _tail;
    private int _head;
    private int _size;

    public int GetSize() => _size;

    public CircularBufferQueue(short maxSize)
    {
        _maxSize = maxSize;
        _queue = new int?[maxSize];
    }

    public bool Push(int item)
    {
        if (_size != _maxSize)
        {
            _queue[_tail] = item;
            _tail = (_tail + 1) % _maxSize;
            _size++;
            return true;
        }

        return false;
    }

    public string? Pop()
    {
        if (_size == 0)
        {
            return "None";
        }

        var pop = _queue[_head];
        _queue[_head] = null; 
        _head = (_head + 1) % _maxSize;
        _size--;

        return pop.ToString();
    }

    public string? Peek()
    {
        if (_size == 0)
        {
            return "None";
        }

        return _queue[_head].ToString();
    }
}