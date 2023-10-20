using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
// Дорогая сеть - максимальное остовное дерево
namespace Section_6_1
{
    /*
    Задание
    Тимофей решил соединить все компьютеры в своей компании в единую сеть.
    Для этого он придумал построить минимальное остовное дерево, чтобы эффективнее использовать ресурсы.
    Но от начальства пришла новость о том, что выделенный на сеть бюджет оказался очень большим и его срочно надо израсходовать.
    Поэтому Тимофея теперь интересуют не минимальные, а максимальные остовные деревья.
    Он поручил вам найти вес такого максимального остовного дерева в неориентированном графе, который задаёт схему офиса.
   */

    /*
    -- ПРИНЦИП РАБОТЫ --

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --


    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --

     */

    /// <summary>
    /// ID успешной посылки:
    /// </summary>
    public class Final_A
    {
        private static StreamReader _reader = new(Console.OpenStandardInput());

        public static void Main1(string[] args)
        {
            var list = ReadList();
            var verticesCount = list[0]; // количество вершин
            var edgesCount = list[1]; // количество ребер

            var graph = new Graph(verticesCount);

            for (int i = 0; i < edgesCount; i++)
            {
                var edges = ReadList();
                graph.AddEdge(edges[0] - 1, edges[1] - 1, edges[2]);
            }

            try
            {
                var mst = MST.Find(graph);
                Console.WriteLine(mst.Sum(x => x.Weight));
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static List<int> ReadList() =>
            _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList() ?? new List<int>();
    }

    public static class MST
    {
        private const string NotFoundMessage = "Oops! I did it again";

        /// <summary>
        /// Множество вершин, уже добавленных в остов.
        /// </summary>
        private static List<int> _addedToSkeleton;

        /// <summary>
        /// Множество вершин, ещё не добавленных в остов
        /// </summary>
        private static HashSet<int> _notAddedToSkeleton;

        /// <summary>
        /// Массив рёбер, исходящих из остовного дерева.
        /// </summary>
        private static PriorityQueue<Edge, int> _edges;

        /// <summary>
        /// Рёбра, составляющие MST
        /// </summary>
        private static List<Edge> _maxSpanningTree;

        /// <summary>
        /// Поиск максимального остовного дерева в неориентированном графе
        /// </summary>
        /// <param name="graph"></param>
        public static IList<Edge> Find(Graph graph)
        {
            Initial(graph);

            void AddVertexToSkeleton(int vertex)
            {
                _notAddedToSkeleton.Remove(vertex);
                _addedToSkeleton.Add(vertex);

                // Добавляем все рёбра, которые инцидентны v, но их конец ещё не в остове.
                foreach (var edge in graph.VertexEdges[vertex])
                {
                    if (_notAddedToSkeleton.Contains(edge.End))
                    {
                        // инвертируем приоритет, т.к. ищем максимум
                        _edges.Enqueue(edge, 0 - edge.Weight);
                    }
                }
            }

            // Берём первую попавшуюся вершину.
            var vertex = 0;
            AddVertexToSkeleton(vertex);

            while (_notAddedToSkeleton.Count > 0 && _edges.Count > 0)
            {
                // Подразумеваем, что extractMinimum извлекает минимальное ребро из массива рёбер и больше данного ребра в массиве не будет.
                // Если хранить рёбра, исходящие из уже собранного подмножества остова в куче с поддержанием минимума, то выбирать ребро с минимальным весом будет легко. 
                var maxEdge = ExtractMaxEdge();

                if (maxEdge != null && _notAddedToSkeleton.Contains(maxEdge.End))
                {
                    // Если вместе с ребром в подграф добавляется новая вершина, то это ребро добавляется в остов.
                    AddVertexToSkeleton(maxEdge.End);
                    _maxSpanningTree.Add(maxEdge);
                }
            }

            if (_notAddedToSkeleton.Count > 0)
            {
                //  верни ошибку "Исходный граф несвязный"
                throw new InvalidDataException(NotFoundMessage);
            }

            return _maxSpanningTree;
        }

        private static Edge? ExtractMaxEdge() =>
            _edges.Count > 0 ? _edges.Dequeue() : null;

        private static void Initial(Graph graph)
        {
            for (int i = 0; i < graph.VerticesCount; i++)
            {
                _notAddedToSkeleton.Add(i);
            }

            _addedToSkeleton = new List<int>();
            _edges = new PriorityQueue<Edge, int>();
            _maxSpanningTree = new List<Edge>();
        }
    }

    public class Graph
    {
        private readonly List<Edge>[] _vertexEdges;

        public int VerticesCount { get; }

        /// <summary>
        /// Важно! Представление в виде списков смежности (позволило ускорить алгоритм)
        /// </summary>
        public List<Edge>[] VertexEdges => _vertexEdges;

        public Graph(int verticesCount)
        {
            VerticesCount = verticesCount;
            _vertexEdges = new List<Edge>[verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                _vertexEdges[i] = new List<Edge>();
            }
        }

        /// <summary>
        /// Добавляет ребро в граф
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="weight"></param>        
        public void AddEdge(int from, int to, int weight)
        {
            // Т.к. граф неориентированный, то ребро 1->2 и 2->1 сохраняем дважды в отедльных ячейках
            VertexEdges[from].Add(new(from, to, weight));
            VertexEdges[to].Add(new(to, from, weight));
        }
    }

    /// <summary>
    /// Ребро графа
    /// </summary>
    public record Edge
    {
        /// <summary>
        /// Стартовая точка. Чисто для наглядности, можно удалить
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// Конечная точка
        /// </summary>
        public int End { get; }

        /// <summary>
        /// Вес пути Start -> End
        /// </summary>
        public int Weight { get; }

        public Edge(int start, int end, int weight)
        {
            Start = start;
            End = end;
            Weight = weight;
        }
    }
}