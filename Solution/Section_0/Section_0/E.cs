namespace Section_1;

public static class E
{
    private static TextReader reader;
    private static TextWriter writer;

    // Если ответ существует, верните список из двух элементов
    // Если нет - то верните пустой список
    public static List<int> TwoSum2(List<int> sortedArray, int X)
    {
        var previous = new HashSet<int>();

        foreach (var A in sortedArray)
        {
            var Y = X - A;

            if (previous.Contains(Y))
            {
                return new List<int>(2) { A, Y };
            }
            else
            {
                previous.Add(A);
            }
        }

        return new List<int>(0);
    }

    // Если ответ существует, верните список из двух элементов
    // Если нет - то верните пустой список
    public static List<int> TwoSum(List<int> sortedArray, int targetSum)
    {
        var left = 0;
        var right = sortedArray.Count - 1;

        while (left < right)
        {
            var sum = sortedArray[left] + sortedArray[right];

            if (sum == targetSum)
            {
                return new List<int>(2) { sortedArray[left], sortedArray[right] };
            }

            if (sum > targetSum)
            {
                right--;
            }

            if (sum < targetSum)
            {
                left++;
            }
        }

        return new List<int>(0);
    }

    public static void MainE(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var n = ReadInt();
        var sortedArray = ReadList();
        var targetSum = ReadInt();
        var result = TwoSum(sortedArray, targetSum);
        writer.WriteLine(!result.Any() ? "None" : string.Join(" ", result));

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static List<int> ReadList()
    {
        return reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}