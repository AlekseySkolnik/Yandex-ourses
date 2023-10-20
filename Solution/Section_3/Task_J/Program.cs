using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Пузырёк
namespace Task_J;
/*
Чтобы выбрать самый лучший алгоритм для решения задачи, Гоша продолжил изучать разные сортировки.
На очереди сортировка пузырьком — https://ru.wikipedia.org/wiki/Сортировка_пузырьком

Её алгоритм следующий (сортируем по неубыванию):

На каждой итерации проходим по массиву, поочередно сравнивая пары соседних элементов. 
Если элемент на позиции i больше элемента на позиции i + 1, меняем их местами.
После первой итерации самый большой элемент всплывёт в конце массива.
Проходим по массиву, выполняя указанные действия до тех пор, пока на очередной итерации не окажется,
что обмены больше не нужны, то есть массив уже отсортирован.
После не более чем n – 1 итераций выполнение алгоритма заканчивается, 
так как на каждой итерации хотя бы один элемент оказывается на правильной позиции.

Помогите Гоше написать код алгоритма.
Формат ввода
В первой строке на вход подаётся натуральное число n — длина массива, 2 ≤ n ≤ 1000.
Во второй строке через пробел записано n целых чисел.
Каждое из чисел по модулю не превосходит 1000.

Обратите внимание, что считывать нужно только 2 строки: значение n и входной массив.

Формат вывода
После каждого прохода по массиву, на котором какие-то элементы меняются местами, 
выводите его промежуточное состояние.
Таким образом, если сортировка завершена за k меняющих массив итераций, 
то надо вывести k строк по n чисел в каждой — элементы массива после каждой из итераций.
Если массив был изначально отсортирован, то просто выведите его.
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var length = short.Parse(_reader.ReadLine());
        var mas = ReadArray();

        BubbleSort(length, mas);
    }

    private static void BubbleSort(short length, short[] mas)
    {
        var IsChanged = true;

        for (short i = 0; i < length - 1; i++)
        {
            for (short j = 1; j < mas.Length; j++)
            {
                if (mas[j] < mas[j - 1])
                {
                    short max = mas[j - 1];
                    mas[j - 1] = mas[j];
                    mas[j] = max;
                    IsChanged = true;
                }
            }

            if (IsChanged)
            {
                Console.WriteLine($"{string.Join(' ', mas)}");
                IsChanged = false;
            }
        }
    }

    private static short[] ReadArray() =>
    _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(short.Parse)
        .ToArray();
}
