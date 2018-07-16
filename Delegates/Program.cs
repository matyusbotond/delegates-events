using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    delegate bool FilterDelegate(int num);

    delegate void FoundNumber(int num);

    class FakeAlgorithm
    {
        public void RemoveAllSubscriber()
        {
            //Program.RaiseFoundNumber(3);
            //Program.RaiseFoundNumber = HandleMagicNumber;
            Program.RaiseFoundNumber += HandleMagicNumber;
        }

        private void HandleMagicNumber(int num)
        {
            Console.WriteLine("Találtam egy számot");
        }
    }

    class Person
    {
        public int Age { get; set; }

        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }

        public void HandleFoundNumber(int num)
        {
            Console.WriteLine("{0} a nevem és megkaptam a magic számot! {1}", Name, num);
        }
    }

    class Program
    {
        public static event FoundNumber RaiseFoundNumber;

        static void Main(string[] args)
        {
            Person p1 = new Person("Béla");
            Person p2 = new Person("Géza");

            //FoundNumber[] subscriptions = new FoundNumber[2];

            //subscriptions[0] = p1.HandleFoundNumber;

            //foreach (FoundNumber fn in subscriptions)
            //{
            //    fn(2);
            //}

            //RaiseFoundNumber += new FoundNumber(p1.HandleFoundNumber);
            RaiseFoundNumber += p1.HandleFoundNumber;
            RaiseFoundNumber += p2.HandleFoundNumber;

            //FakeAlgorithm f = new FakeAlgorithm();

            //f.RemoveAllSubscriber();

            int[] nums = new int[]
            {
                2, 4, 5, 3, 6,
            };

            //RunAlgorithm(nums, new FilterDelegate(IsOdd));

            RunAlgorithm(nums, IsOdd);

            RaiseFoundNumber -= p1.HandleFoundNumber;
         
            Console.ReadKey();

            RunAlgorithm(nums, IsEven);
         
            Console.ReadKey();
        }

        private static void RunAlgorithm(int[] nums, FilterDelegate filter)
        {
            if (filter == null)
            {
                return;
            }

            foreach (int num in nums)
            {
                if (filter(num))
                {
                    //Console.WriteLine("A szűrési feltételnek megfelel! {0}", num);
                    if (RaiseFoundNumber != null)
                    {
                        RaiseFoundNumber(num);
                    }
                }
            }
        }

        static bool IsEven(int num)
        {
            return num % 2 == 0;
        }

        static bool IsOdd(int num)
        {
            //return num % 2 != 0; Ennek felel meg
            return !IsEven(num);
        }
    }
}
