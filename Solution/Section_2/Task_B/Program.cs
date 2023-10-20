using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task_B;

namespace Section_2;

/*
Васе нужно распечатать свой список дел на сегодня. 
Помогите ему: напишите функцию, которая печатает все его дела.
Известно, что дел у Васи не больше 5000.
 */
public class Task_B
{
    public static void Main(string[] args)
    {
        var node3 = new Node<string>("node3", null);
        var node2 = new Node<string>("node2 \r\n node2-2-2-2", node3);
        var node1 = new Node<string>("node1", node2);
        var node0 = new Node<string>("node0", node1);
        Solution<string>.Solve(node0);
    }
}

public class Solution<TValue>
{
    public static void Solve(Node<TValue> head)
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