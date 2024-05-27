using Shabloni2.Models;
using System;
using System.Runtime.InteropServices;
using static CustomConsole;

namespace MiddlewarePipeline
{
    // Интерфейс для обработчика
    public interface IHandler
    {
        IHandler SetNextHandler(IHandler handler);
        bool ProcessRequest(ArchiveModel file);
    }

    // Базовый класс
    public abstract class BaseHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNextHandler(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public abstract bool ProcessRequest(ArchiveModel file);

        protected bool PassNext(ArchiveModel file)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.ProcessRequest(file);
            }

            return true;  // Если нет следующего обработчика, цепочка завершена успешно.
        }

        protected void OK()
        {
            CustomConsole.PrintColor("green", " > OK!");
        }

        protected void FAIL()
        {
            CustomConsole.PrintColor("red", " > FAIL!");
        }
    }

    // Конкретные обработчики
    public class ArchivePropertiesHandler : BaseHandler // проверка свойств
    {
        public override bool ProcessRequest(ArchiveModel file)
        {
            CustomConsole.PrintColor("cyan", "Проверка свойств архива:");

            // настройки
            int maxSize = int.MaxValue;
            int maxCharacter = 16;
            string[] extensions = new string[] { ".zip", ".rar", ".tar" };

            // проверка
            bool containsNonStandardCharacters = file.Name.Any(c => !IsStandardCharacter(c));

            if (!extensions.Contains(file.Extension) ||
                file.Name.Length > maxCharacter || 
                file.Name.Length == 0 || 
                containsNonStandardCharacters ||
                file.Size > maxSize || file.Size < 1)
            {
                FAIL();
                return false;
            }
            else
            {
                OK();
            }

            return PassNext(file);
        }

        static bool IsStandardCharacter(char c)
        {
            return c >= 0 && c <= 127;
        }
    }

    public class CorruptionHandler : BaseHandler // проверка на целостность архива
    {
        public override bool ProcessRequest(ArchiveModel file)
        {
            CustomConsole.PrintColor("cyan", "Проверка на целостность:");

            Random random = new Random();
            int randomInt = random.Next(1, 5);

            bool isCurrupted = (randomInt == 1) ? true : false;

            if (isCurrupted)
            {
                FAIL();
                return false;
            }
            else
                OK();

            return PassNext(file);
        }
    }

    public class VirusScanHandler : BaseHandler // проверка на вирусы
    {
        public override bool ProcessRequest(ArchiveModel file)
        {
            CustomConsole.PrintColor("cyan", "Проверка на наличие вирусов:");

            string[] bannedWords = new string[] { "Virus", "Trojan", "Malware", "Spyware", "Phishing" };

            bool containsBannedWord = bannedWords.Any(word => file.Name.Contains(word));

            if (containsBannedWord)
            {
                FAIL();
                return false;
            }
            else
                OK();

            return PassNext(file);
        }
    }

    public class SaveFileHandler : BaseHandler
    {
        public override bool ProcessRequest(ArchiveModel file)
        {
            CustomConsole.PrintColor("cyan", "Сохранение на сервер:");

            // пусть будет проверка, что файл загружается не ночью (технические работы)
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan nightStart = new TimeSpan(24, 0, 0);
            TimeSpan nightEnd = new TimeSpan(5, 0, 0);

            if (currentTime >= nightStart || currentTime < nightEnd)
            {
                FAIL();
                return false;
            }
            else
                OK();

            // Всегда заключительный этап - возвращаем успешное завершение цепочки
            return true;
        }
    }
}