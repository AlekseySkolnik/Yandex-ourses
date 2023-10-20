using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Сбалансированное дерево (АВЛ-дерево)
namespace Task_B;
/*
Гоше очень понравилось слушать рассказ Тимофея про деревья. 
Особенно часть про сбалансированные деревья.
Он решил написать функцию, которая определяет, сбалансировано ли дерево.
Дерево считается сбалансированным, 
если левое и правое поддеревья каждой вершины отличаются по высоте не больше, чем на единицу.

Формат ввода
На вход функции подаётся корень бинарного дерева.
 */

public class Task_K
{
    private static StreamWriter _reader = new StreamWriter(Console.OpenStandardOutput());
    public static void Main(string[] args)
    {
        var node12 = new Node(12);
        var node4 = new Node(4) { Left = node12 };
        var node8 = new Node(8);
        var node7 = new Node(7) { Left = node4, Right = node8 };
        var node2 = new Node(2);
        var node0 = new Node(0) { Left = node2, Right = node7 };

        Console.WriteLine(Solution.Solve(node0));
    }
}

public class Solution
{
    public static bool Solve(Node root)
    {
        if (root == null)
        {
            return true;
        }

        return IsAVL(root) && IsAVL(root.Left) && IsAVL(root.Right);
    }

    private static bool IsAVL(Node node)
    {
        if (node == null)
        {
            return true;
        }

        var leftHeight = GetHeight(node.Left);
        var rightHeight = GetHeight(node.Right);

        return Math.Abs(leftHeight - rightHeight) <= 1;
    }

    private static int GetHeight(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        var leftHeight = GetHeight(node.Left) + 1;
        var rightHeight = GetHeight(node.Right) + 1;

        return Math.Max(leftHeight, rightHeight);
    }
}