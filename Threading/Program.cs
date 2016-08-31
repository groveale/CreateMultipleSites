using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        public static List<String> sitesToProvision = new List<string>();
        public static List<String> provisionedSites = new List<string>();

        public static int sitesToProcess = 0;

        static void Main(string[] args)
        {

            

            sitesToProcess = 5;

            Task task0 = Task.Factory.StartNew(() => DoesListContainValue(sitesToProcess));

            Task task1 = Task.Factory.StartNew(() => CreateSite("Sites1", 1000, 1));
            Task task2 = Task.Factory.StartNew(() => CreateSite("Sites2", 10000, 2));
            Task task3 = Task.Factory.StartNew(() => CreateSite("Sites3", 5000, 3));
            Task task4 = Task.Factory.StartNew(() => CreateSite("Sites4", 10, 4));
            Task task5 = Task.Factory.StartNew(() => CreateSite("Sites5", 900, 5));

            Task[] tasks = { task0, task1, task2, task3, task4, task5 };
            List<Task> taskslist = new List<Task>();



            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //Task.WaitAll(taskslist.ToArray<Task>());
                Task.WaitAll(tasks);
                sw.Stop();
                Console.WriteLine(String.Format("tasks completed in {0}ms", sw.ElapsedMilliseconds));
            }
            catch
            {
                Console.WriteLine("Error");
            }


            Console.WriteLine("Provisioned Sites:");
            foreach (var site in provisionedSites)
            {
                Console.WriteLine(site);
            }
            

            Console.ReadLine();
        }


        public static void CreateSite(string name, int sleep, int thread)
        {
            Thread.Sleep(sleep);
            Console.WriteLine("SiteCreated: " + name + " - Using Thread " + thread);
            sitesToProvision.Add(name);
        }

        public static void DoesListContainValue(int sitesToProcess)
        {
            while (sitesToProcess > 0)
            {
                if (sitesToProvision.Count > 0)
                {
                    //Console.WriteLine("Provisioning Site: " + sitesToProvision[0] + " - Using Thread 0");
                    // Do provisioning
                    Thread.Sleep(1000);
                    var provisionedSite = sitesToProvision[0] + " PROVISIONED";
                    Console.WriteLine(provisionedSite + " - Using Thread 0");

                    sitesToProvision.RemoveAt(0);
                    provisionedSites.Add(provisionedSite);
                    sitesToProcess--;
                    if (sitesToProcess < 1)
                    {
                        return;
                    }
                }
            }     
        }
      
    }
}
