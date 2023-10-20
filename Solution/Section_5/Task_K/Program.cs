using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Выведи диапазон
namespace Task_K;
/*
Напишите функцию, которая будет выводить по неубыванию все ключи от L до R
включительно в заданном бинарном дереве поиска. Ключи в дереве могут повторяться.
Решение должно иметь сложность O(h+k), где h –— глубина дерева, k — число элементов в ответе.
В данной задаче если в узле содержится ключ x, то другие ключи, равные x,
могут быть как в правом, так и в левом поддереве данного узла.
(Дерево строил стажёр, так что ничего страшного).
 */

public class Task_K
{
    private static StreamWriter _reader = new StreamWriter(Console.OpenStandardOutput());
    public static void Main(string[] args)
    {
        var node1 = new Node(1);
        var node2 = new Node(-5);
        var node3 = new Node(3)
        {
            Left = node1,
            Right = node2
        };
        var node4 = new Node(2)
        {
            Left = node3
        };

        Solution.PrintRange(node4, 1, 3, _reader);
    }
}

public class Solution
{
    public static List<int> _list = new List<int>();
    public static int Left = 0;
    public static int Right = 0;
    public static StreamWriter _writer;

    public static void PrintRange(Node root, int left, int right, StreamWriter writer)
    {
        Left = left;
        Right = right;
        _writer = writer;
        FillList(root);
    }

    private static void FillList(Node vertex)
    {
        if (vertex.Left != null)
        {
            FillList(vertex.Left);
        }

        if (vertex.Value >= Left && vertex.Value <= Right)
        {
            _writer.WriteLine(vertex.Value);
        }

        if (vertex.Right != null)
        {
            FillList(vertex.Right);
        }
    }
}