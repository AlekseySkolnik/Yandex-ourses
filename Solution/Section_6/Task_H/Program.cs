using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// DFS - Время выходить
namespace Task_H
{
    /*
Вам дан ориентированный граф. 
    Известно, что все его вершины достижимы из вершины s = 1.
    Найдите время входа и выхода при обходе в глубину, производя первый запуск из вершины s. 
    Считайте, что время входа в стартовую вершину равно 0. Соседей каждой вершины обходите в порядке увеличения номеров.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static ColorEnum?[] _colors;
        private static new List<int>[] _vertexes;
        private static int?[] _entry;
        private static int?[] _leave;
        private static int _time = -1;

        static void Main(string[] args)
        {
            var list = ReadList();
            var n = list[0]; // количество вершин
            var m = list[1]; // количество ребер
            Initial(n);
            FillVertexes(m, n);

            var startVertex = 1; // номер стартовой вершины s (1 ≤ s ≤ n)
            DFS(startVertex);

            var builder = new StringBuilder();
            for (int i = 1; i < n + 1; i++)
            {
                builder.Append($"{_entry[i]} {_leave[i]}");
                builder.AppendLine();
            }

            Console.WriteLine(builder.ToString());
        }

        private static void Initial(int n)
        {
            _colors = new ColorEnum?[n + 1];
            _vertexes = new List<int>[n + 1];
            _entry = new int?[n + 1];
            _leave = new int?[n + 1];

            for (int i = 1; i < n + 1; i++)
            {
                _vertexes[i] = new List<int>();
                _colors[i] = ColorEnum.White;
            }
        }

        private static void FillVertexes(int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                var edges = ReadList();
                _vertexes[edges[0]].Add(edges[1]);
            }

            for (int i = 1; i < n + 1; i++)
            {
                _vertexes[i].Sort();
            }
        }

        private static void DFS(int startVertex)
        {
            var stack = new Stack<int>();
            stack.Push(startVertex);

            while (stack.Count > 0)
            {
                var vertexIndex = stack.Pop(); // Это может быть как новая вершина, так и уже посещённая однажды

                if (_colors[vertexIndex] == ColorEnum.White)
                {
                    // Красим вершину в серый.
                    _colors[vertexIndex] = ColorEnum.Gray;
                    _time++;
                    _entry[vertexIndex] = _time;

                    // И сразу кладём её обратно в стек: это позволит алгоритму позднее вспомнить обратный путь по графу.
                    stack.Push(vertexIndex);

                    //  Теперь добавляем в стек все непосещённые соседние вершины, вместо вызова рекурсии
                    for (int i = _vertexes[vertexIndex].Count - 1; i >= 0; i--)
                    {
                        int w = _vertexes[vertexIndex][i];

                        if (_colors[w] == ColorEnum.White)
                        {
                            stack.Push(w);
                        }
                    }
                }
                else if (_colors[vertexIndex] == ColorEnum.Gray)
                {
                    // Серую вершину мы могли получить из стека только на обратном пути.
                    // Следовательно, её следует перекрасить в чёрный.
                    _colors[vertexIndex] = ColorEnum.Black;
                    _time++;
                    _leave[vertexIndex] = _time;
                }
            }
        }

        private static List<int> ReadList() =>
            _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList() ?? new List<int>();
    }

    public enum ColorEnum
    {
        White = 0,
        Gray = 1,
        Black = 2
    }
}