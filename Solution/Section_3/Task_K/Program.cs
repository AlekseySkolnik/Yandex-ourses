using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
// Сортировка слиянием
namespace Section_3;

/*
Мама Васи хочет знать, что сын планирует делать и когда. 
Напишите функцию solution, определяющую индекс первого вхождения передаваемого ей на вход значения в связном списке,
если значение присутствует.

Формат вывода
Функция возвращает индекс первого вхождения искомого элемента в список(индексация начинается с нуля).
Если элемент не найден, нужно вернуть -1.
*/

public class Task_K
{
    public static void Main(string[] args)
    {
        var a = new List<int> { 1, 4, 9, 2, 10, 11 };
        var b = Solution.Merge(a, 0, 3, 6);
        var expectedMergeResult = new List<int> { 1, 2, 4, 9, 10, 11 };
        Console.WriteLine(b.SequenceEqual(expectedMergeResult));
        var c = new List<int> { 1, 4, 2, 10, 1, 2 };
        Solution.MergeSort(c, 0, 6);
        var expectedMergeSortResult = new List<int> { 1, 1, 2, 2, 4, 10 };
        Console.WriteLine(c.SequenceEqual(expectedMergeSortResult));
    }
}

public class Solution
{
    /// <summary>
    /// Функция merge_sort принимает некоторый подмассив, который нужно отсортировать.
    /// Подмассив задаётся полуинтервалом — его началом и концом. 
    /// Функция должна отсортировать передаваемый в неё подмассив, она ничего не возвращает.
    /// Функция merge_sort разбивает полуинтервал на две половинки и рекурсивно вызывает сортировку 
    /// отдельно для каждой. Затем два отсортированных массива сливаются в один с помощью merge.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public static void MergeSort(List<int> array, int left, int right)
    {
        var mid = (right + left) / 2;

        if (right - left <= 1) // базовый случай рекурсии
            return;

        // запускаем сортировку рекурсивно на левой половине
        MergeSort(array, left, mid);
        // запускаем сортировку рекурсивно на правой половине
        MergeSort(array, mid, right);
        // сливаем результаты
        array = Merge(array, left, mid, right);
    }


    /// <summary>
    /// Функция merge принимает два отсортированных массива, сливает их в один отсортированный массив и возвращает его. 
    /// Если требуемая сигнатура имеет вид merge(array, left, mid, right), то первый массив задаётся полуинтервалом 
    /// [left,mid) массива array, а второй – полуинтервалом [mid,right) массива array.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="left"></param>
    /// <param name="mid"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static List<int> Merge(List<int> array, int left, int mid, int right)
    {
        // TODO подумать, как убрать копирование
        var leftArr = new int[mid - left];
        var righArr = new int[right - mid];
        array.CopyTo(left, leftArr, 0, mid - left);
        array.CopyTo(mid, righArr, 0, right - mid);
        // -------------------------------------------

        var leftArrLength = mid - left;
        var righArrLength = right - mid;

        var result = new int[leftArrLength + righArrLength];
        int l = 0, r = 0, k = 0;

        while (l < leftArrLength && r < righArrLength)
        {
            if (leftArr[l] <= righArr[r])
            {
                result[k] = leftArr[l];
                l++;
            }
            else
            {
                result[k] = righArr[r];
                r++;
            }

            k++;
        }

        while (l < leftArrLength)
        {
            result[k] = leftArr[l];
            l++;
            k++;
        }

        while (r < righArrLength)
        {
            result[k] = righArr[r];
            r++;
            k++;
        }

        for (int i = 0; i < result.Length; i++)
        {
            array[left + i] = result[i];
        }

        return array;
    }
}