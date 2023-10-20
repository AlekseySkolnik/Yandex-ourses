using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Построить список смежности
namespace Task_A
{
    /*
    Алла пошла на стажировку в студию графического дизайна, где ей дали такое задание:
    для очень большого числа ориентированных графов преобразовать их список рёбер в список смежности.
    Чтобы побыстрее решить эту задачу, она решила автоматизировать процесс.
    Помогите Алле написать программу, которая по списку рёбер графа будет строить его список смежности.

    Формат ввода
    В первой строке дано число вершин n (1 ≤ n ≤ 100) и число ребер m (1 ≤ m ≤ n(n-1)). 
    В следующих m строках заданы ребра в виде пар вершин (u,v), если ребро ведет от u к v.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var list = ReadList();
            var n = list[0];
            var m = list[1];
            var adjacencyList = new Vertex[n + 1];

            for (int i = 1; i < n + 1; i++)
            {
                adjacencyList[i] = new Vertex();
            }

            for (int i = 0; i < m; i++)
            {
                var edges = ReadList();
                adjacencyList[edges[0]].Paths.Add(edges[1]);
            }

            for (int i = 1; i < n + 1; i++)
            {
                var edgeCount = adjacencyList[i].Paths.Count;
                adjacencyList[i].Paths.Sort();
                var vertexes = string.Join(' ', adjacencyList[i].Paths);
                Console.WriteLine($"{edgeCount} {vertexes}");
            }
        }

        private static List<int> ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList() ?? new List<int>();
    }

    public class Vertex
    {
        public List<int> Paths { get; } = new List<int>();
    }
}