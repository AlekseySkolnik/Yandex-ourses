using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Покупка домов
namespace Task_E;
/*
Тимофей решил купить несколько домов на знаменитом среди разработчиков Алгосском архипелаге.
Он нашёл n объявлений о продаже, где указана стоимость каждого дома в алгосских франках.
А у Тимофея есть k франков.
Помогите ему определить, какое наибольшее количество домов на Алгосах он сможет приобрести за эти деньги.

Формат ввода
В первой строке через пробел записаны натуральные числа n и k.
n — количество домов, которые рассматривает Тимофей, оно не превосходит 100000;
k — общий бюджет, не превосходит 100000;

В следующей строке через пробел записано n стоимостей домов. 
Каждое из чисел не превосходит 100000. 
Все стоимости — натуральные числа.

Формат вывода
Выведите одно число —– наибольшее количество домов, которое может купить Тимофей.
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var sts = ReadList();
        var houseCount = sts[0];
        var money = sts[1];
        var amounts = ReadList();

        Array.Sort(amounts);

        var count = 0;

        for (int i = 0; i < houseCount; i++)
        {
            money -= amounts[i];

            if (money >= 0)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        Console.WriteLine(count);
    }

    private static int[] ReadList() =>
        _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x.ToString()))
        .ToArray();
}
