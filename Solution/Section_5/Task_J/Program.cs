using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Добавь узел
namespace Task_J;
/*
Дано BST. Надо вставить узел с заданным ключом. Ключи в дереве могут повторяться.
На вход функции подаётся корень корректного бинарного дерева поиска и ключ, который надо вставить в дерево.
Осуществите вставку этого ключа.
Если ключ уже есть в дереве, то его дубликаты уходят в правого сына.
Таким образом вид дерева после вставки определяется однозначно. 
Функция должна вернуть корень дерева после вставки вершины.
Ваше решение должно работать за O(h), где h –— высота дерева.
На рисунках ниже даны два примера вставки вершин в дерево.

Формат ввода
Ключи дерева – натуральные числа, не превосходящие 10^9. Число вершин в дереве не превосходит 10^5.
 */

public class Task_K
{
    private static StreamWriter _reader = new StreamWriter(Console.OpenStandardOutput());
    public static void Main(string[] args)
    {
        var node1 = new Node(7);
        var node2 = new Node(8)
        {
            Left = node1
        };

        var node3 = new Node(7)
        {
            Right = node2
        };

        var newHead = Solution.Insert(node3, 6);
        Console.WriteLine(newHead == node3);
        Console.WriteLine(newHead.Left.Value == 6);
    }
}

public class Solution
{
    public static Node Insert(Node root, int key)
    {
        if (key < root.Value)
        {
            if (root.Left == null)
            {
                root.Left = new Node(key);
            }
            else
            {
                Insert(root.Left, key);
            }
        }
        else
        {
            if (root.Right == null)
            {
                root.Right = new Node(key);
            }
            else
            {
                Insert(root.Right, key);
            }
        }

        return root;
    }
}