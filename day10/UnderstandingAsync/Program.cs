namespace UnderstandingAsync
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
            Console.WriteLine("Thisis also getting execuetd");
            task.Wait();
            Console.WriteLine("After the task is complete");
        }
        async Task UnderstandingAsyncAwait()
        {
            Console.WriteLine("First line in task");
            Thread.Sleep(2000);
            Console.WriteLine("Last line in task");
        }
        async Task CallTheAsyncMethod()
        {
            //Console.WriteLine("Calling the async method");
            //Task task = UnderstandingAsyncAwait();
            //Console.WriteLine("Call is done");
            //task.Wait();
            Console.WriteLine("Calling the async method");
            await UnderstandingAsyncAwait();//Completes the execution of the method and then comes back to the caller
            Console.WriteLine("Call is done");

        }
        static async Task Main(string[] args)
        {
            Program program = new Program();
            //Thread t1, t2;
            //t1 = new Thread(program.UnderstandingThreading);
            //t2 = new Thread(program.UnderstandingThreading);
            //t1.Name = "Thread 1";
            //t2.Name = "Thread 2";
            //t1.Start();
            //t2.Start();
            await program.CallTheAsyncMethod();
        }
    }
}
