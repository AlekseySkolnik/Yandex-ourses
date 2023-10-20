using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Section_2;

/*
 * Задание
Гоша реализовал структуру данных Дек, максимальный размер которого определяется заданным числом.
Методы push_back(x), push_front(x), pop_back(), pop_front() работали корректно.
Но, если в деке было много элементов, программа работала очень долго. 
Дело в том, что не все операции выполнялись за O(1).
Помогите Гоше! Напишите эффективную реализацию.
Внимание: при реализации используйте кольцевой буфер.

Формат ввода
В первой строке записано количество команд n — целое число, не превосходящее 100000.
Во второй строке записано число m — максимальный размер дека.
Он не превосходит 50000. 
В следующих n строках записана одна из команд:
push_back(value) – добавить элемент в конец дека. 
Если в деке уже находится максимальное число элементов, вывести «error».
push_front(value) – добавить элемент в начало дека.
Если в деке уже находится максимальное число элементов, вывести «error».
pop_front() – вывести первый элемент дека и удалить его.
Если дек был пуст, то вывести «error».
pop_back() – вывести последний элемент дека и удалить его. 
Если дек был пуст, то вывести «error».
Value — целое число, по модулю не превосходящее 1000.
Формат вывода
Выведите результат выполнения каждой команды на отдельной строке. 
Для успешных запросов push_back(x) и push_front(x) ничего выводить не надо.
 */

/*
-- ПРИНЦИП РАБОТЫ --

Данная реализация дека является в каком-то роде комбинацией метода двух указателей
и ограниченной очереди на кольцевом буфере.
Создадим массив, который будет хранить элементы дека. 
Зададим метке начала (head) и метке конца (tail) начальные значение, равыне 0, т.е. ставим их рядом.

PushBack  вставляет   элемент в  массив  слева  направо, при этом метка tail инкрементируется, а head стоит на месте.
PushFront вставляет   элемент в  массив  справа налево,  при этом метка head декрементируется, а tail стоит на месте.
PopFront  вытаскивает элемент из массива справа налево,  при этом метка head инкреметируется,  а tail стоит на месте.
PopBack   вытаскивает элемент из массива справа налево,  при этом метка tail декрементируется, a head стоит на месте.

Т.к. у нас кольцевой буфер, то при движении влево после нулевого элемента следует элемент (_maxSize - 1).
Указатели встречаются и становятся равны, когда дек становится пустой.

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

Из описания видно, что за вставкой справа следит метка tail, за вставкой слева метка head. Наоборот,
за вытаскиванием элементов справа следит метка head, за вытаскиванием слева метка tail. В паре они
обеспечивают правильные запис и чтение. При любом из 4-х действий, выполненных _maxSize раз, массив либо полностью заполнится,
либо полностью очистится, а указатели встретятся и станут равны.
    
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Вставка и удаление элемента из массива осуществляется по индексу за O(1).

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Для хранения в деке N элементов мы выделяем 4 ячейки памяти под указатели и размеры 
и массив, который занимает O(n) памяти.
Поэтому потребление памяти будет O(n).

 */

/// <summary>
/// ID успешной посылки: https://contest.yandex.ru/contest/22781/run-report/88789170/
/// </summary>
public class Final_A
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    public static void Main(string[] args)
    {
        var commandCount = ReadInt(); // целое число, не превосходящее 100000
        var maxDeqSize = ReadInt(); // не превосходит 50000
        var queue = new CircularBufferDeq((ushort)maxDeqSize);

        for (int i = 0; i < commandCount; i++)
        {
            var command = ReadList();

            switch (command[0])
            {
                case "pop_back":
                    Console.WriteLine(queue.PopBack());
                    break;

                case "pop_front":
                    Console.WriteLine(queue.PopFront());
                    break;

                case "push_front":

                    if (!queue.PushFront(int.Parse(command[1])))
                    {
                        Console.WriteLine("error");
                    }

                    break;

                case "push_back":

                    if (!queue.PushBack(int.Parse(command[1])))
                    {
                        Console.WriteLine("error");
                    }

                    break;
            }
        }
    }

    private static List<string> ReadList() =>
        _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .ToList();

    private static int ReadInt()
    {
        return int.Parse(_reader.ReadLine());
    }

    /// <summary>
    /// Структура данных DEQ, реализованная на кольцевом буфере (по заданию)
    /// </summary>
    public class CircularBufferDeq
    {
        private const string Error = "error";
        private readonly int?[] _queue;
        private ushort _maxSize;
        private int _tail;
        private int _head;
        private int _deqSize;

        private bool IsDeqEmpty => _deqSize == 0;
        private bool IsDeqFull => _deqSize == _maxSize;

        public CircularBufferDeq(ushort maxSize)
        {
            _maxSize = maxSize;
            _queue = new int?[_maxSize];
        }

        /// <summary>
        /// Добавить элемент в конец дека. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool PushBack(int item)
        {
            if (IsDeqFull)
            {
                return false;
            }

            _queue[_tail] = item;
            _tail = (_tail + 1) % _maxSize;
            _deqSize++;

            return true;
        }

        /// <summary>
        /// Добавить элемент в начало дека
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool PushFront(int item)
        {
            if (IsDeqFull)
            {
                return false;
            }

            if (_head == 0)
            {
                _head = _maxSize;
            }

            _queue[_head - 1] = item;
            _head = (_head - 1) % _maxSize;
            _deqSize++;

            return true;
        }

        /// <summary>
        /// Вывести первый элемент дека и удалить его
        /// </summary>
        /// <returns></returns>
        public string? PopFront()
        {
            if (IsDeqEmpty)
            {
                return Error;
            }

            var popElement = _queue[_head];

            _queue[_head] = null;
            _head = (_head + 1) % _maxSize;
            _deqSize--;

            return popElement.ToString();
        }

        /// <summary>
        /// Вывести последний элемент дека и удалить его
        /// </summary>
        /// <returns></returns>
        public string? PopBack()
        {
            if (IsDeqEmpty)
            {
                return Error;
            }

            if (_tail == 0)
            {
                _tail = _maxSize;
            }

            var popElement = _queue[_tail - 1];

            _queue[_tail - 1] = null;
            _tail = (_tail - 1) % _maxSize;
            _deqSize--;

            return popElement.ToString();
        }
    }
}