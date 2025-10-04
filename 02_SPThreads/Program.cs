using System.Text;

namespace _02_SPThreads
{
    internal class Program
    {
        static public void Nums150()
        {
            Console.WriteLine("Thread Started!");
            for (int i = 0; i <= 50; i++) {
                Console.WriteLine("\t"+i);
            }
        }
        static public void NumsWithRange(object? tup)
        {
            Console.WriteLine("Thread Started!");
            Tuple<int, int> tuple = (Tuple<int, int>)tup;
            for (int i = tuple.Item1; i < tuple.Item2; i++)
            {
                Console.WriteLine("\t\t"+i);
            }

        }
        static public void ThreadMax(object? array)
        {
            int[] arr = (int[])array;

            Console.WriteLine($"Max element: {arr.Max()}");
        }
        static public void ThreadMin(object? array)
        {
            int[] arr = (int[])array;

            Console.WriteLine($"Min element: {arr.Min()}");
        }
        static public void ThreadAvg(object? array)
        {
            int[] arr = (int[])array;

            Console.WriteLine($"Avg: {arr.Average()}");
        }

        static void WriteFile(object? array)
        {
            Console.WriteLine("File Writing thread started");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Calculations.txt");
            int[] arr = (int[])array;
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {

                string writeText = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    writeText += arr[i] + ", ";
                }
                writeText += $"\nMaximum: {arr.Max()}\nMinimum: {arr.Min()}\nAvg: {arr.Average()}";
                byte[] writeBites = Encoding.UTF8.GetBytes(writeText);
                fs.Write(writeBites, 0, writeBites.Length);

                Console.WriteLine("File was recorded!!!!");

            }
        }
            static void Main(string[] args)
        {
            Console.WriteLine("Task 1");
            Thread th = new Thread(Nums150);
            th.Start(); th.Join();

            Console.WriteLine("\nTask 2");
            Console.Write("Enter begin: "); int beg = int.Parse(Console.ReadLine()!);
            Console.Write("Enter end: "); int end = int.Parse(Console.ReadLine()!);
            Tuple<int, int> tuple = new Tuple<int, int>(beg, end);

            Thread th2 = new Thread(NumsWithRange);
            th2.Start(tuple as object); th2.Join();

            Console.WriteLine("\nTask 3");
            Console.Write("Enter number of Threads: "); int num = int.Parse(Console.ReadLine()!);
            Console.Write("Enter begin: "); int beg2 = int.Parse(Console.ReadLine()!);
            Console.Write("Enter end: "); int end2 = int.Parse(Console.ReadLine()!);
            Tuple<int, int> tuple2 = new Tuple<int, int>(beg2, end2);

            for (int i = 0; i < num; i++) { 
                Thread th3 = new Thread(NumsWithRange);
                th3.Start(tuple2 as object);
                th3.Join();
            }


            Console.WriteLine("\nTask 4");
            Random random = new Random();
            int[]arr = new int[100];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(-50, 50);
            }

            Thread thmax = new Thread(ThreadMax);
            thmax.Start(arr as object);
            Thread thmin = new Thread(ThreadMin);
            thmin.Start(arr as object);
            Thread thavg = new Thread(ThreadAvg);
            thavg.Start(arr as object);

            thmax.Join();thmin.Join();thavg.Join();
            Console.WriteLine("\nTask 5");
            
            Thread thwrite = new Thread(WriteFile);
            thwrite.Start(arr as object);


        }
    }
}
