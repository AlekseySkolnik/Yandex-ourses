using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
// Пирамидальная сортировка
namespace Section_5
{
    /*
    Задание
    Тимофей решил организовать соревнование по спортивному программированию, чтобы найти талантливых стажёров. 
    Задачи подобраны, участники зарегистрированы, тесты написаны. 
    Осталось придумать, как в конце соревнования будет определяться победитель.
    Каждый участник имеет уникальный логин.
    Когда соревнование закончится, к нему будут привязаны два показателя: 
    количество решённых задач Pi и размер штрафа Fi.
    Штраф начисляется за неудачные попытки и время, затраченное на задачу.
    
    Тимофей решил сортировать таблицу результатов следующим образом:
    при сравнении двух участников выше будет идти тот, у которого решено больше задач.
    При равенстве числа решённых задач первым идёт участник с меньшим штрафом.
    Если же и штрафы совпадают, то первым будет тот, у которого логин идёт раньше в алфавитном
    (лексикографическом) порядке.
    
    Тимофей заказал толстовки для победителей и накануне поехал за ними в магазин.
    В своё отсутствие он поручил вам реализовать алгоритм сортировки кучей (англ. Heapsort) для таблицы результатов.

    Формат вывода
    Для отсортированного списка участников выведите по порядку их логины по одному в строке.
   */

    /*
    -- ПРИНЦИП РАБОТЫ --

Общий алгоритм такой:
    1. Создадим пустую бинарную неубывающую кучу (min-heap).
    2. Вставим в неё по одному все элементы массива, сохраняя свойства кучи. 
        При Ascending сортировке (от меньшего к большему) на вершине пирамиды должен оказаться самый маленький элемент. 
        При Descending сортировке (по убыванию) — на вершине был бы самый большой элемент.
        После вставки элемента в кучу нужно проделывать операцию обмена до тех пор, пока куча не станет упорядоченной.
        В худшем случае новый элемент встанет на вершину пирамиды. 
        Операцию восстановления свойств кучи при вставке нового элемента называют SiftUp.
        Так как при вставке мы на каждом уровне проводим только одно сравнение элемента, а куча у нас имеет высоту log⁡2N,
        то вставка происходит за O(logN).
    3. Будем извлекать из неё наиболее приоритетные элементы, удаляя их из кучи.
        Для того чтобы в ней не появились дыры, на место извлечённого элемента поставим последний элемент из кучи.
        Удаление элемента нарушило свойство упорядоченности бинарной кучи.
        Восстановим его при помощи функции SiftDown.
        Так как при удалении мы на каждом уровне дерева проводим не более двух сравнений элемента,
        а куча у нас имеет высоту log2N, то удаление происходит за O(logN).

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --


    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    Уже доказано в теории : O(N*logN).

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    Для описанной реализации алгоритма пирамидальной сортировки нужно выделить память под массив из n элементов. 
    То есть потребуется O(n) дополнительной памяти.

     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/24810/run-report/89609677
    /// </summary>
    public class Final_A
    {
        private static StreamReader _reader = new(Console.OpenStandardInput());

        public static void Main(string[] args)
        {
            var participantCount = int.Parse(_reader.ReadLine());
            var participants = GetParticipants(participantCount);
            var sortedParticipants = Heap.SortOrderBy(participants, OrderByEnum.Descending);
            PrintSortedParticipants(sortedParticipants);
        }

        private static void PrintSortedParticipants(Participant[] participants)
        {
            foreach (var p in participants)
            {
                Console.WriteLine(p.Login);
            }
        }

        private static Participant[] GetParticipants(int count)
        {
            var participants = new Participant[count];

            for (int i = 0; i < count; i++)
            {
                var inputs = ReadList();
                participants[i] = new Participant(inputs[0], uint.Parse(inputs[1]), uint.Parse(inputs[2]));
            }

            return participants;
        }

        private static List<string> ReadList() =>
            _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }

    public static class Heap
    {
        /// <summary>
        /// Сортирует массив по возрастанию/убыванию
        /// </summary>
        /// <param name="array"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static Participant[] SortOrderBy(Participant[] array, OrderByEnum orderBy = OrderByEnum.Ascending)
        {
            // Создадим пустую бинарную кучу.
            var heap = new List<Participant?>(array.Length + 1) { null };

            // Вставим в неё по одному все элементы массива, сохраняя свойства кучи.
            foreach (var participant in array)
            {
                HeapAdd(heap, participant, orderBy);
            }

            // Будем извлекать из неё наиболее приоритетные элементы, удаляя их из кучи.
            var sorted_array = new Participant[array.Length];
            var i = 0;

            while (heap.Count > 1)
            {
                sorted_array[i] = HeapPop(heap, orderBy);
                i++;
            }

            return sorted_array;
        }

        /// <summary>
        /// Добавляет элементы в кучу по возрастанию/убыванию
        /// </summary>
        /// <param name="heap"></param>
        /// <param name="key"></param>
        /// <param name="orderBy"></param>
        private static void HeapAdd(List<Participant?> heap, Participant key, OrderByEnum orderBy)
        {
            var index = heap.Count;
            heap.Add(key);
            SiftUp(heap, index, orderBy);
        }

        /// <summary>
        /// Просеивание элемента в куче вверх по возрастанию/убыванию
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private static int SiftUp(List<Participant?> array, int idx, OrderByEnum orderBy)
        {
            if (idx <= 1)
            {
                return idx;
            }

            var parentIdx = idx / 2;

            if (parentIdx >= 1 && Compare(array, orderBy, parentIdx, idx))
            {
                Swap(array, idx, parentIdx);
                return SiftUp(array, parentIdx, orderBy);
            }

            return idx;
        }

        ///// <summary>
        /// Отдает самый приоритетный элемент из кучи
        /// </summary>
        /// <param name="heap"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        private static Participant? HeapPop(List<Participant?> heap, OrderByEnum orderBy)
        {
            var result = heap[1];
            heap[1] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            SiftDown(heap, 1, orderBy);

            return result;
        }

        /// <summary>
        /// Просеивание элемента в куче вниз по возрастанию/убыванию
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static int SiftDown(List<Participant?> array, int idx, OrderByEnum orderBy)
        {
            if (idx >= array.Count)
            {
                return idx;
            }

            var left = 2 * idx;
            var right = 2 * idx + 1;

            if (left >= array.Count)
            {
                return idx;
            }

            int nextIdx = right < array.Count && Compare(array, orderBy, left, right) ? right : left;

            if (Compare(array, orderBy, nextIdx, idx))
            {
                return idx;
            }

            Swap(array, idx, nextIdx);

            return SiftDown(array, nextIdx, orderBy);
        }

        /// <summary>
        /// Сравнивает элементы по возрастанию/убыванию
        /// </summary>
        /// <param name="array"></param>
        /// <param name="orderBy"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static bool Compare(List<Participant?> array, OrderByEnum orderBy, int first, int second)
        {
            return orderBy == OrderByEnum.Ascending ? array[second] > array[first] : array[second] < array[first];
        }

        /// <summary>
        /// Меняет элементы местами
        /// </summary>
        /// <param name="array"></param>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        private static void Swap(List<Participant?> array, int item1, int item2)
        {
            var temp = array[item1];
            array[item1] = array[item2];
            array[item2] = temp;
        }
    }

    public enum OrderByEnum
    {
        Ascending,
        Descending
    }

    public class Participant : IComparable<Participant>
    {
        public string Login { get; private set; }
        public uint TaskCount { get; private set; }
        public uint FineAmount { get; private set; }

        public Participant(string login, uint taskCount, uint fineAmount)
        {
            Login = login;
            TaskCount = taskCount;
            FineAmount = fineAmount;
        }

        public int CompareTo(Participant? participant)
        {
            if (participant is null)
                throw new ArgumentException("Participant is null");

            if (TaskCount != participant.TaskCount)
            {
                return (int)(participant.TaskCount - TaskCount);
            }
            else if (FineAmount != participant.FineAmount)
            {
                return (int)(FineAmount - participant.FineAmount);
            }
            else
            {
                return Login.CompareTo(participant.Login);
            }
        }

        public static bool operator >(Participant p1, Participant p2)
        {
            return p1.CompareTo(p2) > 0;
        }

        public static bool operator <(Participant p1, Participant p2)
        {
            return p1.CompareTo(p2) < 0;
        }
    }
}