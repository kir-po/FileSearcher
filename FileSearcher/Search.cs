using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Threading;

namespace FileSearcher
{
    public class Search
    {
        // Делегаты (не используются)
        public delegate void FileSearchHandler(object sender, FileSearchEventArgs e);
        public delegate void CurrentDirectoryHandler(object sender, CurrentDirectoryEventArgs e);


        // События (не используются)
        public event FileSearchHandler NotifyAboutFoundFile;
        public event CurrentDirectoryHandler NotifyAboutCurrentDirectory;


        // Поля
        private string startSearchPath;     // Поле стартовой директории поиска
        private ManualResetEvent mre;       // Поле для ManualResetEvent
        private BackgroundWorker worker;    // Поле для BackgroundWorker
        private SearchResults results;      // Поле для результатов поиска


        // Свойства
        public string StartSearchPath           // Инициализация свойства StartSearchPath
        {
            get
            {
                return startSearchPath;         // Возвращает поле startSearchPath без обработки
            }
            set
            {
                // Проверка, существует ли указанная директория
                if (Directory.Exists(value))
                {
                    startSearchPath = value;
                }
            }
        }
        public Regex SearchRegex { get; set; }  // Регулярное выражения использующееся при поиске


        // Конструкторы
        public Search(string startSearchPath, string regex, ManualResetEvent _mre, BackgroundWorker _worker)
        {
            StartSearchPath = startSearchPath;
            SearchRegex = new Regex(regex, RegexOptions.IgnoreCase);
            mre = _mre;
            worker = _worker;
        }


        // Публичные методы

        // Обертка для вызова методов: подсчитывающего общее количество файлов и производящего поиск
        public void GoSearch()
        {
            results = new SearchResults();                                                          // Инициализация поля результатов поиска
            try
            {
                // Инициализация свойств "Кол-во найденных файлов" и "Общее кол-во файлов" поля results
                results.FoundFiles = 0;
                results.FilesCount = 0;
                RecursiveFileCounter(StartSearchPath);                                              // Запуск подсчета общего количества файлов
                worker.ReportProgress(0, results);                                                  // Сообщение об общем количестве файлов в стартовой директории
                                                                                                    // (включая поддиректории)
                RecursiveObserver(Directory.GetDirectories(StartSearchPath), worker);               // Запуск рекурсивного обхода поддиректорий стартовой директории с поиском
            }
            catch (Exception ex)
            {
                Exception e = new Exception("Указанная директория не существует");
                results.Exception = e;
                worker.ReportProgress(0, results);                                                  // Сообщение о произошедшем исключении
            }
        }


        // Приватные методы

        // Метод, производящий подсчет общего количества файлов в стартовой директории (включая файлы в поддиректориях)
        private void RecursiveFileCounter(string path)
        {
            results.FilesCount += Directory.GetFiles(path).Length;
            string[] currentSubDirs = Directory.GetDirectories(path);

            if (currentSubDirs.Length != 0)
            {
                for (int i = 0; i < currentSubDirs.Length; i++)
                {
                    try
                    {
                        if (Directory.GetFiles(currentSubDirs[i]).Length > 0 | Directory.GetDirectories(currentSubDirs[i]).Length > 0)
                        {
                            if (worker.CancellationPending == true)
                            {
                                return;
                            }
                            mre.WaitOne();
                            worker.ReportProgress(0, results);
                            RecursiveFileCounter(currentSubDirs[i]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        // Метод производящий поиск и передающий results в BackgroundWorker.ReportProgress о найденном вхождении для обновления дерева
        private void RecursiveObserver(string[] dirs, BackgroundWorker bw)
        {
            if (worker.CancellationPending == true)
            {
                return;
            }
            for (int i = 0; i < dirs.Length; i++)
            {
                try
                {
                    // Уведомление о текущей директории
                    Console.WriteLine(dirs[i]);
                    results.CurrentDirectory = dirs[i];
                    NotifyAboutCurrentDirectory?.Invoke(this, new CurrentDirectoryEventArgs(dirs[i]));

                    // Поиск совпадений в файлах в текущей директории
                    string[] filesInDir = Directory.GetFiles(dirs[i]);
                    for (int j = 0; j < filesInDir.Length; j++)
                    {
                        FileInfo fInfo = new FileInfo(filesInDir[j]);
                        string fName = fInfo.Name;
                        if (SearchRegex.IsMatch(fName))
                        {
                            if (worker.CancellationPending == true)
                            {
                                return;
                            }
                            mre.WaitOne();
                            NotifyAboutFoundFile?.Invoke(this, new FileSearchEventArgs(filesInDir[j]));
                            Console.WriteLine("Найден файл: " + filesInDir[j]);
                            results.FoundFiles++;
                            results.CurrentFoundFilePath = filesInDir[j];
                            bw.ReportProgress(0, results);
                        }
                    }

                    mre.WaitOne();
                    // Начало рекурсивного обхода папок
                    if (Directory.GetDirectories(dirs[i]).Length > 0)
                    {
                        RecursiveObserver(Directory.GetDirectories(dirs[i]), bw);
                    }
                }
                // Исключение (вероятнее всего - недостаточность прав доступа)
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }


    // Классы аргументов событий (не используются)

    // Класс аргуметов события о найденном файле
    public class FileSearchEventArgs
    {
        // Сообщение
        public string Message { get; }

        public FileSearchEventArgs(string mes)
        {
            Message = mes;
        }
    }

    // Класс аргуметов события о текущей папке при поиске
    public class CurrentDirectoryEventArgs
    {
        // Сообщение
        public string Message { get; }

        public CurrentDirectoryEventArgs(string mes)
        {
            Message = mes;
        }
    }
}
