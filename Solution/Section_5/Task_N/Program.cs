using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
// Разбиение дерева
namespace Task_N;
/*
Дано бинарное дерево поиска, в котором хранятся целые числа. От этого дерева надо отделить 
k самых маленьких элементов. Реализуйте функцию, которая принимает корень дерева и число k,
а возвращает два BST — в первом k наименьших элементов из исходного дерева, 
а во втором — оставшиеся вершины BST. В вершинах дерева уже записаны корректные размеры поддеревьев 
(точное название поля смотрите в заготовках кода). 
После разбиения размеры должны остаться корректными — вам придётся пересчитывать их на ходу.
Ваше решение должно иметь асимптотику O(h), где h — высота исходного дерева. 

Формат ввода
Числа, записанные в вершинах дерева, лежат в диапазоне от 0 до 109. 
Дерево не содержит одинаковых ключей.
Число вершин в дереве не превосходит 105.
*/

public class Task_K
{
    public static void Main(string[] args)
    {
        var node1 = new Node(3, 1);
        var node2 = new Node(2, 2)
        {
            Right = node1
        };

        var node3 = new Node(8, 1);
        var node4 = new Node(11, 1);
        var node5 = new Node(10, 3)
        {
            Left = node3,
            Right = node4
        };

        var node6 = new Node(5, 6)
        {
            Left = node2,
            Right = node5
        };

        var res = Solution.Split(node6, 4);

        Console.WriteLine(res[0].Size == 4);
        Console.WriteLine(res[1].Size == 2);
    }
}

public class Solution
{
    public static List<Node> Split(Node root, int k)
    {
        if (root == null)
        {
            return new List<Node>() { null, null };
        }

        var ls = root.Left != null ? root.Left.Size : 0;
        var rs = root.Right != null ? root.Right.Size : 0;

        Node ln = null, rn = null;

        if (ls + 1 > k)
        {
            // Что должно происходить при спуске рекурсии в левое поддерево?
            var nodes = Split(root.Left, k);
            ln = nodes[0];
            rn = nodes[1];

            var rnSize = rn != null ? rn.Size : 0;
            root.Size = root.Size - ls + rnSize;
            root.Left = rn;
            return new List<Node>() { ln, root };
        }

        k -= 1 + (root.Left != null ? root.Left.Size : 0);
        var rightNodes = Split(root.Right, k);
        ln = rightNodes[0];
        rn = rightNodes[1];
        var lnSize = ln != null ? ln.Size : 0;
        root.Size = root.Size - rs + lnSize;
        root.Right = ln;
        return new List<Node>() { root, rn };
    }
}