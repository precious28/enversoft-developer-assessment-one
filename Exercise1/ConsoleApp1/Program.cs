using System;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[] list1 = new int[] { 1, 2, 3, 4, 5 };
            int[] list2 = new int[] { 3, 4, 5, 6, 7 };

            // a. Common elements in both lists
            var list3 = list1.Intersect(list2);
            Console.WriteLine(string.Join(" ", list3));

            // b. Elements in list1 not in list2
            var list4 = list1.Except(list2);
            Console.WriteLine(string.Join(" ", list4));

            // c. Elements in list2 not in list1
            var list5 = list2.Except(list1);
            Console.WriteLine(string.Join(" ", list5));

            Console.WriteLine("Press <ENTER> to continue");
            Console.ReadLine();
        }
    }
}
