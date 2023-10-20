using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Section_4
{
    /*
    Задание
    В этой задаче можно пользоваться хеш-таблицами из стандартных библиотек.
    Тимофей пишет свою поисковую систему.
    Имеется n документов, каждый из которых представляет собой текст из слов.
    По этим документам требуется построить поисковый индекс.
    На вход системе будут подаваться запросы.
    Запрос —– некоторый набор слов.
    По запросу надо вывести 5 самых релевантных документов.
    Релевантность документа оценивается следующим образом: 
    для каждого уникального слова из запроса берётся число его вхождений в документ,
    полученные числа для всех слов из запроса суммируются. 
    Итоговая сумма и является релевантностью документа. 
    Чем больше сумма, тем больше документ подходит под запрос.
    Сортировка документов на выдаче производится по убыванию релевантности. 
    Если релевантности документов совпадают —
    то по возрастанию их порядковых номеров в базе (то есть во входных данных).
    Подумайте над случаями, когда запросы состоят из слов, встречающихся в малом количестве документов. 
    Что если одно слово много раз встречается в одном документе?
    
    Формат ввода
    В первой строке дано натуральное число n —– количество документов в базе (1 ≤ n ≤ 104).
    Далее в n строках даны документы по одному в строке. 
    Каждый документ состоит из нескольких слов,
    слова отделяются друг от друга одним пробелом и состоят из маленьких латинских букв.
    Длина одного текста не превосходит 1000 символов. 
    Текст не бывает пустым.
    В следующей строке дано число запросов —– натуральное число m (1 ≤ m ≤ 104).
    В следующих m строках даны запросы по одному в строке. 
    Каждый запрос состоит из одного или нескольких слов.
    Запрос не бывает пустым. Слова отделяются друг от друга одним пробелом и состоят из маленьких латинских букв. 
    Число символов в запросе не превосходит 100.
    
    Формат вывода
    Для каждого запроса выведите на одной строке номера пяти самых релевантных документов. 
    Если нашлось менее пяти документов, то выведите столько, сколько нашлось.
    Документы с релевантностью 0 выдавать не нужно.
   */

    /*
    -- ПРИНЦИП РАБОТЫ --
    1. Cчитываем каждую строку, сразу разбиваем ее на отдельные слова, сохраняем в _stringDocuments.
    2. Считываем каждую строку, сразу разбиваем ее на уникальные слова, сохраняем в _requests.
    3. Проходимся по списку _stringDocuments:
        3.1 В _stringDocuments[i] находится список слов документа. 
            Создаем документ, в нем сразу считаем сколько раз встретилось каждое слово.
        3.2. Добавляем документ в список Documents.
        3.3. Сразу отмечаем в словаре WordInDocuments, что такой-то набор слов _stringDocuments[i] встретился в документе с номером i.
    4. В результате у нас словарь WordInDocuments, с помощью которого можно определить список документов,
    в которых встречается текущее слово.
    5. Проходимся по массиву запросов _requests. Один элемент массива - мапа (HashSet) 
        - содержит уникальный набор слов, который мы хотим найти в документах.
      5.1. Получаем список документов, в которых встречалось текущее слово из запроса.
      5.2. Для каждого документа считаем релевантность
    6. Сортируем полученный список документов, возвращаем 5 первых элементов, если они есть.

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
    Для каждого документа подсчитано сколько раз встретилось его каждое слово.
    Каждый документ добавлен в список документов Documents. 
    При добавлении документа в словаре WordInDocuments сразу добавляются слова, 
        которые есть в документе и номер этого документа. 
    WordInDocuments = [{word1, [1]}]. (Слово word1 встречается в документе 1)
    Если слово новое, то добавляется оно и номер документа: WordInDocuments = [{word1, [1]}, {word2, [2]}]
    Если слово не новое, то вставляем номер словаря для этого слова: WordInDocuments = [{word1, [1, 2]}]
    В итоге все слова в документах учтены.
    Из реквестом получаем уникальный набор слов и считаем для каждого релевантность.   

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    Пусть у нас N документов, в каждом документе С уникальных слов.
    Пусть у нас M реквестов, в каждом реквесте T уникальных слов.
    r - произвольная константа
    Сложность создания документа O(r * N * C) = O(N)
    Сложность добавления документа в список O(r*C) = O(n)
    Сложность вычисления релевантности для одного слова O(1), для все слов реквеста O(n)
    Общая сложность O(N*M) 

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    O(n)

     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/24414/run-report/89407414/
    /// </summary>
    public class Final_A
    {
        private static StreamReader _reader = new(Console.OpenStandardInput());
        private static List<string>[] _stringDocuments;
        private static HashSet<string>[] _requests;

        public static void Main(string[] args)
        {
            ReadDocuments();
            ReadRequests();
            var documentsManager = CreateDocumentManager();
            PrintResults(documentsManager);
        }

        private static void ReadDocuments()
        {
            var documentsCount = int.Parse(_reader.ReadLine() ?? "0");
            _stringDocuments = new List<string>[documentsCount];

            for (int i = 0; i < documentsCount; i++)
            {
                _stringDocuments[i] = ReadStringList();
            }
        }

        private static void ReadRequests()
        {
            var requestsCount = int.Parse(_reader.ReadLine() ?? "0");
            _requests = new HashSet<string>[requestsCount];

            for (int i = 0; i < requestsCount; i++)
            {
                _requests[i] = ReadStringHashSet();
            }
        }

        private static DocumentsManager CreateDocumentManager()
        {
            var docManager = new DocumentsManager(_stringDocuments.Length);

            for (int i = 0; i < _stringDocuments.Length; i++)
            {
                var documentWords = _stringDocuments[i];
                var doc = Document.Parse(documentWords, i + 1);
                docManager.AddDocument(doc);
            }

            return docManager;
        }

        private static void PrintResults(DocumentsManager documentsManager)
        {
            foreach (var request in _requests)
            {
                var relevantDocNumbers = documentsManager.GetRelevantDocumentNumbers(request);

                if (relevantDocNumbers.Count > 0)
                {
                    Console.WriteLine(string.Join(' ', relevantDocNumbers));
                }
            }
        }

        private static List<string> ReadStringList() =>
            _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        private static HashSet<string> ReadStringHashSet() =>
            _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .ToHashSet();
    }

    public class Document
    {
        /// <summary>
        /// Порядковый номер документа
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Словарь (какое слово сколько раз встретилось)
        /// </summary>
        public Dictionary<string, int> Words { get; }

        protected Document(int number, Dictionary<string, int> words)
        {
            Words = words;
            Number = number;
        }

        /// <summary>
        /// Релевантность слова в документе
        /// </summary>
        /// <param name="word">Слово, которое ищем в документе</param>
        /// <returns>Сколько раз слово встретилось в документе</returns>
        public int GetRelevant(string word)
        {
            Words.ContainsKey(word);
            return Words.ContainsKey(word) ? Words[word] : 0;
        }

        /// <summary>
        /// Создает документ с порядковым номером number и набором слов documentWords
        /// </summary>
        /// <param name="documentWords">Словарь слов документа и количество их в тексте документа</param>
        /// <param name="number">Порядковый номер документа</param>
        /// <returns></returns>
        public static Document Parse(IEnumerable<string> documentWords, int number)
        {
            var dict = new Dictionary<string, int>();

            foreach (var word in documentWords)
            {
                if (dict.ContainsKey(word))
                {
                    dict[word]++; // O(1)
                }
                else
                {
                    dict.Add(word, 1); // O(1)
                }
            }

            return new Document(number, dict);
        }
    }

    public class DocumentsManager
    {
        /// <summary>
        /// Список документов
        /// </summary>
        public Document[] Documents { get; }

        /// <summary>
        /// Словарь (какое слово встречается в каких номерах документов)
        /// </summary>
        public Dictionary<string, List<int>> WordInDocuments { get; }

        public DocumentsManager(int documentCount)
        {
            Documents = new Document[++documentCount];
            WordInDocuments = new Dictionary<string, List<int>>();
        }

        public void AddDocument(Document doc)
        {
            Documents[doc.Number] = doc;

            foreach (var word in doc.Words.Keys)
            {
                if (WordInDocuments.ContainsKey(word))
                {
                    WordInDocuments[word].Add(doc.Number);
                }
                else
                {
                    WordInDocuments.Add(word, new List<int>() { doc.Number });
                }
            }
        }

        public List<int> GetRelevantDocumentNumbers(HashSet<string> words)
        {
            var docDict = new Dictionary<int, int>();

            foreach (var word in words)
            {
                var docNumbers = GetDocumentsNumberContainingWord(word);

                foreach (var number in docNumbers)
                {
                    if (docDict.ContainsKey(number))
                    {
                        docDict[number] += Documents[number].GetRelevant(word);
                    }
                    else
                    {
                        docDict.Add(number, Documents[number].GetRelevant(word));
                    }
                }
            }

            return TakeSortDocNumbers(docDict);
        }

        private List<int> GetDocumentsNumberContainingWord(string word)
        {
            if (WordInDocuments.ContainsKey(word))
            {
                var numbers = WordInDocuments[word];
                return numbers;
            }

            return new List<int>();
        }

        private static List<int> TakeSortDocNumbers(Dictionary<int, int> docDict)
        {
            return
                 docDict
                 .OrderByDescending(x => x.Value)
                 .ThenBy(x => x.Key)
                 .Select(x => x.Key)
                 .Take(5)
                 .ToList();
        }
    }
}