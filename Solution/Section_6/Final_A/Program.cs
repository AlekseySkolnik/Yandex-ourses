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
// Дорогая сеть - максимальное остовное дерево (MST)
namespace Section_6
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
    В задаче используется алгоритм Прима на очереди с приоритетами:
    1. Берётся любая вершина графа.
    2. Рассмотрим все рёбра, исходящие из этой вершины. 
       Возьмём ребро с max весом и добавим в остов ребро и вершину, в которую оно входило.
    3. Добавим ко множеству потенциально добавляемых рёбер все, которые исходят из новой вершины и входят в вершины, ещё не включённые в остов.
    4. Повторяем пункты 2 и 3 до тех пор, пока в остовном дереве не будет n вершин и, соответственно, n−1 рёбер.
       Так как на каждой итерации цикла мы добавляем ровно одно ребро и одну вершину, нам потребуется n−1 итерация.
    
    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
     O(∣E∣⋅log∣V∣)
    Пояснение:
        Алгоритму требуется число шагов, пропорциональное количеству вершин.
        На каждом шаге мы находим минимальное по весу ребро.
        На поиск минимального ребра нам требуется в худшем случае перебрать все рёбра. 
        В итоге сложность алгоритма будет O(∣V∣⋅∣E∣). 
        Если хранить рёбра, исходящие из уже собранного подмножества остова в куче с поддержанием минимума, то выбирать ребро с минимальным весом будет легко.
        Если вместе с ребром в подграф добавляется новая вершина, то это ребро добавляется в остов.
        Если ребро соединяет две вершины, уже присутствующее в подмножестве остова, мы отбрасываем его из дальнейшего рассмотрения и из кучи в том числе.
        Благодаря приоритетной очереди сложность алгоритма Прима стала O(∣E∣⋅log∣V∣), где 
        ∣E∣ — количество рёбер в графе, а ∣V∣ — количество вершин.

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
       O(∣E∣+∣V∣)
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/25070/run-report/90335103/
    /// </summary>
    public class Final_A
    {
        private static StreamReader _reader = new(Console.OpenStandardInput());

        public static void Main(string[] args)
        {
            var list = ReadList();
            var verticesCount = list[0]; // количество вершин
            var edgesCount = list[1]; // количество ребер

            var graph = new Graph(verticesCount);

            FillEdges(edgesCount, graph);

            MST.Find(graph);
        }

        private static void FillEdges(int edgesCount, Graph graph)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edges = ReadList();
                graph.AddEdge(edges[0] - 1, edges[1] - 1, edges[2]);
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
        /// Список рёбер, исходящих из остовного дерева, на очереди с приоритетами
        /// </summary>
        private static PriorityQueue<Edge, int> _edges;

        /// <summary>
        /// Рёбра, составляющие MST
        /// </summary>
        private static int _maxSpanningTreeSum;

        /// <summary>
        /// Поиск максимального остовного дерева в неориентированном графе
        /// </summary>
        /// <param name="graph"></param>
        public static void Find(Graph graph)
        {
            Initial(graph);

            void AddVertexToSkeleton(int vertex)
            {
                _notAddedToSkeleton.Remove(vertex);
                _addedToSkeleton.Add(vertex);

                // Добавляем все рёбра, которые инцидентны v, но их конец ещё не в остове.
                foreach (var edge in graph.VertexEdges[vertex].Where(edge => _notAddedToSkeleton.Contains(edge.End)))
                {
                    // инвертируем приоритет, т.к. ищем максимум
                    _edges.Enqueue(edge, 0 - edge.Weight);
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
                    _maxSpanningTreeSum += maxEdge.Weight;
                }
            }

            if (_notAddedToSkeleton.Count > 0)
            {
                Console.WriteLine(NotFoundMessage);
            }
            else
            {
                Console.WriteLine(_maxSpanningTreeSum);
            }
        }

        private static Edge? ExtractMaxEdge() =>
            _edges.Count > 0 ? _edges.Dequeue() : null;

        private static void Initial(Graph graph)
        {
            _addedToSkeleton = new List<int>();
            _edges = new PriorityQueue<Edge, int>();
            _notAddedToSkeleton = new HashSet<int>(graph.VerticesCount);

            for (int i = 0; i < graph.VerticesCount; i++)
            {
                _notAddedToSkeleton.Add(i);
            }
        }
    }

    public class Graph
    {
        private readonly List<Edge>[] _vertexEdges;

        /// <summary>
        /// Количетсво вершин графа
        /// </summary>
        public int VerticesCount { get; }

        /// <summary>
        /// Список ребер графа. Важно! Представление в виде списков смежности (позволило ускорить алгоритм)
        /// </summary>
        public IReadOnlyList<Edge>[] VertexEdges => _vertexEdges;

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
            // Т.к. граф неориентированный, то ребро 1->2 и 2->1 сохраняем дважды в отедельных ячейках
            _vertexEdges[from].Add(new(from, to, weight));
            _vertexEdges[to].Add(new(to, from, weight));
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