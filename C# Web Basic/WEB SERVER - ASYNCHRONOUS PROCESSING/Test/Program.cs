using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        //public Task<IActionResult> SaveData()
        //{
        //    //==
        //    await sbContext.SaveChangesAsync();

        //    return this.View();
        //}

        //
        static async Task Main(string[] args)
        {
            //78668
          //  00:00:04.0500773
            var sw = Stopwatch.StartNew();
            var count = 0;
            //for (int i = 0; i < 1000000; i++)

            //78598 => not correct answer - Parallel without lock
            // 00:00:00.8275223

            //78667 => with lock on count variable
           // 00:00:00.7447448
           var obj = new object();
            Parallel.For(1, 1000001, (i) =>
            {
                bool isPrime = true;
                for (int div = 2; div < Math.Sqrt(i); div++)
                {
                    if (i % div == 0)
                    {

                        isPrime = false;
                    }
                }

                if (isPrime)
                {
                    lock (obj)
                    {
                        count++;
                    }
                   
                }
            });

            Console.WriteLine(count);
            Console.WriteLine(sw.Elapsed);
            //await HttpClientAsync();


            //ConcurrentQueueDequeue();

            //TwoThreadsCountPrimeNumbers();
            //IncreaseMoney();

            //DeadLockCondition();

            //Task Parallel Library
            //TaskParallelLibrary();

            //BananaCode();
        }

        private static async Task HttpClientAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpResponse = await httpClient.GetAsync("http://softuni2.bg");
                var result = await httpResponse.Content.ReadAsStringAsync();

                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("jkgkg");
        }

        private static void BananaCode()
        {
            var httpClient = new HttpClient();
            httpClient.GetAsync("http://softuni.bg")
                .ContinueWith((previousTask) =>
                {
                    previousTask.Result.Content.ReadAsStringAsync()
                        .ContinueWith((readAsStringTask) =>
                        {
                            //readAsStringTask.Exception.Message;
                            if (!readAsStringTask.IsFaulted)
                            {
                                Console.WriteLine(readAsStringTask.Result);
                            }
                        });
                });
        }

        private static void TaskParallelLibrary()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine(i);
                }
            }).ContinueWith((previousTasK) =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine(i);
                }
            });

            Task.Run(() =>
            {
                for (int i = 1000; i < 2000; i++)
                {
                    Console.WriteLine(i);
                }
            });

            while (true)
            {
                var line = Console.ReadLine();
            }
        }

        private static void DeadLockCondition()
        {
            var lockObj1 = new object();
            var lockObj2 = new object();

            var thread1 = new Thread(() =>
            {
                lock (lockObj1)
                {
                    Thread.Sleep(1000);
                    lock (lockObj2)
                    {
                    }
                }
            });
            var thread2 = new Thread(() =>
            {
                lock (lockObj2)
                {
                    Thread.Sleep(1000);
                    lock (lockObj1)
                    {
                    }
                }
            });

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
        }

        private static void IncreaseMoney()
        {
            object lockobj = new object();
            decimal money = 0M;
            ThreadStart incrementMyMoney = () =>
            //var thread1 = new Thread(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock (lockobj)
                    {
                        money++;
                    }
                }
            };
            var thread1 = new Thread(incrementMyMoney);
            thread1.Start();
            var thread2 = new Thread(incrementMyMoney);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            Console.WriteLine(money);

            Thread thread = new Thread(MyThreadMainMethod);
            thread.Start();

            while (true)
            {
                var line = Console.ReadLine();
                Console.WriteLine(line.ToUpper());
            }
        }

        private static void TwoThreadsCountPrimeNumbers()
        {
            object lockobj = new object();
            int count = 0;
            var thread1 = new Thread(() =>
            {
                for (int j = 1; j < 500000; j++)
                {
                    bool isPrime = true;
                    for (int div = 2; div < Math.Sqrt(j); div++)
                    {
                        if (j % div == 0)
                        {
                            isPrime = false;
                        }
                    }

                    if (isPrime)
                    {
                        lock (lockobj)
                        {
                            count++;
                        }
                    }
                }
            });

            thread1.Start();

            var thread2 = new Thread(() =>
            {
                for (int j = 500001; j < 1000000; j++)
                {
                    bool isPrime = true;
                    for (int div = 2; div < Math.Sqrt(j); div++)
                    {
                        if (j % div == 0)
                        {
                            isPrime = false;
                        }
                    }

                    if (isPrime)
                    {
                        lock (lockobj)
                        {
                            count++;
                        }
                    }
                }
            });

            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine(count);
        }

        private static void ConcurrentQueueDequeue()
        {
            var numbers = new ConcurrentQueue<int>(Enumerable.Range(0, 10000).ToList());
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    while (numbers.Count > 0)
                        numbers.TryDequeue(out _);
                }).Start();
            }
        }

        private static void MyThreadMainMethod()
        {
            var stopWatch = Stopwatch.StartNew();
            Console.WriteLine(CountPrimeNumbers(1, 1000000));
            Console.WriteLine(stopWatch.Elapsed);
        }

        private static int CountPrimeNumbers(int @from, int to)
        {
            int count = 0;
            for (int j = from; j < to; j++)
            {
                bool isPrime = true;
                for (int div = 2; div < Math.Sqrt(j); div++)
                {
                    if (j % div == 0)
                    {
                        isPrime = false;
                    }
                }

                if (isPrime)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
