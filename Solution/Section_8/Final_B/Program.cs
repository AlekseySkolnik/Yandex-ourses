using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
// Шпаргалка dp+trie

namespace Section_8
{
    /*
    Задание
    Вася готовится к экзамену по алгоритмам и на всякий случай пишет шпаргалки.
    Чтобы уместить на них как можно больше информации, он не разделяет слова пробелами.
    В итоге получается одна очень длинная строка.
    Чтобы на самом экзамене из-за нервов не запутаться в прочитанном, он просит вас написать программу,
    которая по этой длинной строке и набору допустимых слов определит, можно ли разбить текст на отдельные слова из набора.
    
    Более формально: дан текст T и набор строк s1, ... ,sn.
    Надо определить, представим ли T как sk1sk2...skr, где где ki — индексы строк. 
    Индексы могут повторяться. Строка si может встречаться в разбиении текста T произвольное число раз.
    Можно использовать не все строки для разбиения. Строки могут идти в любом порядке.

    Формат вывода
    Выведите «YES», если текст можно разбить на слова из данного словаря, или «NO» в ином случае.
   
    -- ПРИНЦИП РАБОТЫ --
    Алгоритм состоит из 2 этапов:
    1. CreateTrie. Формирование бора (префиксного дерева) по словам из списка ввода.
    2. IsCanSplitText. Динамическое программирование для поиска терминальных узлов дерева в тексте. 
    
    Пример. Строка text = "wetgwell", словарь = ["we", "wet", "well", "tg"].
    "we"+"tg"+"well".

      j = 0 1 2 3 4 5 6 7 8
          - W E T G W E L L
     init T F F F F F F F F  
    i = 0 T F T T F F F F F 
    i = 1 T F T T F F F F F 
    i = 2 T F T T T F F F F 
    i = 3 T F T T T F F F F 
    i = 4 T F T T T F T F F 
    i = 5 T F T T T F T F T 
    i = 6 T F T T T F T F T 
    i = 7 T F T T T F T F T 
    i = 8 T F T T T F T F T

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
    Вводим булевый массив dp.

     Что будет храниться в dp?
        dp — это булевый массив длиной text.Length + 1. Динамика двумерная, но мы будем хранить только последнюю строку.
        dp[i,j] - хранит информацию, есть ли терминальный узел в дереве для подстроки text[i..j], где j > i.
        Например, dp[3,4] = true, т.к. text[3..4] = "tg", такое слово есть в словаре, этот узел терминальный.

        Каким будет базовый случай для задачи? 
            dp[0] = true. Примем, что пустой текст соответствует корню дерева, как будто он терминальный.

        Каким будет переход динамики?
            Итоговая формула : dp[i, j] = FindNodeForStr(text[i..j]) == IsTerminal ? 

        Каким будет порядок вычисления данных в массиве dp? 
            Заполняем сначала целиком первую строку матрицы, потом вторую и так далее — пока не заполним i-ю строку.
            Но для оптимизации dp будем одномерным. Предыдущие i строки хранить нет смысла.

        Где будет располагаться ответ на исходный вопрос? 
            Ответ будет лежать в правом нижнем углу — в ячейке dp[text.Length][text.Length].

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    Сложность построения бора O(L), где L — суммарная длина слов во множестве.
    Сложность поиска  O(N*N). N = text.Length - длина текста

    Общая O(N^2*L)
	
    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    Мощность алфавита AlphabetPower = C = 26. N = Max(Words).Length - длина самого длинного слова из словаря. 
    Хранение бора O(C * N).
    Хранение dp = O(M), M - длина текста.

    Общая O(N*M).

     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/26133/run-report/93681444/
    /// </summary>
    public class Final_B
    {
        private const string AnswerYes = "YES";
        private const string AnswerNo = "NO";
        private static StreamReader _reader = new(Console.OpenStandardInput());

        public static void Main(string[] args)
        {
            var text = _reader?.ReadLine() ?? throw new InvalidDataException();
            var count = int.Parse(_reader?.ReadLine());

            var trie = CreateTrie(count);
            var result = IsCanSplitText(text, trie);

            Console.WriteLine(result ? AnswerYes : AnswerNo);
        }

        private static bool IsCanSplitText(string text, Node root)
        {
            // найденные терминальные состояния
            var dp = new bool[text.Length + 1];
            dp[0] = true;

            for (int i = 0; i < text.Length; i++)
            {
                if (!dp[i])
                {
                    // пропускаем, уже обрабатывали
                    continue;
                }

                var currentNode = root;

                for (int j = i; j <= text.Length; j++)
                {
                    if (currentNode.IsTerminal)
                    {
                        dp[j] = true; // нашли терминальное состояние
                    }

                    if (j == text.Length)
                    {
                        break;
                    }

                    var next = currentNode.GetNext(text[j]);

                    if (next == null)
                    {
                        break;
                    }

                    currentNode = next;
                }
            }

            return dp[text.Length];
        }

        /// <summary>
        /// Создание префиксного дерева
        /// </summary>
        /// <param name="count"></param>
        /// <returns>корень дерева</returns>
        private static Node CreateTrie(int count)
        {
            var root = new Node(); // корень дерева

            for (int i = 0; i < count; i++)
            {
                var word = _reader?.ReadLine();
                AddWordToTrie(root, word);
            }

            return root;
        }

        /// <summary>
        /// Добавление слова в префиксное дерево
        /// </summary>
        /// <param name="root">корень дерева</param>
        /// <param name="word">слово</param>
        private static void AddWordToTrie(Node root, string word)
        {
            var currentNode = root;

            for (int i = 0; i < word.Length; i++)
            {
                var nextNode = currentNode.GetNext(word[i]);
                var isTerminal = i == word.Length - 1;

                if (nextNode == null)
                {
                    nextNode = new Node(word[i], isTerminal);
                    currentNode.AddNext(nextNode);
                }
                else if (isTerminal)
                {
                    nextNode.SetTerminal();
                }

                currentNode = nextNode;
            }
        }
    }

    /// <summary>
    /// Узел префиксного дерева
    /// </summary>
    public class Node
    {
        private const int AlphabetPower = 26;
        private const char MinSymbol = 'a';
        private readonly Node?[] _edges = new Node?[AlphabetPower];

        public bool IsTerminal { get; private set; }
        public int? Index => Symbol.HasValue ? (Symbol - MinSymbol) : null;
        public char? Symbol { get; }
        public IList<Node?> NextEdges => _edges;

        public Node(char? symbol = null, bool isTerminal = false)
        {
            Symbol = symbol;
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

        public void SetTerminal()
        {
            IsTerminal = true;
        }
    }
}