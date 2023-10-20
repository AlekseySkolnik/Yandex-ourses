using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task_C;

namespace Section_2;

/*
Вася размышляет, что ему можно не делать из того списка дел, который он составил.
Но, кажется, все пункты очень важные!
Вася решает загадать число и удалить дело, которое идёт под этим номером. 
Список дел представлен в виде односвязного списка. 
Напишите функцию solution, которая принимает на вход голову списка
и номер удаляемого дела и возвращает голову обновлённого списка.

Формат вывода
Верните голову списка, в котором удален нужный элемент.
 * */
public class Task_C
{
    public static void Main(string[] args)
    {
        var node3 = new Node<string>("node3", null);
        var node2 = new Node<string>("node2", node3);
        var node1 = new Node<string>("node1", node2);
        var node0 = new Node<string>("node0", node1);
        var newHead = Solution<string>.Solve(node0, 1);
        // result is : node0 -> node2 -> node3

        Solution<string>.PrintNodes(newHead);
    }
}

public class Solution<TValue>
{
    public static Node<TValue> Solve(Node<TValue> head, int idx)
    {
        if (idx == 0)
        {
            var newHead = head.Next;
            head.Next = null;
            return newHead;
        }

        var nodes = GetNodes(head, idx - 1);
        var previous = nodes[0];
        var deletion = nodes[1];

        previous.Next = deletion.Next;
        deletion.Next = null;

        return head;
    }

    private static Node<TValue>[] GetNodes(Node<TValue> node, int idx)
    {
        if (idx == 0)
        {
            return new Node<TValue>[] { node, node.Next };
        }

        var previous = node;

        while (idx > 0)
        {
            previous = previous.Next;
            idx--;
        }

        return new Node<TValue>[] { previous, previous.Next };
    }

    public static void PrintNodes(Node<TValue> head)
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