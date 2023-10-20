using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
// Приоритетная очередь. Binary heap. Просеивание вниз
namespace Task_K;
/*
Напишите функцию, совершающую просеивание вниз в куче на максимум. 
Гарантируется, что порядок элементов в куче может быть нарушен только элементом, от которого запускается просеивание.
Функция принимает в качестве аргументов массив, в котором хранятся элементы кучи, и индекс элемента, от которого надо сделать просеивание вниз.
Функция должна вернуть индекс, на котором элемент оказался после просеивания.
Также необходимо изменить порядок элементов в переданном в функцию массиве.
Индексация в массиве, содержащем кучу, начинается с единицы. 
Таким образом, сыновья вершины на позиции v это 2v и 2v+1. 
Обратите внимание, что нулевой элемент в передаваемом массиве фиктивный, вершина кучи соответствует 1-му элементу.

Формат ввода
Элементы кучи —– целые числа, лежащие в диапазоне от −109до 109. Все элементы кучи уникальны. 
Передаваемый в функцию индекс лежит в диапазоне от 1 до размера передаваемого массива. 
В куче содержится от 1 до 105 элементов.
 */

public class Task_K
{
    public static void Main(string[] args)
    {
        var sample = new List<int> { 9 };
        Solution.SiftDown(sample, 1);
    }
}

public class Solution
{
    public static int SiftDown(List<int> array, int idx)
    {
        if (idx >= array.Count)
        {
            return idx;
        }

        var left = 2 * idx;
        var right = 2 * idx + 1;

        // Если у текущего узла отсутствуют потомки. стоп 1
        if (left >= array.Count)
        {
            return idx;
        }

        int largest;

        // есть оба дочерних и правый больше левого
        if (right < array.Count && array[right] > array[left])
        {
            largest = right;
        }
        else
        {
            // правый меньше или его нет вообще
            largest = left;
        }

        // Если значение текущего узла больше, чем значение его потомков.
        if (array[idx] > array[largest])
        {
            return idx;
        }

        Swap(array, idx, largest);

        return SiftDown(array, largest);
    }

    private static void Swap(List<int> array, int item1, int item2)
    {
        var temp = array[item1];
        array[item1] = array[item2];
        array[item2] = temp;
    }
}