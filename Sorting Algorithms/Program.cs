// Author: Thomas Kugelman
// Language: C#
// Description: A few simple sorting algorithms that demonstrate how important efficiency is.
// I originally wrote a program liek this for a class and it was a very eye opening experience for me.
// This is a revised version of that program.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static void InsertionSort(int[] numArray)
        {
            for (int i = 0; i < numArray.Length - 1; i++)
            {
                int value = numArray[i];
                int flag = 0;

                for (int j = i - 1; j >= 0 && flag != 1;)
                {
                    if (value < numArray[j])
                    {
                        numArray[j + 1] = numArray[j];
                        j--;
                        numArray[j + 1] = value;
                    }
                    else flag = 1;
                }
            }
        }

    }
}
