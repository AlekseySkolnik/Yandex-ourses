namespace Algorithms;

internal class Program
{
    static void Main(string[] args)
    {
        GenBinary(3, "");
        Sort.Demo_InsertionSort();
        Sort.Demo_InsertionSortByKey();
        Sort.Demo_InsertionSortByComparator();

        MergeSort.Demo_MergeSort();
        QuickSort.Demo_QuickSort();
    }


    static void GenBinary(int n, string prefix)
    {
        if (n == 0)
        {
            Console.WriteLine(prefix);
        }
        else
        {
            GenBinary(n - 1, prefix + "0");
            GenBinary(n - 1, prefix + "1");
        }
    }


    public static class Sort
    {

        static List<int> digit_lengths = new List<int> { 4, 4, 3, 3, 6, 4, 5, 4, 6, 6 }; // длины слов «ноль», «один»,...

        static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int item_to_insert = array[i];
                int j = i;
                while (j > 0 && item_to_insert < array[j - 1])
                {
                    array[j] = array[j - 1];
                    j--;
                }
                array[j] = item_to_insert;
                Console.WriteLine($"step {i}, sorted {i + 1} elements: {string.Join(" ", array.ToList().GetRange(0, i + 1))}");
            }
        }

        public static void Demo_InsertionSort()
        {
            var array = new List<int> { 5, 2, 4, 6, 1, 3 };
            InsertionSort(array.ToArray());
        }

        // воспользуемся уже знакомой сортировкой вставками
        static void InsertionSortByKey(List<int> array, Func<int, int> key)
        {
            for (int i = 1; i < array.Count; i++)
            {
                int item_to_insert = array[i];
                int j = i;
                // заменим сравнение item_to_insert < array[j-1] на сравнение ключей
                while (j > 0 && key(item_to_insert) < key(array[j - 1]))
                {
                    array[j] = array[j - 1];
                    j--;
                }
                array[j] = item_to_insert;
            }
        }

        static int CardStrength(int card) // ключ сравнения
        {
            return digit_lengths[card];
        }

        public static void Demo_InsertionSortByKey()
        {
            List<int> cards = new List<int> { 3, 7, 9, 2, 3 };

            InsertionSortByKey(cards, CardStrength);

            foreach (int card in cards)
            {
                Console.Write(card + " ");
            }

            Console.WriteLine();
        }

        // воспользуемся уже знакомой сортировкой вставками
        static void InsertionSortByComparator(List<int> array, Func<int, int, bool> less)
        {
            for (int i = 1; i < array.Count; i++)
            {
                int item_to_insert = array[i];
                int j = i;
                // заменим сравнение item_to_insert < array[j-1] на компаратор less
                while (j > 0 && less(item_to_insert, array[j - 1]))
                {
                    array[j] = array[j - 1];
                    j--;
                }
                array[j] = item_to_insert;
            }
        }

        static bool IsFirstCardWeaker(int card_1, int card_2)
        { // функция-компаратор
            return digit_lengths[card_1] < digit_lengths[card_2];
        }

        public static void Demo_InsertionSortByComparator()
        {
            List<int> cards = new List<int> { 3, 7, 9, 2, 3 };
            InsertionSortByComparator(cards, IsFirstCardWeaker);
            foreach (int card in cards)
            {
                Console.Write(card + " ");
            }
            Console.WriteLine();
        }

        static int[] key_for_card(int card)
        {
            return new int[] { -digit_lengths[card], card };
        }

        public static void Demo_LiksokpgraphSort()
        {
            int[] cards = { 2, 3, 7 };

            Array.Sort(cards, (a, b) =>
            {
                int[] keyA = key_for_card(a);
                int[] keyB = key_for_card(b);
                return keyA[0] == keyB[0] ? keyA[1] - keyB[1] : keyA[0] - keyB[0];
            });

            Console.WriteLine(string.Join(" ", cards));
        }

        /// <summary>
        /// Сортировка подсчетом
        /// </summary>
        /// <param name="array"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] CountingSort(int[] array, int k)
        {
            int[] countedValues = new int[k];
            foreach (int value in array)
            {
                countedValues[value]++;
            }

            int index = 0;
            for (int value = 0; value < k; value++)
            {
                for (int amount = 0; amount < countedValues[value]; amount++)
                {
                    array[index] = value;
                    index++;
                }
            }
            return array;
        }

    }

    public static class MergeSort
    {
        public static int[] Mergesort(int[] array)
        {
            if (array.Length == 1) // базовый случай рекурсии
                return array;

            // запускаем сортировку рекурсивно на левой половине
            int[] left = Mergesort(array.Take(array.Length / 2).ToArray());

            // запускаем сортировку рекурсивно на правой половине
            int[] right = Mergesort(array.Skip(array.Length / 2).ToArray());

            // заводим массив для результата сортировки
            int[] result = new int[array.Length];

            // сливаем результаты
            int l = 0, r = 0, k = 0;
            while (l < left.Length && r < right.Length)
            {
                // выбираем, из какого массива забрать минимальный элемент
                if (left[l] <= right[r])
                {
                    result[k] = left[l];
                    l++;
                }
                else
                {
                    result[k] = right[r];
                    r++;
                }
                k++;
            }

            // Если один массив закончился раньше, чем второй, то
            // переносим оставшиеся элементы второго массива в результирующий
            while (l < left.Length)
            {
                result[k] = left[l]; // перенеси оставшиеся элементы left в result
                l++;
                k++;
            }
            while (r < right.Length)
            {
                result[k] = right[r]; // перенеси оставшиеся элементы right в result
                r++;
                k++;
            }

            return result;
        }

        public static void Demo_MergeSort()
        {
            int[] cards = { 1, 4, 2, 10, 1, 2 };
            Console.WriteLine(string.Join(" ", cards));
            var sorted = Mergesort(cards);
            Console.WriteLine(string.Join(" ", sorted));
        }
    }

    public static class QuickSort
    {
        public static void Demo_QuickSort()
        {
            List<int> arr = new List<int> { 5, 2, 8, 4, 7, 1, 3, 6 };
            var result = Quicksort(arr);

            foreach (int x in result)
            {
                Console.Write(x + " ");
            }

            Console.WriteLine();
        }

        static List<int> Quicksort(List<int> array)
        {
            if (array.Count < 2)
            {
                return array; // базовый случай рекурсии !!!
            }

            var rand = new Random();
            int pivot = array[rand.Next(array.Count)];
            var (left, center, right) = Partition(array, pivot);

            var sortedLeft = Quicksort(left);
            var sortedRight = Quicksort(right);

            var result = new List<int>();
            result.AddRange(sortedLeft);
            result.AddRange(center);
            result.AddRange(sortedRight);

            return result;
        }

        static (List<int>, List<int>, List<int>) Partition(List<int> array, int pivot)
        {
            List<int> left = new(), center = new(), right = new();

            foreach (int x in array)
            {
                if (x < pivot)
                {
                    left.Add(x);
                }
                else if (x == pivot)
                {
                    center.Add(x);
                }
                else
                {
                    right.Add(x);
                }
            }

            return (left, center, right);
        }
    }
}
