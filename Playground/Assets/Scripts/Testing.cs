using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    void Start()
    {
    }

    /// <summary>
    /// Compute the distance between two strings.
    /// </summary>
    private int LevenshteinDistance(string firstWord, string secondWord)
    {
        int firstLength = firstWord.Length;
        int secondLength = secondWord.Length;
        int[,] d = new int[firstLength + 1, secondLength + 1];

        if (firstLength == 0)
            return secondLength;
        if (secondLength == 0)
            return firstLength;

        for (int i = 0; i <= firstLength; d[i, 0] = i++) { }
        for (int j = 0; j <= secondLength; d[0, j] = j++) { }

        for (int i = 1; i <= firstLength; ++i)
        {
            for (int j = 1; j <= secondLength; ++j)
            {
                int cost = (secondWord[j - 1] == firstWord[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[firstLength, secondLength];
    }

    /// <summary>
    /// Optimized Fibonacci sequence using dynamic programming.
    /// </summary>
    /// <param name="cache">Hold value of previous numbers</param>
    /// <param name="sequenceLength"></param>
    /// <returns></returns>
    private long DynamicFibonacci(ref long[] cache, long sequenceLength)
    {
        if (sequenceLength <= 1)
            cache[sequenceLength] = 1;
        if (cache[sequenceLength] == 0)
            cache[sequenceLength] = DynamicFibonacci(ref cache, sequenceLength - 1) + DynamicFibonacci(ref cache, sequenceLength - 2);
        return cache[sequenceLength];
    }

    /// <summary>
    /// Standard Fibonacci algorithm using recursion.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static long RecursiveFibonacci(long n)
    {
        if (n <= 1)
            return 1;
        return RecursiveFibonacci(n - 1) + RecursiveFibonacci(n - 2);
    }

}
