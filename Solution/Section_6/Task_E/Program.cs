using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Компоненты связности
namespace Task_E
{
    /*
     * Вам дан неориентированный граф. Найдите его компоненты связности.
    */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static int[] _colors;
        private static int _componentCount = 1;
        private static new List<int>[] _vertexes;
        private static List<int> _order = new List<int>();

        static void Main(string[] args)
        {
            var list = ReadList();
            var n = list[0]; // количество вершин
            var m = list[1]; // количество ребер
            Initial(n);
            FillVertexes(m, n);

            var builder = new StringBuilder();

            for (int i = 1; i < n + 1; i++)
            {
                if (_colors[i] == -1)
                {
                    DFS(i);
                    _componentCount++;
                    _order.Sort();
                    builder.AppendLine(string.Join(' ', _order));
                    _order.Clear();
                }
            }

            Console.WriteLine(_componentCount - 1);
            Console.WriteLine(builder.ToString());
        }

        private static void Initial(int n)
        {
            _colors = new int[n + 1];
            _vertexes = new List<int>[n + 1];

            for (int i = 1; i < n + 1; i++)
            {
                _vertexes[i] = new List<int>();
                _colors[i] = -1;
            }
        }

        private static void FillVertexes(int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                var edges = ReadList();
                _vertexes[edges[0]].Add(edges[1]);
                _vertexes[edges[1]].Add(edges[0]);
            }
        }

        private static void DFS(int startVertex)
        {
            var stack = new Stack<int>();
            stack.Push(startVertex);

            while (stack.Count > 0)
            {
                var vertexIndex = stack.Pop(); // Это может быть как новая вершина, так и уже посещённая однажды

                if (_colors[vertexIndex] == -1)
                {
                    // Красим вершину в серый.
                    _colors[vertexIndex] = 0;
                    // И сразу кладём её обратно в стек: это позволит алгоритму позднее вспомнить обратный путь по графу.
                    stack.Push(vertexIndex);

                    //  Теперь добавляем в стек все непосещённые соседние вершины, вместо вызова рекурсии
                    foreach (var w in _vertexes[vertexIndex])
                    {
                        if (_colors[w] == -1)
                        {
                            stack.Push(w);
                        }
                    }
                }
                else if (_colors[vertexIndex] == 0)
                {
                    // Серую вершину мы могли получить из стека только на обратном пути.
                    // Следовательно, её следует перекрасить в чёрный.
                    _colors[vertexIndex] = _componentCount;
                    _order.Add(vertexIndex);
                }
            }
        }

        private static List<int> ReadList() =>
            _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList() ?? new List<int>();
    }
}