using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// DFS - обход в глубину
namespace Task_C
{
    /*
    Задан неориентированный граф.
    Обойдите с помощью DFS все вершины, достижимые из заданной вершины s.
    Выведите их в порядке обхода, если начинать обход из s.

    Формат ввода
    В первой строке дано количество вершин n (1 ≤ n ≤ 105) и рёбер m (0 ≤ m ≤ 105).
    Далее в m строках описаны рёбра графа.
    Каждое ребро описывается номерами двух вершин u и v (1 ≤ u, v ≤ n). 
    В последней строке дан номер стартовой вершины s (1 ≤ s ≤ n). 
    В графе нет петель и кратных рёбер.
    
    Формат вывода
    Выведите вершины в порядке обхода, считая что при запуске от каждой конкретной вершины
    её соседи будут рассматриваться в порядке возрастания 
    (то есть если вершина 2 соединена с 1 и 3, то сначала обход пойдёт в 1, а уже потом в 3).
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static ColorEnum?[] _colors;
        private static new List<int>[] _vertexes;

        static void Main(string[] args)
        {
            var list = ReadList();
            var n = list[0]; // количество вершин
            var m = list[1]; // количество ребер
            Initial(n);
            FillVertexes(m, n);

            var startVertex = int.Parse(_reader.ReadLine()); // номер стартовой вершины s (1 ≤ s ≤ n)
            DFS(startVertex);

            foreach (var j in _vertexes[startVertex])
            {
                if (_colors[j] == ColorEnum.White)
                {
                    DFS(j);
                }
            }
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
                _vertexes[edges[1]].Add(edges[0]);
            }

            for (int i = 1; i < n + 1; i++)
            {
                _vertexes[i].Sort();
                _vertexes[i].Reverse();
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
                 
                    // Выводим вершины в порядке обхода
                    Console.Write($"{vertexIndex} ");
                  
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