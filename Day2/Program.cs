using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            //get array of directions from file
            string[] directions = ParseNumberFile("input.txt");
            Console.WriteLine(directions[0]);
            //set up number array
            int[] numbers = new int[directions.Length];
            //loop through directions
            string currentLine;
            int convertedNum;
            for(int i = 0; i < directions.Length; i++)
            {
                //for each direction, pull out number and add it to the number array
                string num = string.Empty;
                currentLine = directions[i];
                //pull number from current line
                for(int j = 0; j < currentLine.Length; j++)
                {
                    if (Char.IsDigit(currentLine[j]))
                        num += currentLine[j];
                }
                //convert to int and put into array
                convertedNum = int.Parse(num);
                numbers[i] = convertedNum;
            }
            Console.WriteLine(numbers[numbers.Length-1]);

            //Once we have our data set up, make the calculation
            string testDirection;
            long horizontal = 0, depth = 0, aim = 0;
            for(int k = 0; k < directions.Length; k++)
            {
                //get first character of the current direction
                testDirection = directions[k].Substring(0, 1);
                switch (testDirection)
                {
                    //change value depending on direction
                    case "f":
                        horizontal += numbers[k];
                        depth = depth + numbers[k] * aim;
                        break;
                    case "d":
                        aim += numbers[k];
                        break;
                    case "u":
                        aim -= numbers[k];
                        break;
                    default:
                        break;
                }
            }

            //print output
            Console.WriteLine("horizontal: " + horizontal);
            Console.WriteLine("depth: " + depth);
            long product = horizontal * depth;
            Console.WriteLine("product: " + product);
        }
        static string[] ParseNumberFile(string filename)
        {
            string fileContent = File.ReadAllText(filename);

            string[] integerStrings = fileContent.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return integerStrings;
        }
    }
}
