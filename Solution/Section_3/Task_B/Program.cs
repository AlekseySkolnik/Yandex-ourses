using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Комбинации
namespace Task_B;
/*
 На клавиатуре старых мобильных телефонов каждой цифре соответствовало несколько букв. 
Примерно так:
2:'abc',
3:'def',
4:'ghi',
5:'jkl',
6:'mno',
7:'pqrs',
8:'tuv',
9:'wxyz'

Вам известно в каком порядке были нажаты кнопки телефона, без учета повторов.
Напечатайте все комбинации букв, которые можно набрать такой последовательностью нажатий.

Формат ввода
На вход подается строка, состоящая из цифр 2-9 включительно. 
Длина строки не превосходит 10 символов.

Формат вывода
Выведите все возможные комбинации букв через пробел.
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var numbers = ReadList();
        var count = numbers.Count;

        var seconds = GetLetters(numbers[count - 1]).Select(x => x.ToString()).ToList();

        while (count > 1)
        {
            var firsts = GetLetters(numbers[count - 2]);
            seconds = GenCombinations(firsts, seconds);
            count--;
        }

        Console.WriteLine($"{string.Join(' ', seconds)}");
    }

    private static List<string> GenCombinations(string firsts, List<string> seconds)
    {
        var list = new List<string>();

        foreach (var first in firsts)
        {
            foreach (var s in seconds)
            {
                list.Add(first + s);
            }
        }

        return list;
    }

    private static string GetLetters(byte number) =>
    number switch
    {
        2 => "abc",
        3 => "def",
        4 => "ghi",
        5 => "jkl",
        6 => "mno",
        7 => "pqrs",
        8 => "tuv",
        9 => "wxyz",
        _ => string.Empty,
    };

    private static List<byte> ReadList() =>
        _reader.ReadLine()
        .ToCharArray()
        .Select(x => byte.Parse(x.ToString()))
        .ToList();
}
