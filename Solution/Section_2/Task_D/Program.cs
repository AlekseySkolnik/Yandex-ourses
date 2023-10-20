using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task_D;

namespace Section_2;

/*
Мама Васи хочет знать, что сын планирует делать и когда. 
Напишите функцию solution, определяющую индекс первого вхождения передаваемого ей на вход значения в связном списке,
если значение присутствует.

Формат вывода
Функция возвращает индекс первого вхождения искомого элемента в список(индексация начинается с нуля).
Если элемент не найден, нужно вернуть -1.
 * */

public class Task_D
{
    public static void Main(string[] args)
    {
        var node3 = new Node<string>("node3", null);
        var node2 = new Node<string>("node2", node3);
        var node1 = new Node<string>("node1", node2);
        var node0 = new Node<string>("node0", node1);
        var idx = Solution.Solve(node0, "node2");
        // result is : idx == 2
    }
}

public class Solution
{
    public static int Solve(Node<string> head, string item)
    {
        if (head.Value == item)
        {
            return 0;
        }

        var current = head.Next;
        var count = 1;

        while (current != null)
        {
            if (current.Value == item)
            {
                return count;
            }

            current = current.Next;
            count++;
        }

        return -1;
    }
}