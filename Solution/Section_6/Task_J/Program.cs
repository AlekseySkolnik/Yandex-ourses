using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Топологическая сортировка
namespace Task_J
{
    /*
    Дан ациклический ориентированный граф (так называемый DAG, directed acyclic graph).
    Найдите его топологическую сортировку, то есть выведите его вершины в таком порядке, 
    что все рёбра графа идут слева направо.
    У графа может быть несколько подходящих перестановок вершин. 
    Вам надо найти любую топологическую сортировку.*/

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static ColorEnum?[] _colors;
        private static new List<int>[] _vertexes;
        private static Stack<int> _orderStack = new Stack<int>();

        static void Main(string[] args)
        {
            var list = ReadList();
            var n = list[0]; // количество вершин
            var m = list[1]; // количество ребер
            Initial(n);
            FillVertexes(m, n);

            var startVertex = 1; // номер стартовой вершины s (1 ≤ s ≤ n)
            DFS(startVertex);

            for (int i = 1; i < n + 1; i++)
            {
                if (_colors[i] == ColorEnum.White)
                {
                    DFS(i);
                }              
            }

            var builder = new StringBuilder();

            while (_orderStack.Count > 0) 
            {
                builder.Append($"{_orderStack.Pop()} ");
            }

            Console.WriteLine(builder.ToString());
        }

        private static void Initial(int n)
        {
            _colors = new ColorEnum?[n + 1];
            _vertexes = new List<int>[n + 1];

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
                    // И сразу кладём её обратно в стек: это позволит алгоритму позднее вспомнить обратный путь по графу.
                    stack.Push(vertexIndex);

                    //  Теперь добавляем в стек все непосещённые соседние вершины, вместо вызова рекурсии
                    foreach (var w in _vertexes[vertexIndex])
                    {
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
                    _orderStack.Push(vertexIndex);
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