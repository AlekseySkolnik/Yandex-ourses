using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Гардероб
namespace Task_G;
/*
Рита решила оставить у себя одежду только трёх цветов: розового, жёлтого и малинового. 
После того как вещи других расцветок были убраны, Рита захотела отсортировать свой новый гардероб по цветам.
Сначала должны идти вещи розового цвета, потом —– жёлтого, и в конце —– малинового.
Помогите Рите справиться с этой задачей.

Примечание: попробуйте решить задачу за один проход по массиву!

Формат ввода
В первой строке задано количество предметов в гардеробе: n –— оно не превосходит 1000000.
Во второй строке даётся массив, в котором указан цвет для каждого предмета.
Розовый цвет обозначен 0, жёлтый —– 1, малиновый –— 2.

Формат вывода
Нужно вывести в строку через пробел цвета предметов в правильном порядке.
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var count = int.Parse(_reader.ReadLine());
        var colors = ReadList();

        var countColor = new int[3];

        foreach (var color in colors)
        {
            if (color == 0)
                countColor[0]++;

            if (color == 1)
                countColor[1]++;

            if (color == 2)
                countColor[2]++;
        }

        var outputArray = new int[count];

        var length = 0;

        for (int i = 0; i < length + countColor[0]; i++)
        {
            outputArray[i] = 0;
        }

        length += countColor[0];

        for (int i = length; i < length + countColor[1]; i++)
        {
            outputArray[i] = 1;
        }

        length += countColor[1];

        for (int i = length; i < length + countColor[2]; i++)
        {
            outputArray[i] = 2;
        }

        Console.WriteLine($"{string.Join(' ', outputArray)}");
    }


    private static List<byte> ReadList() =>
        _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(byte.Parse)
        .ToList();
}
