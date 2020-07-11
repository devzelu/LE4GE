using System;
using System.IO;

namespace le4ge.Utilites.Custom
{
    internal class CustomLogger
    {
        private static void FileSaver(string type, string date, string message)
        {
            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            docPath = docPath + "\\logs\\";
            switch (type)
            {
                case "Info":
                    {
                        docPath = docPath + type;
                    }
                    break;

                case "Error":
                    {
                        docPath = docPath + type;
                    }
                    break;

                case "Success":
                    {
                        docPath = docPath + type;
                    }
                    break;

                case "Warining":
                    {
                        docPath = docPath + type;
                    }
                    break;

                case "Debug":
                    {
                        docPath = docPath + type;
                    }
                    break;

                default:
                    break;
            }
            if (!Directory.Exists(docPath))
            {
                Directory.CreateDirectory(docPath);
            }
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"{date}.txt"), true))
            {
                outputFile.WriteLine(message);
            }
        }

        internal static void Error(string message)
        {
            var time = DateTime.Now;
            var logmessage = $"[{time}][Error]{message}";
            var fileName = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(logmessage);
            FileSaver("Error", fileName.Replace("'/'", "-"), logmessage);
        }

        internal static void Info(string message)
        {
            var time = DateTime.Now;
            var logmessage = $"[{time}][Info]{message}";
            var fileName = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(logmessage);
            FileSaver("Info", fileName.Replace("'/'", "-"), logmessage);
        }

        internal static void Success(string message)
        {
            var time = DateTime.Now;
            var logmessage = $"[{time}][Success]{message}";
            var fileName = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(logmessage);
            FileSaver("Success", fileName.Replace("'/'", "-"), logmessage);
        }

        internal static void Warning(string message)
        {
            var time = DateTime.Now;
            var logmessage = $"[{time}][Warning]{message}";
            var fileName = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(logmessage);
            FileSaver("Warning", fileName.Replace("'/'", "-"), logmessage);
        }

        internal static void Debug(string message)
        {
            var time = DateTime.Now;
            var logmessage = $"[{time}][Debug]{message}";
            var fileName = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(logmessage);
            FileSaver("Debug", fileName.Replace("'/'", "-"), logmessage);
        }
    }
}