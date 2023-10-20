using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Section_1;

/*
 Метеорологическая служба вашего города решила исследовать погоду новым способом.

Под температурой воздуха в конкретный день будем понимать максимальную температуру в этот день.
Под хаотичностью погоды за n дней служба понимает количество дней, в которые температура строго больше, чем в день до (если такой существует) и в день после текущего (если такой существует). 
Например, если за 5 дней максимальная температура воздуха составляла [1, 2, 5, 4, 8] градусов, то хаотичность за этот период равна 2: в 3-й и 5-й дни выполнялись описанные условия.
Определите по ежедневным показаниям температуры хаотичность погоды за этот период.

Заметим, что если число показаний n=1, то единственный день будет хаотичным.

Формат ввода
В первой строке дано число n –— длина периода измерений в днях, 1 ≤ n≤ 10^5. Во второй строке даны n целых чисел –— значения температуры в каждый из n дней. Значения температуры не превосходят 273 по модулю.

Формат вывода
Выведите единственное число — хаотичность за данный период. 
 */
public class Task_D
{
    private static TextReader _reader;
    private static TextWriter _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        var count = ReadInt();

        var temperatures = new List<short> { -274 };
        temperatures.AddRange(ReadList());
        temperatures.Add(-274);

        // Минимум 3 числа в массиве
        var randomness = 0;

        for (int i = 1; i <= count; i++)  // ввели 3 числа, в массиве 5 [0-4], проверяем 1 2 3
        {
            if (temperatures[i] > temperatures[i - 1]
               && temperatures[i] > temperatures[i + 1])
            {
                randomness++;
            }
        }

        ReturnAnswer(randomness);
    }

    private static void ReturnAnswer(int randomness)
    {
        _writer.Write("{0}", randomness);
        _reader.Close();
        _writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(_reader.ReadLine());
    }

    private static List<short> ReadList() =>
        _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(short.Parse)
            .ToList();
}