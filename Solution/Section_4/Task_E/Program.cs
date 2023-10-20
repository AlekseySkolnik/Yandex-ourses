using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Подстроки
namespace Task_E;
/*
На вход подается строка.
Нужно определить длину наибольшей подстроки, которая не содержит повторяющиеся символы.
Формат ввода
Одна строка, состоящая из строчных латинских букв. Длина строки не превосходит 10 000.
Формат вывода
Выведите натуральное число —– ответ на задачу.
 */

internal class Program
{
    private static StreamReader _reader = new(Console.OpenStandardInput());
    private static string Str = string.Empty;

    static void Main(string[] args)
    {
        Str = _reader.ReadLine() ?? string.Empty;
        int max = ThirdVariant();

        Console.WriteLine(max);
    }

    private static int FirstVariant()
    {
        static int GetCurrentMax(int i)
        {
            var count = 0;
            var history = new HashSet<char>();

            while (i < Str.Length && !history.Contains(Str[i]))
            {
                history.Add(Str[i]);
                i++;
                count++;
            }

            return count;
        }

        var max = 0;

        for (int i = 0; i < Str.Length; i++)
        {
            max = Math.Max(max, GetCurrentMax(i));
        }

        return max;
    }

    // Оптимальнее
    private static int SecondVariant()
    {
        if (Str.Length == 0)
        {
            return 0;
        }

        var history = new Dictionary<char, int> { { Str[0], 0 } };
        var max = 1;
        var left = 0;
        var right = 1;

        while (right < Str.Length && left < Str.Length)
        {
            if (!history.ContainsKey(Str[right]))
            {
                history.Add(Str[right], right);
                right++;
                continue;
            }

            max = Math.Max(max, history.Count);
            left = history[Str[right]] + 1;
            history.Clear();
            history.Add(Str[left], left);
            right = left + 1;
        }

        max = Math.Max(max, history.Count);

        return max;
    }

    // Оптимальнее
    private static int ThirdVariant()
    {
        if (Str.Length == 0)
        {
            return 0;
        }

        var history = new HashSet<char> { Str[0] };
        var max = 1;
        var left = 0;
        var right = 1;

        while (right < Str.Length && left < Str.Length)
        {
            if (!history.Contains(Str[right]))
            {
                history.Add(Str[right]);
                right++;
                continue;
            }

            max = Math.Max(max, history.Count);

            do
            {
                if (history.Remove(Str[left]))
                {
                    left++;
                }
                else
                {
                    break;
                }
            } while (Str[left - 1] != Str[right]);
        }

        max = Math.Max(max, history.Count);

        return max;
    }
}
