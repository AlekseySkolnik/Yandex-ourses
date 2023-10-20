using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Section_2;

/*
 * Задание
Формат ввода:
В единственной строке дано выражение, записанное в обратной польской нотации. 
Числа и арифметические операции записаны через пробел.
На вход могут подаваться операции: +, -, *, / и числа, по модулю не превосходящие 10000.
Гарантируется, что значение промежуточных выражений в тестовых данных по модулю не больше 50000.
Формат вывода:
Выведите единственное число — значение выражения.
 */

/*
-- ПРИНЦИП РАБОТЫ --

Рассмотрим алгоритм более подробно. Для его реализации будем использовать стек.
Для вычисления значения выражения, записанного в обратной польской нотации,
нужно считывать выражение слева направо и придерживаться следующих шагов:

1. Обработка входного символа:
- Если на вход подан операнд, он помещается на вершину стека.
- Если на вход подан знак операции, то эта операция выполняется над требуемым количеством значений,
    взятых из стека в порядке добавления. Результат выполненной операции помещается на вершину стека.
2. Если входной набор символов обработан не полностью, перейти к шагу 1.
3. После полной обработки входного набора символов результат вычисления выражения находится в вершине стека.
Если в стеке осталось несколько чисел, то надо вывести только верхний элемент.

В текущей задаче гарантируется, что деления на отрицательное число нет.

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Не понял, что тут надо доказать, когда алгоритм был подробно расписан и он доказан другими)
    
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Для N введённых чисел выполнится N-1 операций. Одна операция включает:
- 2 вставки в стек, 
- 2 извлечения из стека,
- 1 операция сравнения
- 1 арифметическая операция
- 1 вставка в стек результата
Т.е. одна операция сложностью 7*O(1) = O(1).
В целом сложность O(n) ?!

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Стек постоянно заполняется и частично очищается. Пространственная сложность точно O(n).
В худшем случае в стек надо вставить N/2+1 чисел
 */

/// <summary>
/// ID успешной посылки: https://contest.yandex.ru/contest/22781/run-report/88789384/
/// </summary>
public class Final_B
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    public static void Main(string[] args)
    {
        var inputs = ReadList();
        var operands = new CustomStack(inputs.Count / 2 + 1);

        for (int i = 0; i < inputs.Count; i++)
        {
            if (int.TryParse(inputs[i], out var digit))
            {
                operands.Push(digit);
                continue;
            }

            var result = PerformOperation(inputs[i], operands);

            if (result != null)
            {
                operands.Push(result.Value);
            }
        }

        Console.WriteLine(operands.Pop());
    }

    private static int? PerformOperation(string operation, CustomStack operands)
    {
        var second = operands.Pop();
        var first = operands.Pop();
        int? result = null;

        switch (operation)
        {
            case "+": result = first + second; break;
            case "-": result = first - second; break;
            case "*": result = first * second; break;
            case "/":
                if (first >= 0)
                {
                    result = first / second;
                    break;
                }
                var del = first / second;
                // равносильно result = first != second * del ? del - 1 : del;
                result = first % second != 0 ? del - 1 : del;
                break;
        }

        return result;
    }

    private static List<string> ReadList() =>
        _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .ToList();

    public class CustomStack
    {
        private const string EmptyStack = "EmptyStack";
        private readonly int?[] _items;
        private int _count;

        public CustomStack(int capacity)
        {
            _items = new int?[capacity];
        }

        public void Push(int item)
        {
            _items[_count] = item;
            _count++;
        }

        public int Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException(EmptyStack);
            }

            var current = _items[_count - 1];
            _items[_count - 1] = null;
            _count--;
            return current.Value;
        }
    }
}