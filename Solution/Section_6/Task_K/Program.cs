using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Алгоритм Дейкстры - Дерево кратчайших путей - SPT - Достопримечательности
namespace Task_K
{
    /*
    Вы приехали на архипелаг Алгосы (наконец-то!).
    Здесь есть n достопримечательностей.
    Ваша лодка может высадить вас у одной из них, забрать у какой-то другой,
    возможно, той же самой достопримечательности и увезти на материк.
    Чтобы более тщательно спланировать свой маршрут, вы хотите узнать расстояния между каждой парой достопримечательностей Алгосов. 
    Некоторые из них соединены мостами, по которым вы можете передвигаться в любую сторону.
    Всего мостов m. Есть вероятность, что карта архипелага устроена так,
    что нельзя добраться от какой-то одной достопримечательности до другой без использования лодки.
    Найдите кратчайшие расстояния между всеми парами достопримечательностей.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static new List<(int to, int weigth)>[] _vertexes;
        private static int?[] _previous;
        private static bool[] _visited;
        private static int[] _distance;

        static void Main(string[] args)
        {
            var list = ReadList();
            var n = list[0]; // количество вершин
            var m = list[1]; // количество ребер
            Initial(n);
            FillVertexes(m, n);

            for (int v = 1; v < n + 1; v++)
            {
                Dijkstra(v);

                // Выводим вершины в порядке обхода
                for (int i = 1; i < n + 1; i++)
                {
                    Console.Write($"{(_distance[i] != int.MaxValue ? _distance[i] : -1)} ");
                }

                Console.WriteLine();
            }
        }

        private static void Initial(int n)
        {
            _vertexes = new List<(int to, int weigth)>[n + 1];
            _distance = new int[n + 1];
            _previous = new int?[n + 1];
            _visited = new bool[n + 1];
            _distance[0] = int.MinValue;

            for (int i = 1; i < n + 1; i++)
            {
                _vertexes[i] = new List<(int to, int weigth)>();
                _distance[i] = -1;
            }
        }

        private static void FillVertexes(int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                var edges = ReadList();
                _vertexes[edges[0]].Add((edges[1], edges[2]));
                _vertexes[edges[1]].Add((edges[0], edges[2]));
            }
        }

        static void Relax(int u, int v, int weight)
        {
            // Проверяем, не получился ли путь короче найденного ранее.
            if (_distance[v] > _distance[u] + weight)
            {
                _distance[v] = _distance[u] + weight;
                _previous[v] = u;
            }
        }

        static int? GetMinDistNotVisitedVertex()
        {
            // Находим ещё непосещённую вершину с минимальным расстоянием от s.
            int currentMinimum = int.MaxValue;
            int? currentMinimumVertex = null;

            //foreach (var v in graph.Vertices)
            for (int v = 1; v < _vertexes.Length; v++)
            {
                if (!_visited[v] && _distance[v] < currentMinimum)
                {
                    currentMinimum = _distance[v];
                    currentMinimumVertex = v;
                }
            }

            return currentMinimumVertex;
        }

        static void Dijkstra(int s)
        {
            // foreach (var v in graph.Vertices)
            for (int v = 1; v < _vertexes.Length; v++)
            {
                _distance[v] = int.MaxValue;         // Задаём расстояние по умолчанию.
                _previous[v] = null;          // Задаём предшественника для восстановления SPT.
                _visited[v] = false;        // Список статусов посещённости вершин.
            }

            _distance[s] = 0;     // Расстояние от вершины до самой себя 0.

            while (true)
            {
                int? u = GetMinDistNotVisitedVertex();

                if (u == null || _distance[u.Value] == int.MaxValue)
                {
                    break;
                }

                _visited[u.Value] = true;
                // из множества рёбер графа выбираем те, которые исходят из вершины u
                var neighbours = _vertexes[u.Value];

                foreach (var neighbour in neighbours)
                {
                    Relax(u.Value, neighbour.to, neighbour.weigth);
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