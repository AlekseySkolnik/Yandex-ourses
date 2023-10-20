using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
// Приоритетная очередь. Binary heap. Просеивание вверх
namespace Task_M;
/*
Напишите функцию, совершающую просеивание вверх в куче на максимум. 
Гарантируется, что порядок элементов в куче может быть нарушен только элементом,
от которого запускается просеивание.
Функция принимает в качестве аргументов массив, в котором хранятся элементы кучи, 
и индекс элемента, от которого надо сделать просеивание вверх. 
Функция должна вернуть индекс, на котором элемент оказался после просеивания. 
Также необходимо изменить порядок элементов в переданном в функцию массиве.
 */

public class Task_K
{
    public static void Main(string[] args)
    {
        var sample = new List<int> { -1, 12, 6, 8, 3, 15, 7 };
        Console.WriteLine(Solution.SiftUp(sample, 5) == 1);
    }
}

public class Solution
{
    public static int SiftUp(List<int> array, int idx)
    {
        if (idx >= array.Count || idx <= 1)
        {
            return idx;
        }

        var parentIdx = idx / 2;

        if (parentIdx >= 1 && array[parentIdx] < array[idx])
        {
            Swap(array, idx, parentIdx);
            return SiftUp(array, parentIdx);
        }

        return idx;
    }

    private static void Swap(List<int> array, int item1, int item2)
    {
        var temp = array[item1];
        array[item1] = array[item2];
        array[item2] = temp;
    }
}