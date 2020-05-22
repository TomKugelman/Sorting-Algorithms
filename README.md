# Sorting-Algorithms

Author: Thomas Kugelman
Language: C#
Framework: .NET 4.2.7

A small collection of simple sorting algorithms of varying time complexity that showcase the importance of efficient algorithms.

Algorithms used {
Insertion Sort
Bubble Sort
Quick Sort
Merge Sort
Selection Sort
}

also includes Linear and Binary searches.

While this program still has room for improvement, its main goal is to show off how algorithms that are simple to implament 
generaly have longer runtimes. This can only be shown with large sets of data (in this program we have arrays of thousands of integers).

Algorithms of various time complexity have marginal differences in runtime at lower data sets, 
but often more complex and RUNTIME efficient algorithms like O(log N) take longer to implament 
and thus will not be TIME efficient to implament if the data sets are consistantly low (less than a thousand).

If you would like to change the size of the data being sorted, navigate to the top of the <main> function 
and change the local variables <range> and <MAX>. 

Please note that because this program uses only unique numbers (for the sake of clarity when looking at a sorted list)
<range> MUST be higher than <MAX>.