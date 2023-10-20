/*
using System.Reflection.Metadata.Ecma335;

namespace Algorithms;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    void insert_node(Node root, int key)
    {
        if (key < root.Value)
        {
            if (root.Left == null)
            {
                root.Left = new Node(key);
            }
            else
            {
                insert_node(root.Left, key);
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
                insert_node(root.Right, key);
            }
        }
    }

    private Node? FindNode(Node root, int value)
    {
        if (root == null)
        {
            return null;
        }
        if (value < root.Value)
        {
            return FindNode(root.Left, value);
        }
        if (value == root.Value)
        {
            return root;
        }
        if (value > root.Value)
        {
            return FindNode(root.Right, value);
        }

        return null;
    }

    /// <summary>
    /// Прямой обход дерева
    /// </summary>
    /// <param name="vertex"></param>
    void PrintForward(Node vertex)
    {
        Console.WriteLine(vertex.Value);
        foreach (Node child in vertex.Children)
        {
            PrintForward(child);
        }
    }

    /// <summary>
    /// Обрытнй обход дереве
    /// </summary>
    /// <param name="vertex"></param>
    void PrintReversed(Node vertex)
    {
        foreach (var child in vertex.children)
        {
            PrintReversed(child);
        }
        Console.WriteLine(vertex.value);
    }

    /// <summary>
    /// Центрированный обход
    /// </summary>
    /// <param name="vertex"></param>
    void print_LMR(Node vertex)
    {
        if (vertex.Left != null)
        {
            print_LMR(vertex.Left);
        }
        Console.WriteLine(vertex.Value);
        if (vertex.Right != null)
        {
            print_LMR(vertex.Right);
        }
    }

    // Запишем алгоритм добавления элемента в кучу псевдокодом:
    void AddToHeap(int[] heap, int key)
    {
        var index = heap.size + 1;
        heap[index] = key;
        SiftUp(heap, index);
    }

    private void SiftUp(int[] heap, int index)
    {
        if (index == 1)
        {
            return; // завершить работу
        }

        var parent_index = index / 2; // (целочисленное деление)

        if (heap[parent_index] < heap[index])
        {
            var temp = heap[parent_index];
            heap[parent_index] = heap[index];
            heap[index] = temp;

            SiftUp(heap, parent_index);
        }
    }
}

public class Node
{
    public int Value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

class BalanceNode
{
    public int key;
    public int height;
    public Node left;
    public Node right;

    public BalanceNode(int k, int h = 1, Node l = null, Node r = null)
    {
        key = k;
        height = h;
        left = l;
        right = r;
    }
}
*/