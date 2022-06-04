using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 7; i++)
            {
                Animal Dragon = new Animal(i);
            }
            Thread.Sleep(20000);
            Console.WriteLine("Драконы сыты");
            Console.ReadKey();
        }
        class Animal
        {
            static Semaphore sem = new Semaphore(4, 4);
            Thread Threading;
            int fullness = 0; //Уровень сытости
            int cout = 4;

            public Animal(int i)
            {
                Threading = new Thread(Eating);
                Threading.Name = $"Дракон {i}";
                Threading.Start();
            }

            public void Eating()
            {
                while (cout > 0)
                {
                    sem.WaitOne();
                    while (fullness != 100)
                    {
                        if (fullness == 0)
                        {
                            Console.WriteLine($"{Thread.CurrentThread.Name} прилетел на кормешку");

                        }
                        else Console.WriteLine($"{Thread.CurrentThread.Name} сыт на {fullness} % из 100 %");
                        Thread.Sleep(1000);
                        fullness += 20;
                        if (fullness == 100)
                        {
                            Console.WriteLine($"{Thread.CurrentThread.Name} наелся");
                            Console.WriteLine($"{Thread.CurrentThread.Name} улетает в пещеру");
                        }

                        cout--;
                        Thread.Sleep(1000);
                    }
                    sem.Release();
                }
            }

        }
    }
}
