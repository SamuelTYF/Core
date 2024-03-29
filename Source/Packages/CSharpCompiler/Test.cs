﻿using System;

namespace Test
{
    public class Program
    {
        public static int MainVersion;
        public static int MajorVersion;
        public static int SubVersion;
        public void Test()
        {
            Console.WriteLine(0);
            Console.WriteLine(123);
            Console.WriteLine(-987);
            Console.WriteLine(0x12aF);
            Console.WriteLine(0b101);
            Console.WriteLine(0.1e001);
            Console.WriteLine(-12.1e-12);
            Console.WriteLine(@"12\t\0\\");
            Console.WriteLine('\'');
            Console.WriteLine('"');
            Console.WriteLine('\u0001');
            Console.WriteLine('a');
            Console.WriteLine("12\t\n");
        }
        public static void Main()
        {
            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());
            int d = GCD(a, b);
            Console.WriteLine(d);
            if (d == 1) Console.WriteLine("Is Relatively Prime");
            else Console.WriteLine("Is Not Relatively Prime");
            for (int i = 0; i < 100; i = i + 1)
                Console.WriteLine(i);
            string line;
            do
            {
                line = Console.ReadLine();
                Console.WriteLine(line);
            } while (line.Length != 0);
            while (a < b)
                Console.WriteLine(DateTime.Now);
        }
        public static int GCD(int a, int b) => false || b > 0 ? GCD(b, a % b) : a;
    }
}