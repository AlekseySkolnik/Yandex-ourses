using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Section_4
{
    /*
   Задание
    Тимофей, как хороший руководитель, хранит информацию о зарплатах своих сотрудников в базе данных и постоянно её обновляет.
    Он поручил вам написать реализацию хеш-таблицы, чтобы хранить в ней базу данных с зарплатами сотрудников.
    Хеш-таблица должна поддерживать следующие операции.
    В таблице хранятся уникальные ключи.
    Требования к реализации: 
    Нельзя использовать имеющиеся в языках программирования реализации хеш-таблиц 
    Разрешать коллизии следует с помощью метода цепочек или с помощью открытой адресации.
    Все операции должны выполняться за O(1) в среднем.
    Поддерживать рехеширование и масштабирование хеш-таблицы не требуется.
    Ключи и значения, id сотрудников и их зарплата, —– целые числа. 
    Поддерживать произвольные хешируемые типы не требуется.

    Формат ввода
    В первой строке задано общее число запросов к таблице n (1≤ n≤ 106).
    В следующих n строках записаны запросы, которые бывают трех видов –— get, put, delete —– как описано в условии.
    Все ключи и значения –— целые числа, не превосходящие 109 по модулю.
    Числа могут быть и отрицательными в том числе.
    При любой последовательности команд, количество ключей в хеш-таблице не может превышать 105.
    
    Формат вывода
    На каждый запрос вида get и delete выведите ответ на него в отдельной строке.
   */

    /*
    -- ПРИНЦИП РАБОТЫ --
    Создаем хеш-таблицу на основе массива. Элементом массива будет связный список (для разрешения коллизий).
    В каждой команде первым вводится ключ. Получаем индекс элемента по хешу ключа. 
    Если элемент null, то:
        - для get и delete возвращаем "None"
        - для put создаем связный список, добавляем в него head, кладем в ячейку массива 
    Если элемент не null, то:
        - для get проходимся по связному списку, как только значение ключа совпало с Id сотрудника,
        прерываемся и возвращаем его зарплату.
        - для delete проходимся по связному списку, как только значение ключа совпало с Id сотрудника,
        прерываемся, кешируем зарплату сорудника, удаляем найднный элемент из связного списка, возвращаем зарплату.
        - для put проходимся по связному списку. Если нашлось совпадение ключа и Id сотрудника, 
        то обновляем у этого сотрудника зарплату. Если нет - вставляем новый элемент в начало связного списка.

    Значение KeysCount нужно подбирать с учётом специфики задачи и имеющихся ресурсов.
    Если параметр будет слишком мал, возникнет много коллизий. 
    Если будет слишком большой — потребуется много памяти для хранения таблицы.
    Т.к. по условию при любой последовательности команд, количество ключей в хеш-таблице не может превышать 10^5,
    то KeysCount для уменьшения вероятности коллизий должен быть простым числов. Возьмем 100003.

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    Хеш по ключу считается за O(1). 
    Поиск элемента в массиве по индексу за O(1).
    Вставка и удаление элемента из связного списка за O(1).
    Все операции должны выполняться за O(1) в среднем.

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    O(n)
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/24414/run-report/89407472/
    /// </summary>
    public class Final_B
    {
        private const string None = "None";
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
        private static List<string>[] _commands;

        public static void Main(string[] args)
        {
            ReadCommands();
            var strBuilder = new StringBuilder();
            ProcessingCommands(strBuilder);
            Console.WriteLine(strBuilder.ToString());
        }

        private static void ProcessingCommands(StringBuilder strBuilder)
        {
            var hashTable = new CustomHashTable();
            int key;
            int? value;

            foreach (var command in _commands)
            {
                switch (command[0])
                {
                    case Commands.Get:
                        key = int.Parse(command[1]);
                        value = hashTable.Get(key);
                        strBuilder.AppendLine(value.HasValue ? value.Value.ToString() : None);
                        break;

                    case Commands.Delete:
                        key = int.Parse(command[1]);
                        value = hashTable.Delete(key);
                        strBuilder.AppendLine(value.HasValue ? value.Value.ToString() : None);
                        break;

                    case Commands.Put:
                        key = int.Parse(command[1]);
                        value = int.Parse(command[2]);
                        hashTable.Put(key, value.Value);
                        break;
                }
            }
        }

        private static void ReadCommands()
        {
            var commandsCount = int.Parse(_reader.ReadLine() ?? "0");
            _commands = new List<string>[commandsCount];

            for (int i = 0; i < commandsCount; i++)
            {
                _commands[i] = ReadList();
            }
        }

        private static List<string> ReadList() =>
            _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }
}

public class CustomHashTable
{
    private const int KeysCount = 100_003;

    private readonly LinkedList<Employee>?[] _employeers = new LinkedList<Employee>?[KeysCount];

    /// <summary>
    /// Получение значения по ключу. Если ключа нет в таблице, то вывести «None». Иначе вывести найденное значение. 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int? Get(int key)
    {
        var index = GetIndex(key);

        if (_employeers[index] != null)
        {
            foreach (var employee in _employeers[index])
            {
                if (employee.Id == key)
                {
                    return employee.Salary;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Удаление ключа из таблицы.
    /// Если такого ключа нет, то вывести «None», иначе вывести хранимое по данному ключу значение и удалить ключ.
    /// </summary>
    public int? Delete(int key)
    {
        Employee? result = null;
        var index = GetIndex(key);

        if (_employeers[index] != null)
        {
            foreach (var employee in _employeers[index])
            {
                if (employee.Id == key)
                {
                    result = employee;
                    break;
                }
            }

            if (result != null)
            {
                _employeers[index].Remove(result);
            }
        }

        return result?.Salary;
    }

    /// <summary>
    /// Добавление пары ключ-значение.
    /// Если заданный ключ уже есть в таблице, то соответствующее ему значение обновляется. 
    /// </summary>
    public void Put(int key, int value)
    {
        var index = GetIndex(key);

        if (_employeers[index] != null)
        {
            Employee? result = null;

            foreach (var employee in _employeers[index])
            {
                if (employee.Id == key)
                {
                    result = employee;
                    break;
                }
            }

            if (result == null)
            {
                _employeers[index].AddFirst(new Employee(key, value));
            }
            else
            {
                result.Salary = value;
            }
        }
        else
        {
            var node = new LinkedList<Employee>();
            node.AddFirst(new Employee(key, value));
            _employeers[index] = node;
        }
    }

    private static int GetIndex(int key)
    {
        // Вычислить значение хеш-функции x=h(k) от ключа.
        var x = Hash(key);
        // По значению хеш-функции получаем индекс корзины i = x mod len(H).
        var index = x % KeysCount;

        if (index < 0)
        {
            index %= KeysCount;
            index += KeysCount;
            index %= KeysCount;
        }

        return index;
    }

    // Введём специальную тождественную хеш-функцию, которая будет возвращать свой аргумент неизменным hash(k)=k
    private static int Hash(int key) => key;

    private class Employee
    {
        public int Id { get; set; }
        public int Salary { get; set; }

        public Employee(int id, int salary)
        {
            Id = id;
            Salary = salary;
        }
    }
}

public static class Commands
{
    public const string Get = "get";
    public const string Delete = "delete";
    public const string Put = "put";
}