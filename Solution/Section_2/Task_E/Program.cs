using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task_E;

namespace Section_2;

/*
Вася решил запутать маму – делать дела в обратном порядке.
Список его дел теперь хранится в двусвязном списке. 
Напишите функцию, которая вернёт список в обратном порядке.

Формат вывода
Функция должна вернуть голову развернутого списка.
 * */

public class Task_E
{
    public static void Main(string[] args)
    {
        var node3 = new Node<string>("node3", null, null);
        var node2 = new Node<string>("node2", node3, null);
        var node1 = new Node<string>("node1", node2, null);
        var node0 = new Node<string>("node0", node1, null);
        var newNode = Solution.Solve(node0);
        /*
        result is :
        newNode == node3
        node3.next == node2

        node2.next == node1
        node2.prev == node3
     
        node1.next == node0
        node1.prev == node2
       
        node0.prev == node1
        */

        Solution.PrintNodes(newNode);
    }
}

public class Solution
{
    public static Node<string> Solve(Node<string> head)
    {
        var prev = head.Next;
        Node<string> newNode = new Node<string>(head.Value, next: null, prev: head.Next);

        while (prev != null)
        {
            newNode = new Node<string>(prev.Value, next: newNode, prev: prev.Next);
            prev = prev.Next;
        }

        return newNode;
    }

    public static void PrintNodes(Node<string> head)
    {
        Console.WriteLine(head.Value);
        var current = head.Next;

        while (current != null)
        {
            Console.WriteLine(current.Value);
            current = current.Next;
        }
    }
}