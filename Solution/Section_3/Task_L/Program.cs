using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
// Два велосипеда
namespace Section_3;

/*
 
Вася решил накопить денег на два одинаковых велосипеда — себе и сестре.
У Васи есть копилка, в которую каждый день он может добавлять деньги (если, конечно, у него есть такая финансовая возможность). 
В процессе накопления Вася не вынимает деньги из копилки.
У вас есть информация о росте Васиных накоплений — сколько у Васи в копилке было денег в каждый из дней.
Ваша задача — по заданной стоимости велосипеда определить первый день, в которой Вася смог бы купить один велосипед,
и первый день, в который Вася смог бы купить два велосипеда.
Подсказка: решение должно работать за O(log n).

Формат ввода
В первой строке дано число дней n, по которым велись наблюдения за Васиными накоплениями. 1 ≤ n ≤ 106.
В следующей строке записаны n целых неотрицательных чисел. Числа идут в порядке неубывания. Каждое из чисел не превосходит 106.
В третьей строке записано целое положительное число s — стоимость велосипеда. Это число не превосходит 106.

Формат вывода
Нужно вывести два числа — номера дней по условию задачи.

Если необходимой суммы в копилке не нашлось, нужно вернуть -1 вместо номера дня.
 */

public class Task_L
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    public static void Main(string[] args)
    {
        var daysCount = uint.Parse(_reader.ReadLine());
        var moneyByDays = ReadList();
        var price = uint.Parse(_reader.ReadLine());

        var firstDay = BinarySearchDay(moneyByDays, price, 0, (int)daysCount);
        var secondDay = BinarySearchDay(moneyByDays, 2 * price, firstDay, (int)daysCount);
        Console.WriteLine($"{firstDay + 1} {secondDay + 1}");
    }

    private static int BinarySearchDay(List<uint> moneyByDays, uint price, int left, int right)
    {
        if (right <= left)
            return -2;

        var mid = (right + left) / 2;

        if (moneyByDays[mid] >= price && (mid == 0 || moneyByDays[mid - 1] < price))
        {
            return mid;
        }
        else if (moneyByDays[mid] >= price)
        {
            return BinarySearchDay(moneyByDays, price, left, mid);
        }
        else
        {
            return BinarySearchDay(moneyByDays, price, mid + 1, right);
        }
    }

    private static List<uint> ReadList() =>
    _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(uint.Parse)
        .ToList();
}