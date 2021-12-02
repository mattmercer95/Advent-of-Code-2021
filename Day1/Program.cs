using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    /*Advent of Code 2021: Day 1
     * 2021-12-01*/
    class Program
    {
        static void Main(string[] args)
        {
            //get array of numbers from file
            int[] numbers = ParseNumberFile("input.txt");
            //initialize increasedCounter
            int increasedCounter = 0;
            for(int i = 1; i < numbers.Length; i++)
            {
                if(numbers[i] > numbers[i - 1])
                {
                    increasedCounter++;
                }
            }
            //output Part 1 answer
            Console.WriteLine("Part 1 Answer = " + increasedCounter.ToString());

            //Part 2
            //create the new array of window sums
            int[] windows = new int[(numbers.Length/3)*3];
            //get array of widows
            for(int j = 0; j < windows.Length; j++)
            {
                //create the window (3 long)
                windows[j] = numbers[j] + numbers[j + 1] + numbers[j + 2];
            }
            Console.WriteLine("last used in window sum: " + windows[windows.Length - 1].ToString());
            //find increased amount for part 2
            int windowCounter = 0;
            for (int i = 1; i < windows.Length; i++)
            {
                if (windows[i] > windows[i - 1])
                {
                    windowCounter++;
                }
            }
            //output Part 2 answer
            Console.WriteLine("Part 2 Answer = " + windowCounter.ToString());
        }
        static int[] ParseNumberFile(string filename)
        {
            string fileContent = File.ReadAllText(filename);

            string[] integerStrings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int[] integers = new int[integerStrings.Length];

            for (int n = 0; n < integerStrings.Length; n++)
                integers[n] = int.Parse(integerStrings[n]);

            return integers;
        }
    }
}
