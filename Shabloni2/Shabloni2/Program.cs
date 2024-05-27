using MiddlewarePipeline;
using Shabloni2;
using Shabloni2.Models;
using static CustomConsole;

class Program
{
    static void Main()
    {
        // Формирование цепочки обработчиков
        ArchivePropertiesHandler archivePropertiesHandler = new ArchivePropertiesHandler();
        CorruptionHandler corruptionHandler = new CorruptionHandler();
        VirusScanHandler virusScanHandler = new VirusScanHandler();
        SaveFileHandler saveFileHandler = new SaveFileHandler();

        // Устанавливаем звенья в цепочке обработчиков
        archivePropertiesHandler.SetNextHandler(corruptionHandler).
            SetNextHandler(virusScanHandler).
            SetNextHandler(saveFileHandler);


        // Наши данные
        List<ArchiveModel> data = DataTest.GetData();
        START:
        int fileIndex = ChooseFile(data); // <= Какой файл мы решили отправить на обработку (см. DataTest.cs)
        ArchiveModel selectedFile = data[fileIndex];

        // Запуск обработки файла через цепочку обработчиков, начиная с первого
        CustomConsole.PrintColor("Yellow", "Начало обработки файла...");
        bool status = archivePropertiesHandler.ProcessRequest(selectedFile);
        
        if (status)
            CustomConsole.PrintColor("Green", "Обработка завершена успешно!");
        else
            CustomConsole.PrintColor("Red", "Обработка прошла неуспешно!");

        CustomConsole.PrintColor("darkgray", "\nНажмите любую клавишу, чтобы начать новую попытку...");
        Console.ReadKey();
        goto START;
    }

    static int ChooseFile(List<ArchiveModel> data)
    {
        Console.Clear();
        Console.WriteLine("Файлы:");
        for (int i = 0; i < data.Count; i++)
        {
            Console.WriteLine($"{i}: {data[i].Name} ({data[i].Extension}, {data[i].Size} байт)");
        }

        Console.Write("\nВведите индекс выбранного файла: ");
        if (int.TryParse(Console.ReadLine(), out int selectedIndex))
        {
            if (selectedIndex >= 0 && selectedIndex < data.Count)
            {
                ArchiveModel selectedFile = data[selectedIndex];
                Console.Clear();
                Console.WriteLine($"Вы выбрали файл: {selectedFile.Name} ({selectedFile.Extension}, {selectedFile.Size} байт)");
                return selectedIndex;
            }
        }

        Console.Clear();
        CustomConsole.PrintColor("red", "Некорректный ввод!");
        Console.WriteLine("Автоматически выбран файл под индексом 0:");
        Console.WriteLine($"{data[0].Name} ({data[0].Extension}, {data[0].Size} байт.");
        return 0;
    }
}
