using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Лампочки
namespace Task_A;
/*
Гоша повесил на стену гирлянду в виде бинарного дерева, в узлах которого находятся лампочки.
У каждой лампочки есть своя яркость.
Уровень яркости лампочки соответствует числу, расположенному в узле дерева.
Помогите Гоше найти самую яркую лампочку в гирлянде, то есть такую, у которой яркость наибольшая.
 */

public class Task_B
{
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

        Console.WriteLine(Solution.Solve(node4));
    }
}

public class Solution
{
    public static int Max = int.MinValue;

    public static int Solve(Node root)
    {
        Max = root.Value;

        FindMax(root.Left);
        FindMax(root.Right);

        return Max;
    }

    public static void FindMax(Node root)
    {
        if (root == null)
        {
            return;
        }

        Max = Max > root.Value ? Max : root.Value;

        FindMax(root.Left);
        FindMax(root.Right);
    }
}