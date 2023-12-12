using System;

class Program
{
    static void Main()
    {
        int N = 10;
        Console.WriteLine($"\nФибоначчи({N}) разными функциями:");
        Console.WriteLine($"fib_td({N}) = {Fibonacci(N, "TD")}");
        Console.WriteLine($"fib_bu({N}) = {Fibonacci(N, "BU")}");
        Console.WriteLine($"fib_bu_improved({N}) = {Fibonacci(N, "BU_I")}");
    }

    static int Fibonacci(int n, string func = "TD")
    {
        int[] f = new int[n + 1];

        int FibTD(int k)
        {
            if (k <= 1)
                f[k] = k;
            else
                f[k] = FibTD(k - 1) + FibTD(k - 2);
            return f[k];
        }

        int FibBU(int k)
        {
            int[] fib = new int[k + 1];
            fib[0] = 0;
            fib[1] = 1;
            for (int i = 2; i <= k; i++)
                fib[i] = fib[i - 1] + fib[i - 2];
            return fib[k];
        }

        int FibBUImproved(int k)
        {
            if (k <= 1)
                return k;
            int prev = 0, curr = 1;
            for (int i = 1; i < k; i++)
            {
                int temp = curr;
                curr = prev + curr;
                prev = temp;
            }
            return curr;
        }

        switch (func)
        {
            case "TD":
                f = new int[n + 1];
                return FibTD(n);
            case "BU":
                return FibBU(n);
            case "BU_I":
                return FibBUImproved(n);
            default:
                Console.WriteLine($"Неизвестная функция {func}");
                Environment.Exit(1);
                return 0; // Эта строка не выполняется, но нужна для компиляции
        }
    }
}
