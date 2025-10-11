namespace _04_SPTasks
{
    internal class Program
    {
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static int[] generateArr()
        {
            Random random = new Random();
            var arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(-50, 50);
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            return arr;
        }
        public static int[] arrDistinct(int[] arr)
        {
            arr = arr.Distinct().ToArray();
            foreach (int i in arr)
            {
                Console.Write(i +" ");
            }
            Console.WriteLine();
            return arr;
        }
        public static int[] arrSort(int[] arr)
        {
            Array.Sort(arr);
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            return arr;
        }
        public static void binarySearch(int[] arr)
        {
            Console.Write("Enter value to find: "); int val = int.Parse(Console.ReadLine()!);
            int index = Array.BinarySearch(arr, val);
            if (index > 0)
            {
                Console.WriteLine($"Found {val} at index: {index}");
            }
            else { Console.WriteLine($"{val} wasn't found in array"); }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 1");
            Task t1 = new Task(() => Console.WriteLine($"Time with Task start: {DateTime.Now }"));
            t1.Start();
            Task t2 = Task.Factory.StartNew(() => Console.WriteLine($"Time with Task startNew: {DateTime.Now }"));
            Task t3 =  Task.Run(() => Console.WriteLine($"Time with Task Run: {DateTime.Now}"));
            Console.ReadKey(true);

            Console.WriteLine("\nTask 2");
            Task t4 = Task.Run(() =>
            {
                for (int i = 0; i <= 1000; i++)
                {
                    if (IsPrime(i)) { Console.Write(i + " "); }
                }
            });
            Console.ReadKey(true);

            Console.WriteLine("\nTask 3");
            Task t5 = new Task(() => {
                Console.Write("Enter start: "); int start = int.Parse(Console.ReadLine()!); Console.Write("Enter end: "); int end = int.Parse(Console.ReadLine()!); int j = 0;
                for (int i = start; i <= end; i++)
                {
                    if (IsPrime(i)) { Console.Write(i + " "); j++; }
                }
                Console.WriteLine($"\nNumber of Prime numbers: {j}");
            });
            t5.Start();
            t5.Wait();
            Console.ReadKey(true);


            Console.WriteLine("\nTask 4");   

            Random random = new Random();
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(-50, 50);
            }
            Task[] tasks = new Task[4]
            {
                new Task(()=>Console.WriteLine($"Minimum element: {arr.Min()}")),
                new Task(()=> Console.WriteLine($"Maximum element: {arr.Max()}")),
                new Task(()=> Console.WriteLine($"Avg of array: {arr.Average()}")),
                new Task(() => Console.WriteLine($"Sum of array: {arr.Sum()}"))               
            };
            foreach (var t in tasks) { t.Start(); }
            Console.ReadKey(true);

            Console.WriteLine("\nTask 5");
            Task<int[]> t7 = Task.Run(() => generateArr());
            Task<int[]> t8 = t7.ContinueWith(prevtask=>arrDistinct(prevtask.Result)).ContinueWith(prevtask2=>arrSort(prevtask2.Result));
            Task t9 = t8.ContinueWith(prevtask3 => binarySearch(prevtask3.Result));
            t9.Wait();
        }
    }
}
