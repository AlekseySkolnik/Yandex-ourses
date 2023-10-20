using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// BFS - обход в ширину
namespace Task_D
{
    /*
     Задан неориентированный граф. 
     Обойдите поиском в ширину все вершины, достижимые из заданной вершины s.
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
            BFS(startVertex);
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
            }
        }

        private static void BFS(int startVertex)
        {
            // Создадим пустую очередь planned, куда будем записывать запланированные к посещению узлы
            var planned = new Queue<int>();
            // Добавим в planned стартовую вершину s.
            planned.Enqueue(startVertex);
            // Покрасим её в серый цвет: color[s] = gray.
            _colors[startVertex] = ColorEnum.Gray;

            // Пока в очереди planned остаются узлы, в цикле повторяем следующие действия:
            while (planned.Count > 0)
            {
                // Получить вершину из очереди. В этот момент она уже серая. Дать вершине имя (допустим, u).
                var virtexU = planned.Dequeue();

                // Перебрать все рёбра, исходящие из u.
                foreach (var virtexV in _vertexes[virtexU])
                {
                    // Серые и чёрные вершины уже либо в очереди, либо обработаны.
                    if (_colors[virtexV] == ColorEnum.White) 
                    {
                        // Если вершина v оказалась белой, перекрашиваем в серый и добавляем её в очередь.
                        _colors[virtexV] = ColorEnum.Gray;
                        planned.Enqueue(virtexV);
                    }

                    //  В противном случае пропускаем эту вершину.
                }

                // Перекрасить u в чёрный цвет. С этого момента вершина считается посещённой.
                _colors[virtexU] = ColorEnum.Black;
                // Выводим вершины в порядке обхода
                Console.Write($"{virtexU} ");
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