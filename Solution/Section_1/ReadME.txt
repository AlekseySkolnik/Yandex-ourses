Если нужно вывести два числа через пробел без перевода строки, подойдёт метод Write:
    writer.Write("{0} {1}", a, b);

Допустим, вам хочется считывать из файла, а не из стандартного потока ввода. Это можно сделать так:
    reader = new StreamReader("input.txt");
// остальной код аналогичен 

Чтобы считать массив чисел, используйте следующую функцию:
    private static List<int> ReadList()
    {
        return reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    } 

Когда в массиве могут быть как числа, так и строки, считайте массив строк, а потом решите, конвертировать конкретный элемент в число или нет:
    private static List<string> ReadList()
    {
        return reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    } 