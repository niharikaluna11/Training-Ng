namespace UnderstandingThreads
{
    internal class Program
    {

        void UnderstandingThreading()
        {
            lock (this)
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"The thread {Thread.CurrentThread.Name} is printing {i}");
                }
            }
        }
        void UndertsandingUsageOfWaitTime()
        {
            for (int i = 10; i < 101; i = i + 10)
            {
                if (i == 50)
                {
                    Thread.Sleep(4000);//mimics a wait for a process to complete
                }
                Console.WriteLine(i);
            }
        }

        void UnderstandingTask()
        {
            Task task = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"The task is printing {i}");
                }
            });

            Console.WriteLine("Created teh task");
            task.Start();
            Console.WriteLine("helo");
            task.Wait(7888);
            Console.WriteLine("After the task is complete");
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            //Thread t1, t2;
            //t1 = new Thread(program.UnderstandingThreading);
            //t2 = new Thread(program.UnderstandingThreading);
            //t1.Name = "Thread 1";
            //t2.Name = "Thread 2";
            //t1.Start();
            //t2.Start();
            program.UnderstandingTask();
        }
    }
}
