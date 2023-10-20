using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Section_1;

/*
Игра «Тренажёр для скоростной печати» представляет собой поле из клавиш 4x4.
В нём на каждом раунде появляется конфигурация цифр и точек. 
На клавише написана либо точка, либо цифра от 1 до 9.
В момент времени t игрок должен одновременно нажать на все клавиши, на которых написана цифра t.
Гоша и Тимофей могут нажать в один момент времени на k клавиш каждый. 
Если в момент времени t нажаты все нужные клавиши, то игроки получают 1 балл.
Найдите число баллов, которое смогут заработать Гоша и Тимофей, если будут нажимать на клавиши вдвоём.

Формат ввода
В первой строке дано целое число k (1 ≤ k ≤ 5).
В четырёх следующих строках задан вид тренажёра -— по 4 символа в каждой строке.
Каждый символ – либо точка, либо цифра от 1 до 9. 
Символы одной строки идут подряд и не разделены пробелами.

Формат вывода
Выведите единственное число -— максимальное количество баллов, которое смогут набрать Гоша и Тимофей.
 */

//https://contest.yandex.ru/contest/22450/run-report/88581996/
public class Task_Final_2
{
    private const int TableSize = 4;
    private const int NumberSize = 9;

    private static TextReader _reader;
    private static TextWriter _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        var fingers = ReadInt();
        var digitals = new int[NumberSize] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        FillDigitalsWithData(digitals);
        var scores = CalculateScores(fingers, digitals);

        _writer.WriteLine(scores);
        _reader.Close();
        _writer.Close();
    }

    private static int CalculateScores(int fingers, int[] digitals)
    {
        var scores = 0;

        foreach (var digit in digitals)
        {
            if (digit != 0 && digit <= 2 * fingers)
            {
                scores++;
            }
        }

        return scores;
    }

    private static void FillDigitalsWithData(int[] digitals)
    {
        for (int i = 0; i < TableSize; i++)
        {
            var symbols = _reader.ReadLine();

            foreach (var symbol in symbols)
            {
                if (char.IsDigit(symbol))
                {
                    var index = int.Parse(symbol.ToString());
                    digitals[index - 1]++;
                }
            }
        }
    }

    private static int ReadInt()
    {
        return int.Parse(_reader.ReadLine());
    }
}