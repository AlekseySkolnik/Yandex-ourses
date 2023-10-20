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
// Железные дороги - DFS c поиском цикла
namespace Section_6
{
    /*
    Задание

    В стране X есть n городов, которым присвоены номера от 1 до n.
    Столица страны имеет номер n. Между городами проложены железные дороги.
    Однако дороги могут быть двух типов по ширине полотна.
    Любой поезд может ездить только по одному типу полотна.
    Условно один тип дорог помечают как R, а другой как B. 
    То есть если маршрут от одного города до другого имеет как дороги типа R,
    так и дороги типа B, то ни один поезд не сможет по этому маршруту проехать.
    От одного города до другого можно проехать только по маршруту, состоящему исключительно из дорог типа R или только из дорог типа B.
    Но это ещё не всё. По дорогам страны X можно двигаться только от города с меньшим номером к городу с большим номером.
    Это объясняет большой приток жителей в столицу, у которой номер n.
    Карта железных дорог называется оптимальной, если не существует пары городов A и B такой, 
    что от A до B можно добраться как по дорогам типа R, так и по дорогам типа B.
    Иными словами, для любой пары городов верно, что от города с меньшим номером до города с бОльшим номером 
    можно добраться по дорогам только какого-то одного типа или же что маршрут построить вообще нельзя.
    Выясните, является ли данная вам карта оптимальной.

   */

    /*
    -- ПРИНЦИП РАБОТЫ --

    В основе алгоритма будет лежать стандартный алгоритм DFS, описанный в теории, с единственным дополнением -
    поиском цикла в графе (194-198 строки кода), для чего воспользуемся массивом цветов вершин. 
    Если при проверке смежных по исходящим дугам вершин очередная вершина окажется серой — цикл есть.

    Для чего нам поиск цикла в графе?

    При заполнении графа для дорог с полотном типа R будем записывать ребро с указанным направлением,
    а для дорог типа B инвертируем направление ребра. Дальше запустим алгоритм DFS для полного обхода графа в глубину.
    Если при проверке смежных по исходящим дугам вершин (городов) очередная вершина окажется серой — в графе есть цикл.
    Это означает, что есть пара городов, между которыми есть два маршрута с разным типом дорог.
    Значит, карта железных дорог в таком случае не является оптимальной. 
    Таким образом, задача поиска оптимальности железнодорождной карты сводится к поиску к циклу в графе.

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

    Возьмем пример из задачи. Ввод: 3 RB R.
    (1 -> 2, тип R),
    (1 => 3, тип В)
    (2 -> 3, тип R)
    Из 1 в 3 можно добраться двумя путями:
    (1 => 3, тип В) и  (1 -> 2, тип R) +  (2 -> 3, тип R). Эта карта является неоптимальной.
    Если инвертировать направление ребра для дороги B, то получится цикл (замкнутный граф):
    (1 -> 2, тип R) -> (2 -> 3, тип R) -> (3 => 1, тип В).
    Наличие цикла свидетельствует о неоптимальности карты.
    Предположим, что это не так (метод от противного).
    Пусть карта оптимальна, если есть цикл. Также по условию карта оптимальна,
    если не существует пары городов X и Y такой, что можно добраться как по дорогам типа R, так и по дорогам типа B.
    Пусть в графе существует цикл Xn, Xn+1, ... Xn+i, ... Xn.
    Xk ---B---> Xn ----B----> Xn+1---B-->Xj
     |                                    |
    Xk<===============R==================Xj
    Существует пара Xj, Xk такая, что Xk<Xj и при этом есть путь из Xk в Xj дорогами типа B.
    Одновременно с этим существует путь из Xk в Xj дорогами типа R (реверсивное ребро).
    Мы пришли к противоречию, следовательно, карта не оптимальна.

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
   
    В алгоритме используется  DFS со списками смежности, поэтому сложность  O(|V| + |E|),
    где E - количество вершин, V - количество рёбер.

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    O(|V| * |E|), где E - количество вершин, V - количество рёбер.
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/25070/run-report/90339882/
    /// </summary>
    public class Final_B
    {
        private static StreamReader _reader = new(Console.OpenStandardInput());
        private const string AnswerYes = "YES";
        private const string AnswerNo = "NO";

        public static void Main(string[] args)
        {
            var verticesCount = int.Parse(_reader?.ReadLine()); // количество городов в стране
            var graph = new Graph(verticesCount);

            FillGraph(verticesCount, graph);

            var dfs = new DFS(verticesCount);
            var startVertex = 1;
            var existCycle = dfs.FindCycle(graph, startVertex);

            if (existCycle)
            {
                Console.WriteLine(AnswerNo);
                return;
            }

            for (int j = 2; j <= graph.VerticesCount; j++)
            {
                if (dfs.Colors[j] == ColorEnum.White)
                {
                    existCycle = dfs.FindCycle(graph, j);

                    if (existCycle)
                    {
                        Console.WriteLine(AnswerNo);
                        return;
                    }
                }
            }

            Console.WriteLine(AnswerYes);
        }

        private static void FillGraph(int verticesCount, Graph graph)
        {
            for (int i = 1; i < verticesCount; i++)
            {
                var j = 1;

                foreach (var jColor in _reader?.ReadLine())
                {
                    var isDirect = jColor == 'B';

                    if (isDirect)
                    {
                        graph.AddEdge(i, i + j);
                    }
                    else
                    {
                        graph.AddEdge(i + j, i);
                    }

                    j++;
                }
            }
        }
    }

    public class DFS
    {
        private ColorEnum?[] _colors;
        public IReadOnlyList<ColorEnum?> Colors => _colors;

        public DFS(int verticesCount)
        {
            _colors = new ColorEnum?[verticesCount + 1];

            for (int i = 1; i < verticesCount + 1; i++)
            {
                _colors[i] = ColorEnum.White;
            }
        }

        /// <summary>
        /// Поиск цикла в графе с помощью DFS
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="startVertex"></param>
        /// <returns></returns>
        public bool FindCycle(Graph graph, int startVertex)
        {
            var stack = new Stack<int>();
            stack.Push(startVertex);

            while (stack.Count > 0)
            {
                // Получаем из стека очередную вершину.
                var vertexIndex = stack.Pop(); // Это может быть как новая вершина, так и уже посещённая однажды

                if (_colors[vertexIndex] == ColorEnum.White)
                {
                    // Красим вершину в серый.
                    _colors[vertexIndex] = ColorEnum.Gray;

                    // И сразу кладём её обратно в стек: это позволит алгоритму позднее вспомнить обратный путь по графу.
                    stack.Push(vertexIndex);

                    //  Теперь добавляем в стек все непосещённые соседние вершины
                    for (int i = 0; i < graph.Vertices[vertexIndex].Count; i++)
                    {
                        var neighboringVertex = graph.Vertices[vertexIndex][i];

                        if (_colors[neighboringVertex] == ColorEnum.Gray)
                        {
                            // Цикл найден
                            return true;
                        }
                        else if (_colors[neighboringVertex] == ColorEnum.White)
                        {
                            stack.Push(neighboringVertex);
                        }
                    }
                }
                else if (_colors[vertexIndex] == ColorEnum.Gray)
                {
                    // Серую вершину мы могли получить из стека только на обратном пути. Следовательно, её следует перекрасить в чёрный.
                    _colors[vertexIndex] = ColorEnum.Black;
                }
            }

            return false;
        }
    }

    public class Graph
    {
        private static new List<int>[] _vertices;

        public IReadOnlyList<int>[] Vertices => _vertices;
        public int VerticesCount { get; }

        public Graph(int verticesCount)
        {
            VerticesCount = verticesCount;

            _vertices = new List<int>[verticesCount + 1];

            for (int i = 1; i <= verticesCount; i++)
            {
                _vertices[i] = new List<int>();
            }
        }

        public void AddEdge(int from, int to)
        {
            _vertices[from].Add(to);
        }
    }

    public enum ColorEnum
    {
        White = 0,
        Gray = 1,
        Black = 2
    }
}