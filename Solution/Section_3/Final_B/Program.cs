using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Section_3
{
    /*
    Задание
    Каждый участник имеет уникальный логин. Когда соревнование закончится, к нему будут привязаны два показателя:
    количество решённых задач Pi и размер штрафа Fi. Штраф начисляется за неудачные попытки и время, затраченное на задачу.
    Тимофей решил сортировать таблицу результатов следующим образом:
    при сравнении двух участников выше будет идти тот, у которого решено больше задач.
    При равенстве числа решённых задач первым идёт участник с меньшим штрафом. 
    Если же и штрафы совпадают, то первым будет тот, у которого логин идёт раньше в алфавитном (лексикографическом) порядке.
    Реализация сортировки не может потреблять O(n) дополнительной памяти для промежуточных данных (называется "in-place").
     */

    /*
    -- ПРИНЦИП РАБОТЫ --
    Как работает in-place quick sort
    Как и в случае обычной быстрой сортировки, которая использует дополнительную память,
    необходимо выбрать опорный элемент (англ. pivot), а затем переупорядочить массив. 
    Сделаем так, чтобы сначала шли элементы, не превосходящие опорного, а затем —– большие опорного.
    Затем сортировка вызывается рекурсивно для двух полученных частей.
    Именно на этапе разделения элементов на группы в обычном алгоритме используется дополнительная память. 
    Теперь разберёмся, как реализовать этот шаг in-place.
    Пусть мы как-то выбрали опорный элемент. 
    Заведём два указателя left и right, которые изначально будут указывать на левый и правый концы отрезка соответственно.
    Затем будем двигать левый указатель вправо до тех пор, пока он указывает на элемент, меньший опорного.
    Аналогично двигаем правый указатель влево, пока он стоит на элементе, превосходящем опорный. 
    В итоге окажется, что что левее от left все элементы точно принадлежат первой группе, а правее от right — второй. 
    Элементы, на которых стоят указатели, нарушают порядок. 
    Поменяем их местами (в большинстве языков программирования используется функция swap()) 
    и продвинем указатели на следующие элементы. 
    Будем повторять это действие до тех пор, пока left и right не столкнутся.

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --


    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    O(NlogN)

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    O(1). Память выделяем только под хранение пивота и для свапа элементов
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/23815/run-report/89070530/
    /// </summary>
    public class Final_B
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static Participant[] _array;

        public static void Main(string[] args)
        {
            var participantCount = int.Parse(_reader.ReadLine());
            _array = GetParticipants(participantCount);

            if (_array.Length > 2)
            {
                QsortInPlace(0, _array.Length - 1);
            }
            else
            {
                SwapByPivot(0, _array.Length - 1, 0);
            }

            PrintSortedParticipants();
        }

        /// <summary>
        /// Разбивает массив на 2 подотрезка, в правом элементы больше пивота, в левом нет.
        /// Затем сортировка вызывается рекурсивно для двух полученных частей.
        /// </summary>
        static void QsortInPlace(int start, int end)
        {
            var rand = new Random();
            var pivotIndex = rand.Next(start, end);

            if (end - start <= 1)
            {
                // базовый случай рекурсии - в подмассив на сортировку не более 2х элементов
                SwapByPivot(start, end, pivotIndex);
                return;
            }

            var (left, right) = SwapByPivot(start, end, pivotIndex);

            QsortInPlace(start, right); // сортируем левый подмассив
            QsortInPlace(left, end); // сортируем правый подмассив
        }

        /// <summary>
        /// Сортирует массив так, что левее указателя left оказываются элементы, не превосходящие pivot,
        /// а правее указателя right элементы больше pivot.
        /// </summary>
        static (int left, int right) SwapByPivot(int left, int right, int pivotIndex)
        {
            var pivot = _array[pivotIndex];

            while (left < right)
            {
                if (_array[left] < pivot)
                {
                    left++;
                    continue;
                }

                if (_array[right] > pivot)
                {
                    right--;
                    continue;
                }

                // Swap
                (_array[right], _array[left]) = (_array[left], _array[right]);
                left++;
                right--;
            }

            return (left, right);
        }

        private static void PrintSortedParticipants()
        {
            foreach (var p in _array)
            {
                Console.WriteLine(p.Login);
            }
        }

        private static Participant[] GetParticipants(int participantCount)
        {
            var participants = new Participant[participantCount];

            for (int i = 0; i < participantCount; i++)
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

    public class Participant : IComparable<Participant>
    {
        //уникальным логином (строкой из маленьких латинских букв длиной не более 20)
        public string Login { get; private set; }

        // Число решённых задач Pi
        public uint TaskCount { get; private set; }

        // Штраф
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