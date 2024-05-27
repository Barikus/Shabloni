using Common;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Сегодня: {DateTime.Today.ToLongDateString()}\n");

        Console.WriteLine("Выберите, какой плагин использовать:");

        START:
        try
        {
            IPlugin plugin = PluginFactory.GetPlugin(int.Parse(Console.ReadLine()));

            plugin.Initialize();
            plugin.Execute();
            plugin.Terminate();
        }
        catch (Exception ex)
        {
            Console.WriteLine("К сожалению, плагин под таким номером недоступен\nПопробуйте другой...\n");
        }
        goto START;
    }
}