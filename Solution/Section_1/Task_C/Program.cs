using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Section_1;

/*
Дана матрица. Нужно написать функцию, которая для элемента возвращает всех его соседей.
Соседним считается элемент, находящийся от текущего на одну ячейку влево, вправо, вверх или вниз. Диагональные элементы соседними не считаются.
Например, в матрице A соседними элементами для (0, 0) будут 2 и 0. А для (2, 1) –— 1, 2, 7, 7.
Формат ввода
В первой строке задано n — количество строк матрицы. Во второй — количество столбцов m. Числа m и n не превосходят 1000. 
В следующих n строках задана матрица. Элементы матрицы — целые числа, по модулю не превосходящие 1000.
В последних двух строках записаны координаты элемента, соседей которого нужно найти. Индексация начинается с нуля.

Формат вывода
Напечатайте нужные числа в возрастающем порядке через пробел.
 * */
public class Task_C
{
    private static TextReader _reader;
    private static TextWriter _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        var rowCount = ReadInt();
        var columnCount = ReadInt();
        var matrix = ReadMatrix(rowCount, columnCount);
        var coordinateX = ReadInt();
        var coordinateY = ReadInt();

        var hasLeft = coordinateY != 0;
        var hasRight = coordinateY != columnCount - 1;
        var hasUp = coordinateX != 0;
        var hasDown = coordinateX != rowCount - 1;
        var array = new List<int>();

        if (hasLeft)
        {
            array.Add(matrix[coordinateX, coordinateY - 1]);
        }

        if (hasRight)
        {
            array.Add(matrix[coordinateX, coordinateY + 1]);
        }

        if (hasUp)
        {
            array.Add(matrix[coordinateX - 1, coordinateY]);
        }

        if (hasDown)
        {
            array.Add(matrix[coordinateX + 1, coordinateY]);
        }

        array.Sort();

        var answer = string.Join(' ', array);

        _writer.WriteLine(answer);

        _reader.Close();
        _writer.Close();
    }

    private static int[,] ReadMatrix(int rowCount, int columnCount)
    {
        var matrix = new int[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            var str = ReadList();
            for (int j = 0; j < str.Count; j++)
            {
                matrix[i, j] = str[j];
            }
        }

        return matrix;
    }

    private static int ReadInt()
    {
        return int.Parse(_reader.ReadLine());
    }

    private static List<int> ReadList() =>
        _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
}