using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Разные деревья поиска
// https://habr.com/ru/companies/yandex/articles/337690/
namespace Task_I;
/*
Ребятам стало интересно, сколько может быть различных деревьев поиска, 
содержащих в своих узлах все уникальные числа от 1 до n.
Помогите им найти ответ на этот вопрос.

Формат ввода
В единственной строке задано число n. Оно не превосходит 15.

Формат вывода
Нужно вывести число, равное количеству различных деревьев поиска,
в узлах которых могут быть размещены числа от 1 до n включительно.

 */

internal class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var n = int.Parse(_reader.ReadLine());
        var count = FirstVariant(n);
        Console.WriteLine(count);
    }

    // Динамическое программирование
    private static int FirstVariant(int n)
    {
        int[] G = new int[n + 1];
        G[0] = 1;
        G[1] = 1;

        for (int i = 2; i <= n; i++)
        {
           // Console.WriteLine($"i = {i}");

            for (int j = 1; j <= i; j++)
            {
                G[i] += G[j - 1] * G[i - j];
               // Console.WriteLine($"    j = {j}, G[{i}] += G[{j - 1}] * G[{i - j}], G[{i}] += {G[j - 1]} * {G[i - j]}");
            }

          //  Console.WriteLine($"G[{i}] = {G[i]}");
        }

        return G[n];
    }

    // https://ru.wikipedia.org/wiki/Числа_Каталана
    // (Правильные скобочные последовательности)
    // Двоичные деревья – деревья, из каждого узла которых (кроме листьев) выходит ровно две ветки.
    // Количество бинарных деревьев с заданным числом листьев – число Каталана

    private static int SecondVariant(int n)
    {
        // (2n)! / (n! * (n+1)!)
        long C = 1;

        for (int i = 0; i < n; ++i)
        {
            C = C * 2 * (2 * i + 1) / (i + 2);
        }

        return (int)C;
    }
}
