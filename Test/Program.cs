using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            const double Resolution = 4.0 * 1000 / 6 / 32767;
            List<int> y = new List<int>();
            string data = File.ReadAllText(@"C:\Users\irantech24.com\source\repos\DGDena_Charts_PlayGround\ConsoleApp1\ConsoleApp2\file\a.txt");

            String[] spearator = { "Data:\r\n" };

            String[] hexValuesSplit = data.Split(spearator,
                   StringSplitOptions.RemoveEmptyEntries);

            string arr = hexValuesSplit.Skip(1).First();

            List<string> newArr = new List<string>();
            int p = 0;
            string text = null;
            for (int q = 0; q < arr.Length; q++)
            {
                if (arr[q] == '\n')
                {
                    text = arr.Substring(p, q - p - 1);
                    newArr.Add(text);
                    p = q + 1;
                }
            }

            foreach (var line in newArr)
            {

                byte[] x = new byte[64];
                var i = 0;

                foreach (string hex in line.Split(new char[] { ' ' }))
                {
                    x[i++] = Convert.ToByte(hex, 16);
                }

                byte[] a = new byte[2];
                for (int c = 12; c < x.Length; c += 19)
                {
                    a[0] = x[c + 1];
                    a[1] = x[c];
                    var g = BitConverter.ToInt16(a, 0);
                    y.Add(g);
                }

            }


            foreach (var item in y)
            {
                var t = item * Resolution;
                Console.WriteLine(t);
            }




            Console.ReadLine();
        }
    }
}
