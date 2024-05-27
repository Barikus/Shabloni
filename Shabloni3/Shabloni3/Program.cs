using Shabloni3;
using static CustomConsole;

class Program
{
    static void Main()
    {
        DataProxy proxy = new DataProxy();

        CustomConsole.PrintColor("Cyan", "Кэширование:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(proxy.GetData("key"));
        }

        Console.WriteLine();

        CustomConsole.PrintColor("Blue", "Вызов функции:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(DataSource.GetData());
        }
    }
}