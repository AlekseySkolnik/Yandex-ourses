using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Section_1;

// Представьте себе онлайн-игру для поездки в метро: игрок нажимает на кнопку, и на экране появляются три случайных числа. Если все три числа оказываются одной чётности, игрок выигрывает.
// Напишите программу, которая по трём числам определяет, выиграл игрок или нет.
public class Task_B
{
    private static TextReader _reader;
    private static TextWriter _writer;

    const string WIN = nameof(WIN);
    const string FAIL = nameof(FAIL);

    private static string _answer = FAIL;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        byte count = 0b_0000_0110;

        foreach (var item in ReadList())
        {
            count = (byte)((item % 2 == 0)
                ? count >> 1
                : count << 1);
        }

        var result = (byte)(count & 0b_0000_1111) == 0 ? WIN : FAIL;

        _writer.WriteLine(result);

        _reader.Close();
        _writer.Close();
    }

    private static List<int> ReadList() =>
        _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
}