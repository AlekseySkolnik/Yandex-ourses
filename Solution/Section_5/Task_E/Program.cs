using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using static Task_E.Solution;
// Дерево поиска
namespace Task_E;
/*
Гоша понял, что такое дерево поиска, и захотел написать функцию, которая определяет, является ли заданное дерево деревом поиска.
Значения в левом поддереве должны быть строго меньше, в правом —- строго больше значения в узле.
Помогите Гоше с реализацией этого алгоритма.
 */

public class Task_B
{
    public static void Main(string[] args)
    {
        var node1 = new Node(1);
        var node2 = new Node(4);
        var node3 = new Node(3)
        {
            Left = node1,
            Right = node2
        };
        var node4 = new Node(8);
        var node5 = new Node(5)
        {
            Left = node3,
            Right = node4
        };

        Console.WriteLine(Solve(node5));
        node2.Value = 5;
        Console.WriteLine(Solve(node5));
    }
}

public class Solution
{
    public static bool Solve(Node root)
    {
        var result = IsBST(root, int.MinValue, int.MaxValue);
        return result;
    }

    public static bool IsBST(Node node, int from, int to)
    {
        if (node == null)
        {
            return true;
        }

        if (node.Value <= from || node.Value > to)
        {
            return false;
        }

        Console.WriteLine($"node.Value = {node?.Value}, from = {from}, to = {to}");

        var IsLeftBST = IsBST(node.Left, from, node.Value);
        var IsRightBST = IsBST(node.Right, node.Value, to);

        return IsLeftBST && IsRightBST;
    }
}