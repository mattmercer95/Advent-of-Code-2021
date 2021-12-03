using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part I

            //get txt input
            string[] binaryNums = ParseNumberFile("input.txt");

            //create 2d array
            char[,] binaryMatrix = new char[binaryNums.Length, 12];

            int columnCounter;
            //loop through the array
            for (int i = 0; i < binaryNums.Length; i++)
            {
                //set the counter to zero
                columnCounter = 0;
                //loop through the chars at each binary string, add to matrix
                foreach (char c in binaryNums[i])
                {
                    binaryMatrix[i, columnCounter] = c;
                    columnCounter++;
                }
            }

            //get gammaRate
            int[] gammaRate = new int[12];
            //declare 1 and 0 counters
            int one, zero;
            //loop through each digit position
            for (int i = 0; i < 12; i++)
            {
                //reset digit counters
                one = 0;
                zero = 0;
                //loop through each binary number
                for (int j = 0; j < binaryNums.Length; j++)
                {
                    //increment counter based on digit
                    switch(binaryMatrix[j, i])
                    {
                        case '1':
                            one++;
                            break;
                        case '0':
                            zero++;
                            break;
                        default:
                            break;
                    }
                }
                //put most occuring digit in gammaRate
                if (one > zero)
                    gammaRate[i] = 1;
                else
                    gammaRate[i] = 0;
            }

            //create epsilonRate
            int[] epsilonRate = new int[12];
            //set epsilonRate to the complement of gammaRate
            for (int i = 0; i < gammaRate.Length; i++)
            {
                //flip the bits
                switch (gammaRate[i])
                {
                    case 1:
                        epsilonRate[i] = 0;
                        break;
                    case 0:
                        epsilonRate[i] = 1;
                        break;
                    default:
                        break;
                }
            }

            //now, to calc power consumpstion, multiply them together
            //first, convert them to decimal numbers
            //convert gamma rate to decimal
            int gammaDecimal = 0;
            int digitCounter = 0;
            for (int i = gammaRate.Length-1; i >=0; i--)
            {
                gammaDecimal += gammaRate[i] * (int) Math.Pow(2, digitCounter);
                digitCounter++;
            }
            //convert epsilon rate to decimal
            int epsilonDecimal = 0;
            digitCounter = 0;
            for (int i = epsilonRate.Length - 1; i >= 0; i--)
            {
                epsilonDecimal += epsilonRate[i] * (int)Math.Pow(2, digitCounter);
                digitCounter++;
            }

            //calculate power consumption rate
            int powerConsumption = gammaDecimal * epsilonDecimal;

            //switch gammaRates to chars for output
            char[] gammaChars = new char[12];
            for (int i = 0; i < gammaRate.Length; i++)
            {
                switch (gammaRate[i])
                {
                    case 1:
                        gammaChars[i] = '1';
                        break;
                    case 0:
                        gammaChars[i] = '0';
                        break;
                    default:
                        break;
                }
            }

            //switch epsilonRates to chars for output
            char[] epsilonChars = new char[12];
            for (int i = 0; i < epsilonRate.Length; i++)
            {
                switch (epsilonRate[i])
                {
                    case 1:
                        epsilonChars[i] = '1';
                        break;
                    case 0:
                        epsilonChars[i] = '0';
                        break;
                    default:
                        break;
                }
            }

            //print Part 1 output
            Console.WriteLine("Part 1 Output:"+"\n"+"gamma rate: ");
            Console.WriteLine(gammaChars);
            Console.WriteLine("gamma decimal: ");
            Console.WriteLine(gammaDecimal);
            Console.WriteLine("epsilon rate: ");
            Console.WriteLine(epsilonChars);
            Console.WriteLine("epsilon decimal: ");
            Console.WriteLine(epsilonDecimal);
            Console.WriteLine("power consumption: ");
            Console.WriteLine(powerConsumption);

            //Part II

            //create an oxygen list and a c02 list
            List<String> oxygenList = new List<string>(binaryNums);
            List<String> c02 = new List<string>(binaryNums);

            //find oxygen generator rating
            char charflag;
            for (int i = 0; i < 12; i++)
            {
                //if there is only 1 element left in the list, stop the loop
                if (oxygenList.Count == 1)
                    break;
                //count 1 and 0 occurances
                one = 0;
                zero = 0;
                foreach (string line in oxygenList)
                {
                    switch (line[i])
                    {
                        case '1':
                            one++;
                            break;
                        case '0':
                            zero++;
                            break;
                    }
                }
                //set the charflag to the most common digit
                if(one >= zero)
                {
                    charflag = '1';
                }
                else
                {
                    charflag = '0';
                }
                //remove the binary nums with the lesser occuring digits
                foreach (string item in oxygenList.ToList())
                {
                    if (item[i] != charflag)
                        oxygenList.Remove(item);
                }
            }

            //find c02 generator rating
            for (int i = 0; i < 12; i++)
            {
                //if there is only 1 element left in the list, stop the loop
                if (c02.Count == 1)
                    break;
                //count 1 and 0 occurances
                one = 0;
                zero = 0;
                foreach (string line in c02)
                {
                    switch (line[i])
                    {
                        case '1':
                            one++;
                            break;
                        case '0':
                            zero++;
                            break;
                    }
                }
                //set the charflag to the least common digit
                if (zero <= one)
                {
                    charflag = '0';
                }
                else
                {
                    charflag = '1';
                }
                //remove the binary nums with the greater occuring digits
                foreach (string item in c02.ToList())
                {
                    if (item[i] != charflag)
                        c02.Remove(item);
                }
            }

            //convert ratings to decimal
            int oxygenDecimal = BinToDec(oxygenList[0]);
            int carbonDecimal = BinToDec(c02[0]);

            //finally, find the life support rating
            int lifeSupport = oxygenDecimal * carbonDecimal;

            //print Part 2 output
            Console.WriteLine("Part 2 Output:" + "\n" + "oxygenList: ");
            foreach (string item in oxygenList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Decimal Value: " + oxygenDecimal);
            Console.WriteLine("C02 List: ");
            foreach (string item in c02)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Decimal Value: " + carbonDecimal);
            Console.WriteLine("Life Support Rating: " + lifeSupport);
        }

        static string[] ParseNumberFile(string filename)
        {
            string fileContent = File.ReadAllText(filename);

            string[] integerStrings = fileContent.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return integerStrings;
        }

        static int BinToDec(string binNum)
        {
            int dec = 0, digitCounter = 0;
            for (int i = binNum.Length - 1; i >= 0; i--)
            {
                dec += (int)Char.GetNumericValue(binNum[i]) * (int)Math.Pow(2, digitCounter);
                digitCounter++;
            }
            return dec;
        }
    }
}
