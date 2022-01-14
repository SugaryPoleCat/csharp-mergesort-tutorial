using System;

namespace csharp_mergesort_tutorial
{
    internal class Program
    {
        /// <summary>
        /// This will print out an array.
        /// </summary>
        /// <param name="numbers">The array to print</param>
        private static void arrPint(int[] numbers)
        {
            for(int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }
        }

        /// <summary>
        /// This will sort the array. What this does is that it starts with unsorted array, then divides it into half.
        /// Then it works those halves into smaller halves and so on until it reaches an array of length less than < 2.
        /// Then it sorts and merges those arrays into one again, in proper order.
        /// It is supposedly extremely fast.
        /// </summary>
        /// <param name="numbers"></param>
        private static void mergeSort(int[] numbers)
        {
            int numbersLength = numbers.Length;
            if(numbersLength < 2) { return; }

            // we need to get the index where the middle of the array is
            int midIndex = numbersLength / 2;
            int[] leftHalf = new int[midIndex];
            // then we need to get right half, which starts from middle index.
            // so we have to take the whole array length - mid index to get the middle start.
            int[] rightHalf = new int[numbersLength - midIndex];

            // fill up arrays.
            // first left half
            for(int i = 0; i < midIndex; i++)
            {
                leftHalf[i] = numbers[i];
            }
            // right half
            for(int i = midIndex; i < numbersLength; i++)
            {
                // we have to correct so that we dont start from index 23, 34, or wherever we will start.
                // to correct that and start from 0, we have to - midIndex, since thats our i = ;
                rightHalf[i - midIndex] = numbers[i];
            }

            // now run this recursively until we run out of arrrays to split.
            mergeSort(leftHalf);
            mergeSort(rightHalf);


            // now perform the critical function, of merging!
            merge(numbers, leftHalf, rightHalf);
        }

        private static void merge(int[] inputArray, int[] leftHalf, int[] rightHalf)
        {
            int leftSize = leftHalf.Length;
            int rightSize = rightHalf.Length;

            // register indexes for arrays.
            // i, left, j right, k inputArray
            int i = 0;
            int j = 0;
            int k = 0;

            // now sort them!
            while(i < leftSize && j < rightSize)
            {
                // check if left array is lower.
                if(leftHalf[i] <= rightHalf[j])
                {
                    // if they are, input left half in!
                    inputArray[k] = leftHalf[i];
                    // and increase its index!
                    i++;
                } 
                // if left half is NOT bigger than right
                else
                {
                   // input right side!
                    inputArray[k] = rightHalf[j];
                    j++;
                }
                // increase K everytime this loop runs
                k++;
            }

            // in case we have stragglers, make sure to also handle them!
            while(i < leftSize)
            {
                // increase index for both obviously.
                inputArray[k] = leftHalf[i];
                i++;
                k++;
            }
            // and also handle right side
            while(j < rightSize)
            {
                inputArray[k] = rightHalf[j];
                j++;
                k++;
            }
        }

        static void printTime(DateTime a, DateTime b, string name)
        {
            TimeSpan time = b.Subtract(a);
            Console.WriteLine($"\nTime it took from {name}: {time.TotalMinutes.ToString("0.00")} minutes and {time.TotalSeconds.ToString("0.00")} seconds.");
        }

        static void Main(string[] args)
        {
            Random random = new Random();

            int amount = 100000000;

            int[] arr_numbers = new int[amount];

            DateTime init = DateTime.Now;
            Console.WriteLine($"We started program at: {init}\n\nNow we fill the array with a bunch of numbers\n\n\n");

            for (int i = 0; i < arr_numbers.Length; i++)
            {
                arr_numbers[i] = random.Next(amount);
            }

            // get start date and print
            DateTime start = DateTime.Now;
            printTime(init, start, "application start to registering array full of numbers");
            Console.WriteLine($"\n\nActual mergeSort starts now: {start}\n");

            // do the thing
            mergeSort(arr_numbers);

            // then print finish date. WITH seconds.
            DateTime end = DateTime.Now;
            Console.WriteLine($"\n\n\nFinshed: {end}");

            // time it took from the init
            printTime(init, end, "initialization");

            // time it took from sort start
            printTime(start, end, "sorting");

            Console.ReadKey();
        }
    }
}
