using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Section_3;

/*
Задание
Алла ошиблась при копировании из одной структуры данных в другую.
Она хранила массив чисел в кольцевом буфере.
Массив был отсортирован по возрастанию, и в нём можно было найти элемент за логарифмическое время.
Алла скопировала данные из кольцевого буфера в обычный массив, 
но сдвинула данные исходной отсортированной последовательности.
Теперь массив не является отсортированным.
Тем не менее, нужно обеспечить возможность находить в нем элемент за O(logn).
Можно предполагать, что в массиве только уникальные элементы.
От вас требуется реализовать функцию, осуществляющую поиск в сломанном массиве.

Формат ввода
Функция принимает массив натуральных чисел и искомое число k. 
Длина массива не превосходит 10000. 
Элементы массива и число k не превосходят по значению 10000. 
В примерах:
В первой строке записано число n –— длина массива.
Во второй строке записано положительное число k –— искомый элемент. 
Далее в строку через пробел записано n натуральных чисел – элементы массива.

Формат вывода
Функция должна вернуть индекс элемента, равного k, если такой есть в массиве (нумерация с нуля). 
Если элемент не найден, функция должна вернуть −1.
Изменять массив нельзя.
Для отсечения неэффективных решений ваша функция будет запускаться от 100000 до 1000000 раз.
 */

/*
-- ПРИНЦИП РАБОТЫ --
Алгоритм представляет собой модернизированный бинарный поиск, т.к. изначально массив
отсортирован не верно. Находим середину массива mid, при этом либо левая половина, либо правая отсортирована корректно.
Если левая половина отсортирована корректно (array[left] < array[mid]) 
и элемент находится внутри нее (array[left] < el < array[mid]), то продолжаем его искать внутри этого отрезка,
используя обычный бинарный поиск, т.к. отрезок отсортирован.
Если элемента в нем нет, значит нужно искать в правой части.
Если правая половина отсортирована корректно (array[mid] < array[right])  
и элемент находится внутри нее (array[mid] < el < array[right]), то продолжаем его искать внутри этого отрезка,
используя обычный бинарный поиск, т.к. отрезок отсортирован.
Если элемента в нем нет, значит нужно искать в левой части.

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Массив представляет собой по сути 2 отсортированных отрезка. 
Есть только один элемент внутри массива, для которого верно array[i] < array [i-1].
Если его найти, то было бы сразу понятно в какой половине искать требуемый элемент бинарным поиском.
Однако быстро (за logN) найти его не получится, только линейным поиском.
Т.к. мы знаем о сдвижке массива, то можно уверенно сказать, 
что изначально самый левый элемент больше самого правого array[left] > array[right].
Поэтому смотрим на середину всего массива mid. 
Точно выполняется одно из условий:
или отрезок [left, mid) или отрезок [mid, right) отсортирован верно.
Мы можем сразу понять, есть ли там искомый элемент, проверив:
для [left, mid)  -> array[left] < el < array[mid]
для [mid, right) -> array[mid] < el < array[right]
Если какое-то из условий выполняется, значит элемент находится внутри этого отрезка и в нем проводим бинарный поиск.
Если элемента нет, то искать надо в другом отрезке. Сдвигаем границу отрезка (или right = mid - 1 или left = mid + 1).
Для другого отрезка снова действует правило как для изначального массива, левая или правая половина отсортирована верно.
Рекурсивно ищем элемент.
    
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Т.к. для поиска элемента используется модернизированный бинарный поиск,
то временная сложность log(N+c), c = const (несколько добавочных действий)

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
В процессе поиска мы не изменяем массив, не копируем его. 
Выделяется память всего под несколько переменных и под стек рекурсии.
Сложность log(N)
 */

/// <summary>
/// ID успешной посылки: https://contest.yandex.ru/contest/23815/run-report/89019808/
/// </summary>
public class Solution
{
    private const int None = -1;

    public static int BrokenSearch(List<int> array, int el)
    {
        int left = 0, right = array.Count - 1;

        if (array.Count == 0)
        {
            return None;
        }

        var index = ModernBinarySearch(array, el, left, right);

        return index;
    }

    /// <summary>
    /// Модернизированный бинарный поиск
    /// </summary>
    private static int ModernBinarySearch(List<int> array, int el, int left, int right)
    {
        var indexResult = СheckExtremePoints(array, el, left, right);

        if (indexResult != None)
        {
            return indexResult;
        }

        if (left >= right)
        {
            return None; // базовый случай рекурсии
        }

        GetNextLeftAndRight(array, el, ref left, ref right);
        indexResult = ModernBinarySearch(array, el, left, right);

        return indexResult;
    }

    /// <summary>
    /// Вычисляет отрезок, в котором находится искомый элемент
    /// </summary>
    private static void GetNextLeftAndRight(List<int> array, int el, ref int left, ref int right)
    {
        var mid = (right + left) / 2;

        if (array[left] < array[right]) // Подотрезок отсортирован правильно (можно применить обычный бинарный поиск)
        {
            if (el < array[mid])
            {
                right = mid - 1;  // искомый элемент левее среднего, сдвигаем границу справа
            }
            else
            {
                left = mid + 1; // искомый элемент правее среднего, сдвигаем границу слева
            }
        }
        else
        {
            if (array[left] < array[mid]) // левая половинка в порядке
            {
                if (el > array[left] && el < array[mid]) // элемент внутри нее
                {
                    right = mid - 1;
                    return;
                }

                left = mid + 1; // элемент в правой воловине
            }

            if (array[mid] < array[right]) // правая половинка в порядке
            {
                if (el > array[mid] && el < array[right]) // элемент внутри нее
                {
                    left = mid + 1;
                    return;
                }

                right = mid - 1; // элемент в левой половине
            }
        }
    }

    /// <summary>
    /// Проверяет равен ли элемент крайним точкам или середине, если да,
    /// то сразу возвращаем его индекс. Иначе отдаем None.
    /// </summary>
    private static int СheckExtremePoints(List<int> array, int el, int left, int right)
    {
        var mid = (right + left) / 2;

        if (array[mid] == el)
        {
            return mid;
        }

        if (array[left] == el)
        {
            return left;
        }

        if (array[right] == el)
        {
            return right;
        }

        return None;
    }
}

public class Final_A
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    public static void Main(string[] args)
    {
        var arr = ReadList();
        var el = int.Parse(_reader.ReadLine());
        var index = Solution.BrokenSearch(arr, el);
        Console.WriteLine(index);
    }

    private static List<int> ReadList() =>
        _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
}