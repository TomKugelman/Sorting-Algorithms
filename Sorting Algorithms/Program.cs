// Author: Thomas Kugelman
// Language: C#
// Framework: .NET 4.7.2
//-------------------------------------------------------------------------------
// Description: A few simple sorting algorithms that demonstrate how important efficiency is.
// I originally wrote a program like this for a class and it was a very eye opening experience for me.
// This is a revised version of that program.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sorting_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Constants
            int MAX = 6000;
            int range = 10000;
            Stopwatch stopWatch = new Stopwatch();
            #endregion

            #region Data Members
            // Call function to fill array of <MAX> size within the <range> of numbers
            // NOTE: This fills with unique numbers only!
            // <range> MUST be HIGHER than <MAX>
            int[] array = FillArray(MAX, range);

            /* My original program had each sorting function run one at a time via a while loop, cycling to the next
            sorting function after every loop iteration. I chose to instead create a seperate array for each sorting function that copies the original
            <array>. While this takes up more total memory, for the purpose of this project I prefer using a more obvious separation of each function.*/
            int[] insertionSortArray = new int[MAX];
            int[] bubbleSortArray = new int[MAX];
            int[] quickSortArray = new int[MAX];
            int[] mergeSortArray = new int[MAX];
            int[] selectionSortArray = new int[MAX];

            // Variables to store time elapsed after each sorting/searching function
            string insertionTime = "";
            string bubbleTime = "";
            string quickTime = "";
            string selectionTime = "";
            string mergeTime = "";

            string linearTime = "";
            string binaryTime = "";

            string linearResult = "";
            string binaryResult = "";

            string[] sortTimerArray = { insertionTime, bubbleTime, quickTime, selectionTime, mergeTime };
            string[] searchTimerArray = { linearTime, binaryTime };

            string[] sortingFunctionNames = { "Insertion Sort", "Bubble Sort", "Quick Sort", "Selection Sort", "Merge Sort" };
            string[] searchFunctionNames = { "Linear Search", "Binary Search" };

            // An array of the sorted arrays, will make outputting easier.
            int[][] sortedArrays = {
                insertionSortArray, quickSortArray,
                bubbleSortArray, selectionSortArray, mergeSortArray
            };

            string[] searchResults = { linearResult, binaryResult };

            // Using a list of functions to process each array without duplicate code.
            List<Func<int[], int[]>> SortingFunctions = new List<Func<int[], int[]>>
            {
                InsertionSort,
                BubbleSort,
                QuickSort,
                SelectionSort,
                MergeSort
            };
            #endregion

            #region Process arrays
            // Copy the original <array> to new arrays which will be sorted
            for (int index = 0; index < sortedArrays.Length; index++)
            {
                for (int innerIndex = 0; innerIndex < sortedArrays[index].Length; innerIndex++)
                {
                    sortedArrays[index][innerIndex] = array[innerIndex];
                }
            }

            // This is neat as it allows me to process all of my not yet sorted <sortedArrays> without having to type code for each method.
            // However, it is extremely rigid and requires two arrays (sortTimerArray & sortedArrays) to line up perfectly.
            int sortingIndex = 0;
            foreach (Func<int[], int[]> method in SortingFunctions)
            {
                stopWatch.Start();
                // Call sorting function
                sortedArrays[sortingIndex] = method(sortedArrays[sortingIndex]);
                stopWatch.Stop();
                // Save runtime of each function
                sortTimerArray[sortingIndex] = stopWatch.Elapsed.ToString();
                stopWatch.Reset();

                sortingIndex++;
            }

            #endregion

            #region Output
            // Output original unsorted <array>
            Console.WriteLine("Original unsorted array\n");
            for (int index = 0; index < array.Length; index++)
            {
                Console.Write(array[index].ToString() + " ");
            }
            Console.WriteLine("\n---------------------------------------------------------------------------------------\n");

            // Output <sortedArrays>, the type of sorting algorithm, and their runtime
            for (int index = 0; index < sortedArrays.Length; index++)
            {
                Console.WriteLine(sortingFunctionNames[index]);
                Console.WriteLine(sortTimerArray[index] + "\n");
                for (int innerIndex = 0; innerIndex < sortedArrays[index].Length; innerIndex++)
                {
                    Console.Write(sortedArrays[index][innerIndex].ToString() + " ");
                }
                Console.WriteLine("\n---------------------------------------------------------------------------------------\n");
            }

            // Get input for searching functions
            bool validInput = false;
            int input = 0;

            while (!validInput)
            {
                Console.WriteLine("\nWhat positive integer would you like to search for?\n");
                try
                {                    
                    input = Convert.ToInt32(Console.ReadLine());

                    validInput = true;
                    break;
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is OverflowException)
                    {
                        validInput = false;
                        Console.WriteLine("Invalid Input! Please try again.");
                    }
                }
            }

            // Call searching functions
            

            stopWatch.Start();
            searchResults[0] = LinearSearch(insertionSortArray, input);
            stopWatch.Stop();
            searchTimerArray[0] = stopWatch.Elapsed.ToString();
            stopWatch.Reset();

            stopWatch.Start();
            searchResults[1] = BinarySearch(insertionSortArray, input);
            stopWatch.Stop();
            searchTimerArray[1] = stopWatch.Elapsed.ToString();
            stopWatch.Reset();


            // Output searching function results
            for (int index = 0; index < searchFunctionNames.Length; index++)
            {
                Console.WriteLine("\n" + searchFunctionNames[index] + "\n" + searchTimerArray[index] + "\n\n"
                    + searchResults[index]);
                Console.WriteLine("\n---------------------------------------------------------------------------------------\n");
            }

            // Reiterate sorting function runtimes
            for (int index = 0; index < sortingFunctionNames.Length; index++)
            {
                Console.WriteLine("\n" + sortingFunctionNames[index] + " runtime: " + sortTimerArray[index]);
            }

            Console.ReadLine();
            #endregion
        }

        #region Searching Functions
        private static string LinearSearch(int[] numArray, int input)
        {
            int targetIndex = 0;
            bool foundInteger = false;
            string result;

            // Loops through array and at each index compares it tot eh user's <input>
            for (int index = 0; index < numArray.Length; index++)
            {
                if (numArray[index] == input)
                {
                    targetIndex = index;
                    foundInteger = true;
                    break;
                }
            }

            // Either return a string declaring the position of the searched integer or declare that it is not present
            if (foundInteger)
            {
                result = "The integer you searched for is at index " + targetIndex.ToString() + ".";
                return result;
            }
            else
            {
                result = "The integer you searched for is not in the array!";
                return result;
            }
        }

        private static string BinarySearch(int[] numArray, int input)
        {
            int middleIndex = numArray.Length / 2;
            int[] childArray;
            string result;

            if (numArray[middleIndex] == input)
            {
                return "The integer you searched for is in the array!";
            }
            
            if (numArray[0] == input)
            {
                result = "The integer you searched for is in the array!";
                return result;
            }
            else if (numArray.Length == 1 && numArray[0] != input)
            {
                result = "The integer you searched for is not present.";
                return result;
            }

            if (input < numArray[middleIndex])
            {
                childArray = new int[middleIndex];
                for (int index = 0; index < childArray.Length; index++)
                {
                    childArray[index] = numArray[index];
                }
            }
            else
            {
                childArray = new int[numArray.Length - middleIndex];
                for (int index = 0; index < childArray.Length && middleIndex < numArray.Length; index++)
                {
                    childArray[index] = numArray[middleIndex];
                    middleIndex++;
                }
            }

            result = BinarySearch(childArray, input);
            return result;
        }


        #endregion

        #region Sorting Functions

        private static int[] MergeSort(int[] numArray)
        {
            if (numArray.Length <= 1)
                return numArray;

            int[] leftArray = new int[numArray.Length / 2];
            int[] rightArray = new int[numArray.Length - (numArray.Length / 2)];

            int middleIndex = (numArray.Length / 2);

            // Populate both subarrays
            for (int index = 0; index < middleIndex; index++)
            {
                leftArray[index] = numArray[index];
            }

            for (int index = 0; middleIndex < numArray.Length; index++)
            {
                rightArray[index] = numArray[middleIndex];
                middleIndex++;
            }

            leftArray = MergeSort(leftArray);
            rightArray = MergeSort(rightArray);

            return Merge(leftArray, rightArray);
        }

        private static int[] Merge(int[] leftArray, int[] rightArray)
        {
            int resultLength = rightArray.Length + leftArray.Length;
            int[] result = new int[resultLength];

            // These will "move the head" of the array we are working on.
            int indexLeft = 0;
            int indexRight = 0;
            int indexResult = 0;

            bool leftDone = false;
            bool rightDone = false;
            bool allDone = false;

            while (!allDone)
            {
                // Check if either side has merged its full array, if both have finished break the loop.
                if (indexLeft == leftArray.Length)
                    leftDone = true;
                if (indexRight == rightArray.Length)
                    rightDone = true;
                if (rightDone && leftDone)
                {
                    allDone = true; // This is redundant, but not important enough to change
                    break;
                }

                // Check which side's current head index has the lowest integer, 
                // then add that side to final array and incrament the chosen side and the final array.
                if (leftDone && !rightDone)
                {
                    result[indexResult] = rightArray[indexRight];
                    indexRight++;
                    indexResult++;
                }
                else if (rightDone && !leftDone)
                {
                    result[indexResult] = leftArray[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (leftArray[indexLeft] < rightArray[indexRight])
                {
                    result[indexResult] = leftArray[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (rightArray[indexRight] < leftArray[indexLeft])
                {
                    result[indexResult] = rightArray[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }

            return result;
        }

        private static int[] SelectionSort(int[] numArray)
        {
            // Loop through array (N) times where (N) equals <numArray.Length>
            // Start of unordered subarray is equal to <index>
            for (int index = 0; index < numArray.Length; index++)
            {
                int smallest = index;
                // Find index of the smallest integer in array
                for (int innerIndex = index; innerIndex < numArray.Length; innerIndex++)
                {
                    if (numArray[innerIndex] < numArray[smallest])
                    {
                        smallest = innerIndex;
                    }
                }

                // Swap the smallest integer with the integer at the head of the unordered subarray <numArray[index]>
                int temp = numArray[smallest];
                numArray[smallest] = numArray[index];
                numArray[index] = temp;
            }
            return numArray;
        }

        private static int[] QuickSort(int[] numArray)
        {
            // In order to fit the schema of my processing loop, the <QuickSort> function must call a more specialized function
            // that can handle recursive calls to itself
            numArray = QuickSortProcess(numArray, 0, numArray.Length - 1);

            return numArray;
        }

        private static int[] QuickSortProcess(int[] numArray, int Left, int Right)
        {
            int LeftBound = Left;
            int RightBound = Right;
            int divider = numArray[(Left + Right) / 2];

            while (LeftBound <= RightBound)
            {
                while (numArray[LeftBound] < divider)
                {
                    LeftBound++;
                }

                while (numArray[RightBound] > divider)
                {
                    RightBound--;
                }

                if (LeftBound <= RightBound)
                {
                    var temp = numArray[LeftBound];
                    numArray[LeftBound] = numArray[RightBound];
                    numArray[RightBound] = temp;

                    LeftBound++;
                    RightBound--;
                }
            }

            if (Left < RightBound)
                QuickSortProcess(numArray, Left, RightBound);
            if (LeftBound < Right)
                QuickSortProcess(numArray, LeftBound, Right);

            return numArray;
        }

        private static int[] BubbleSort(int[] numArray)
        {
            // loops through entire array (N) times where (N) equals <MAX>
            for (int index = 0; index <= numArray.Length - 2; index++)
            {
                // loops through array (N^2) times where (N) is equal to <MAX>
                for (int indexTwo = 0; indexTwo <= numArray.Length - 2; indexTwo++)
                {
                    if (numArray[indexTwo] > numArray[indexTwo + 1])
                    {
                        int temp = numArray[indexTwo + 1];
                        numArray[indexTwo + 1] = numArray[indexTwo];
                        numArray[indexTwo] = temp;
                    }
                }
            }
            return numArray;
        }

        private static int[] InsertionSort(int[] numArray)
        {
            // Loops through entire array
            for (int index = 1; index < numArray.Length; index++)
            {
                int value = numArray[index];
                bool flag = false;

                for (int indexTwo = index - 1; indexTwo >= 0 && !flag;)
                {
                    if (value < numArray[indexTwo])
                    {
                        numArray[indexTwo + 1] = numArray[indexTwo];
                        indexTwo--;
                        numArray[indexTwo + 1] = value;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }

            return numArray;
        }

        #endregion

        #region Setup <array>
        // fills an array of <MAX> integers with unique numbers between 0 and <range>
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

        #endregion
    }
}
