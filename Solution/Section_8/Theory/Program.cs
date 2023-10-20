using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Theory;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var t = 35;
        var s = (char)t;

        char b = (char)((int)'a' + 1);

        //   Insert("abcdefghigk", 5, "123");

        PrefixFunc("тет-а-тет");
        LogOptimizedPrefixFunc("тет-а-тет");
        OptimizedPrefixFunc("sip#mississippi");
       
        Console.WriteLine(s);
    }

    // Вставить строку substring в строку string перед позицией index.
    static string Insert(string str, int index, string substr)
    {
        int length = str.Length;
        int shift = substr.Length;
        if (index > length)
        {
            // index == length - край строки
            throw new ArgumentException("Нет такой позиции");
        }
        str = str.PadRight(length + shift);
        if (length > 0)
        {
            // Если length == 0, делать сдвиг нет смысла.
            // Кроме того, не следует в вычислениях писать (length - 1),
            // не проверив, что индекс не ноль.
            // В некоторых языках длина представляется беззнаковым целым числом,
            // в таком случае (length - 1) будет равен не -1, а числу MAX_INT,
            // и цикл станет некорректным. Мы этого избегаем.
            for (int i = length - 1; i >= index; i--)
            {
                str = str[..(i + shift)] + str[i] + str[(i + shift + 1)..];
            }
        }
        for (int i = 0; i < shift; i++)
        {
            str = str[..(index + i)] + substr[i] + str[(index + i + 1)..];
        }

        return str;
    }

    // Поиск шаблона в строке. Наивный алгоритм

    // Найти первое вхождение подстроки pattern в строке text, находящееся на позиции не раньше start.
    public static int Find(string text, string pattern, int start = 0)
    {
        if (text.Length < pattern.Length)
        {
            return -1; // Длинный шаблон не может содержаться в короткой строке.
        }

        for (int pos = start; pos <= text.Length - pattern.Length; pos++)
        {
            bool match = true;

            // Проверяем, не совпадёт ли шаблон, сдвинутый на позицию pos, с соответствующим участком строки.
            for (int offset = 0; offset < pattern.Length; offset++)
            {
                if (text[pos + offset] != pattern[offset])
                {
                    // Одного несовпадения достаточно, чтобы не проверять дальше текущее расположение шаблона.
                    match = false;
                    break;
                }
            }

            // Как только нашлось совпадение шаблона, возвращаем его. Это первое вхождение шаблона в строку.
            if (match == true)
            {
                return pos;
            }

            // Если совпадение не нашлось, цикл перейдёт к проверке следующей позиции.
        }

        // Числом -1 часто маркируют, что подстрока не была найдена, поскольку в строке нет позиции -1.        
        return -1;
    }

    // Поиск всех вхождений шаблона в строку
    public static List<int> FindAll(string text, string pattern)
    {
        List<int> occurrences = new List<int>();
        int start = 0; // Начнём поиск с начала строки. Найдём первое вхождение, если оно есть.
        int pos;

        while ((pos = Find(text, pattern, start)) != -1)
        {
            occurrences.Add(pos); // Сохраним вхождение в список.
            start = pos + 1;      // И продолжим поиск, начиная с позиции, 
                                  // следующей за только что найденной.
        }

        return occurrences;
    }

    // Наивный алгоритм вычисления префикс-функции
    public static int[] PrefixFunc(string s)
    {
        var N = s.Length;
        var π = new int[N];

        for (int i = 1; i <= N; i++)
        {
            var substring = s[..i];

            for (int k = i - 1; k >= 0; k--)
            {
                var prefix = substring[..k];
                var suffix = substring.Substring(i - k, k);

                if (prefix == suffix)
                {
                    π[i - 1] = k;
                    break;
                }
            }
        }

        return π; // π[i] — это длина наибольшего самоперекрытия подстроки s[0, i + 1), где 0⩽i <∣s∣.
    }

    // Эффективный алгоритм вычисления префикс-функции
    public static int[] OptimizedPrefixFunc(string s)
    {
        var N = s.Length;
        var p = new int[N];

        for (int i = 1; i < N; i++)
        {
            int k = p[i - 1];

            while (k > 0 && s[k] != s[i])
            {
                k = p[k - 1];
            }

            if (s[k] == s[i])
            {
                k++;
            }

            p[i] = k;
        }

        Console.WriteLine(string.Join(' ', p));

        return p;
    }

    public static int[] LogOptimizedPrefixFunc(string s)
    {
        var N = s.Length;
        var π = new int[N];

        Console.WriteLine(s);

        for (int i = 1; i < N; i++)
        {
            int k = π[i - 1];

            Console.WriteLine($"i = {i}, '{s[0..i]}', k = п[i - 1] = {k}");

            while (k > 0 && s[k] != s[i])
            {
                k = π[k - 1];
                Console.WriteLine($"i = {i}, '{s[0..i]}', while,  k = п[k - 1] = {k}");
            }

            if (s[k] == s[i])
            {
                Console.WriteLine($"i = {i}, '{s[0..i]}', s[{k}] = '{s[k]}' == s[{i}] = '{s[i]}', k -> k++");
                k++;
            }
            else
            {
                Console.WriteLine($"i = {i}, '{s[0..i]}', s[{k}] = '{s[k]}' != s[{i}] = '{s[i]}'");
            }

            π[i] = k;

            Console.WriteLine($"i = {i}, '{s[0..i]}', п[i]=п[{i}] = k = {k}");
        }

        return π;
    }

    public static List<int> MemoryFindSubstring(string pattern, string text)
    {
        var result = new List<int>();
        string s = pattern + "#" + text;
        var prefix = new int[pattern.Length + 1]; // хранить префикс-функцию только от первых ∣p∣ символов комбинированной строки
        prefix[0] = 0;

        int prefix_prev = 0; // и  префикс-функцию для последнего обрабатываемого элемента. 

        // В таком случае построение префикс-функции и поиск её максимального значения будут совмещены.
        for (int i = 1; i < s.Length; i++)
        {
            var k = prefix_prev;

            while (k > 0 && s[k] != s[i])
            {
                k = prefix[k - 1];
            }

            if (s[k] == s[i])
            {
                k++;
            }

            if (i <= pattern.Length)
            {
                prefix[i] = k;
            }

            prefix_prev = k;

            if (k == pattern.Length)
            {
                result.Add(i - 2 * pattern.Length);
            }
        }

        return result;
    }





    // Поиск шаблона в строке. Наивный алгоритм

    // Найти первое вхождение подстроки pattern в строке text, находящееся на позиции не раньше start.
    public static int FindWithTrie(string text, string pattern, int start = 0)
    {
        if (text.Length < pattern.Length)
        {
            return -1; // Длинный шаблон не может содержаться в короткой строке.
        }

        for (int pos = start; pos <= text.Length - pattern.Length; pos++)
        {
            bool match = true;

            // Проверяем, не совпадёт ли шаблон, сдвинутый на позицию pos, с соответствующим участком строки.
            for (int offset = 0; offset < pattern.Length; offset++)
            {
                if (text[pos + offset] != pattern[offset])
                {
                    // Одного несовпадения достаточно, чтобы не проверять дальше текущее расположение шаблона.
                    match = false;
                    break;
                }
            }

            // Как только нашлось совпадение шаблона, возвращаем его. Это первое вхождение шаблона в строку.
            if (match == true)
            {
                return pos;
            }

            // Если совпадение не нашлось, цикл перейдёт к проверке следующей позиции.
        }

        // Числом -1 часто маркируют, что подстрока не была найдена, поскольку в строке нет позиции -1.        
        return -1;
    }

    // искать в тексте не одну строку, а сразу несколько слов или подстрок. 
    private static int FindAny(string text, Node root)
    {
        for (int pos = 0; pos < text.Length; pos++)
        {
            // Начинаем с корня бора
            var currentNode = root;
            var offset = 0;
            var mismatchNotFound = true; // Расхождений с шаблоном пока не найдено.

            while (mismatchNotFound && (pos + offset) < text.Length)
            {
                var symbol = text[pos + offset];
                var nextNode = currentNode.GetNext(symbol);

                // если из current_node есть переход по symbol
                if (nextNode != null)
                {
                    // Сдвинуться на следующий символ
                    currentNode = nextNode;
                    if (currentNode.IsTerminal)
                    {
                        return pos;
                    }

                    offset++;
                }
                else // Подходящее ребро отсутствует.
                {
                    //  Найдено разночтение, мы должны завершить сканирование шаблона на текущей стартовой позиции.
                    //  Внутренний цикл прервётся. Работа на этом не заканчивается: будет взята следующая стартовая позиция.
                    mismatchNotFound = false;
                }
            }
        }

        // Ни на одной стартовой позиции мы не дошли до терминального узла, значит, шаблон не найден.
        return -1;
    }

    private static Node? FindNode(Node root, string word)
    {
        var currentNode = root;

        for (int i = 0; i < word.Length; i++)
        {
            var next = currentNode.GetNext(word[i]);

            if (next == null)
            {
                return null;
            }

            currentNode = next;
        }

        return currentNode;
    }

    private static void AddNode(Node root, string word)
    {
        var currentNode = root;

        for (int i = 0; i < word.Length; i++)
        {
            var nextNode = currentNode.GetNext(word[i]);

            if (nextNode == null)
            {
                nextNode = new Node(word[i], i == word.Length - 1);
                currentNode.AddNext(nextNode);
            }

            currentNode = nextNode;
        }
    }
}

public class Node
{
    private const int AlphabetPower = 26;
    private const char MinSymbol = 'a';
    private readonly Node?[] _edges = new Node?[AlphabetPower];

    public bool IsTerminal { get; }
    public char? Symbol { get; }
    public int? Index { get; }
    public IList<Node?> NextEdges => _edges;

    public Node(char? symbol = null, bool isTerminal = false)
    {
        Symbol = symbol;
        Index = symbol == null ? null : (symbol - MinSymbol);
        IsTerminal = isTerminal;
    }

    public Node? GetNext(char symbol)
    {
        var index = symbol - MinSymbol;
        return NextEdges[index];
    }

    public void AddNext(Node node)
    {
        var index = node.Index ?? throw new InvalidDataException();
        NextEdges[index] = node;
    }
}