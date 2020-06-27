# Sorting-Algorithms

Author: Thomas Kugelman

Language: C#

Framework: .NET 4.2.7

## Description

A small collection of simple sorting algorithms of varying time complexities that showcase the importance of efficient algorithms.

- All of these algorithms listed below were written by hand, no libraries or class methods were used to sort or search unless absolutely necessary

## Algorithms used 

- Insertion Sort
- Bubble Sort
- Quick Sort
- Merge Sort
- Selection Sort

- Linear Search
- Binary Search

## Notes

This program's main goal is to show off how algorithms that are simple to implement 
generaly have longer runtimes. This can only be shown with large sets of data (in this program we have arrays of thousands of integers).

Algorithms of various time complexity have marginal differences in runtime at lower data sets, 
but often more complex and RUNTIME efficient algorithms like O(log N) take longer to implement 
and thus will not be TIME efficient (how long it takes to code the algorithm) if the data sets are consistantly low (less than a thousand). We can often use built-in algorithms across many languages and libraries, however some processes need very specific custom algorithms to accomplish their task. Some cannot accept simple parameters like an INT or STRING. These are the situations where we need to seriously think about what algorithm is the most efficient overall.


### Usage

If you would like to change the size of the data being sorted, navigate to the top of the "main" function 
and change the local variables "range" and "MAX" under the "Constants" region. 

Please note that because this program uses only unique numbers (for the sake of clarity when looking at a sorted list)
"range" MUST be higher than "MAX".
