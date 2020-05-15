// Author: Thomas Kugelman
// Language: C#
// Description: A few simple sorting algorithms that demonstrate how important efficiency is.
// I originally wrote a program like this for a class and it was a very eye opening experience for me.
// This is a revised version of that program.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            int MAX = 100;
            int range = 1000;
            Stopwatch stopWatch = new Stopwatch();

            int[] array = FillArray(MAX, range);

            // Array.Sort(array);
            for (int i = 0; i < array.Length; i++)
                Console.Write(array[i].ToString() + " ");
        }

        private void InsertionSort(int[] numArray)
        {
            // Loops through entire array
            for (int i = 0; i < numArray.Length - 1; i++)
            {
                int value = numArray[i];

                for (int j = i - 1; j >= 0 && flag != 1;)
                {
                    if (value < numArray[j])
                    {
                        numArray[j + 1] = numArray[j];
                        j--;
                        numArray[j + 1] = value;
                    }
                }
            }
        }



        // fills an array of 100 integers with unique numbers between 0 and 1,000
        private static int[] FillArray(int MAX, int range)
        {
            int[] uniqueArray = new int[MAX];
            int index = 0;

            uniqueArray = GetUniqueInt(uniqueArray, MAX, range, index);

            return uniqueArray;
        }

        private static int[] GetUniqueInt(int[] uniqueArray, int MAX, int range, int index)
        {
            // If we have filled the array with unique numbers up to the <MAX> we have filled the array
            if (index <= MAX - 1)
            {
                Random random = new Random();
                bool goodNum = false;

                while (!goodNum)
                {
                    
                    int num = random.Next(0, range);

                    int foundNum = Array.IndexOf(uniqueArray, num);

                    if (foundNum < 0)
                    {
                        uniqueArray[index] = num;
                        index++;
                        goodNum = true;
                    }
                    else
                    {
                        goodNum = false;
                    }
                }

                // Recursively call this function to add more unique numbers
                uniqueArray = GetUniqueInt(uniqueArray, MAX, range, index);
                return uniqueArray;
            }
            // When we get here we have filled the array, return it to the previous recursive call and so on. THIS IS OUR EXIT CONDITION
            else
            {
                return uniqueArray;
            }
        }
    }
}
