namespace Section_1;

public class Solution
{
    private static TextReader _reader;
    private static TextWriter _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());

        //Task_B();
        //Task_C();
        // Task_D();

        E.MainE(args);

        _reader.Close();
        _writer.Close();
    }

    /// <summary>
    /// Застёжка-молния
    /// </summary>
    private static void Task_B()
    {
        var n = ReadInt();
        var numbers1 = ReadList();
        var numbers2 = ReadList();

        for (var i = 0; i < n; i++)
        {
            _writer.Write("{0} ", numbers1[i]);
            _writer.Write("{0} ", numbers2[i]);
        }
    }

    /// <summary>
    /// C. Скользящее среднее
    /// </summary>
    private static void Task_C()
    {
        var n = ReadInt();
        var timeseries = ReadList();
        var k = ReadInt();

        var result = new List<double>(n - k + 1);

        var current_sum = 0;

        for (int i = 0; i < k; i++)
        {
            current_sum += timeseries[i];
        }

        result.Add((double)(current_sum / (double)k));

        for (int i = 0; i < n - k; i++)
        {
            current_sum -= timeseries[i];
            current_sum += timeseries[i + k];
            result.Add((double)(current_sum / (double)k));
        }

        for (var i = 0; i < result.Count; i++)
        {
            _writer.Write($"{result[i]} ");
            //System.Diagnostics.Debug.Write($"{result[i]} ");
        }
    }


    /// <summary>
    /// Две фишки
    /// </summary>
    private static void Task_D()
    {
        /*
        Рита и Гоша играют в игру. У Риты есть n фишек, на каждой из которых написано количество очков. 
        Сначала Гоша называет число k, затем Рита должна выбрать две фишки, сумма очков на которых равна заданному числу.
        Рите надоело искать фишки самой, и она решила применить свои навыки программирования для решения этой задачи. 
        Помогите ей написать программу для поиска нужных фишек.
         */

        var n = ReadInt();
        var numbers = ReadList();
        var sum = ReadInt();

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (numbers[i] + numbers[j] == sum)
                {
                    _writer.Write($"{numbers[i]} {numbers[j]}");
                    System.Diagnostics.Debug.Write($"{numbers[i]} {numbers[j]}");
                    return;
                }
            }
        }

        _writer.Write("None");
    }

    private static int ReadInt() =>
        int.Parse(s: _reader?.ReadLine());

    private static List<int> ReadList() =>
        _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
}