using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> a = new List<int> { 7, 2, 1, 3, 8, 4, 9, 1, 2, 6, 5, 9, 3, 8, 1 };

        var result = ListBottomUp2(a);

        Console.WriteLine($"Длина самой длинной возрастающей подпоследовательности: {result.Item1}");

        Console.Write("Использование предыдущего списка: ");
        Console.WriteLine($"[{string.Join(", ", result.Item2.Item1)}]");

        Console.Write("Без использования предыдущего списка: ");
        Console.WriteLine($"[{string.Join(", ", result.Item2.Item2)}]");
    }

    static (int, (List<int>, List<int>)) ListBottomUp2(List<int> a)
    {
        List<int> d = new List<int>();
        List<int> prev = new List<int>();

        for (int i = 0; i < a.Count; i++)
        {
            d.Add(1);
            prev.Add(-1);
            for (int j = 0; j < i; j++)
            {
                if (a[j] < a[i] && d[j] + 1 > d[i])
                {
                    d[i] = d[j] + 1;
                    prev[i] = j;
                }
            }
        }

        int ans = 0, maxIndex = 0;
        for (int i = 0; i < d.Count; i++)
        {
            if (ans < d[i])
            {
                ans = d[i];
                maxIndex = i;
            }
        }

        List<int> listUsingPrev = RestoreUsingPrev(prev, maxIndex);
        List<int> listWithoutPrev = RestoreWithoutPrev(ans, maxIndex, d, a);

        return (ans, (listUsingPrev, listWithoutPrev));
    }

    static List<int> RestoreUsingPrev(List<int> prev, int maxIndex)
    {
        List<int> result = new List<int>();
        while (true)
        {
            result.Add(maxIndex);
            if (prev[maxIndex] == -1)
                break;
            maxIndex = prev[maxIndex];
        }
        result.Reverse();
        return result;
    }

    static List<int> RestoreWithoutPrev(int ans, int maxIndex, List<int> d, List<int> a)
    {
        List<int> result = new List<int>();
        while (true)
        {
            result.Add(maxIndex);
            if (ans == 1)
                break;
            ans--;

            while (true)
            {
                maxIndex--;
                if (maxIndex < 0) break; // Добавлено условие для предотвращения выхода за границы массива
                if (d[maxIndex] == ans && a[maxIndex] < a[result[result.Count - 1]])
                    break;
            }
        }
        result.Reverse();
        return result;
    }
}
