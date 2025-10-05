using System.Runtime.CompilerServices;

namespace _03_SPThreadSynchronization
{
    class Statistic
    {
        public static int Letters { get; set; }
        public static int Digits { get; set; }
        public static int Punctuation { get; set; }
    }

    internal class Program
    {
        static void Analyzer(object text)
        {
            Statistic st = new Statistic();

            string txt = (string)text;
            foreach (var elem in txt)
            {
                lock (typeof(Statistic))
                {
                    if (Char.IsLetter(elem)) { Statistic.Letters++; }
                    else if (Char.IsDigit(elem)) { Statistic.Digits++; }
                    else if (Char.IsPunctuation(elem)) { Statistic.Punctuation++; }
                }
            }
        }
        static void Main(string[] args)
        {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] files = Directory.GetFiles($@"{desktopPath}\test");

            Thread[] threads = new Thread[files.Length];

            int i = 0;
            foreach (var f in files)
            {
                Console.WriteLine($"\n{f}");
                string text = File.ReadAllText(f);
                Console.WriteLine(text);
                threads[i] = new Thread(Analyzer);
                threads[i].Start(text as object);
                i++; 
            }
            foreach (var t in threads)
            {
                t.Join();
            }
            Console.WriteLine($"\nNumber of Letters: {Statistic.Letters}");
            Console.WriteLine($"Number of Digits: {Statistic.Digits}");
            Console.WriteLine($"Number of Punctuation marks: {Statistic.Punctuation}");
        }
        
    }
}
