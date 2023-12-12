using System;

class Program
{
    static void Main()
    {
        int W = 10;
        int[] weight = { 6, 3, 4, 2 };
        int[] cell = { 30, 14, 16, 9 };

        var result = KnapsackBU(W, weight, cell);
        Console.WriteLine($"С повторениями:: {result.WithRep}\n Без повторений: {result.WithoutRep.Value}");
        Console.WriteLine("Реконструированное решение: [" + string.Join(", ", result.WithoutRep.Solution) + "]");
    }

    static (int WithRep, (int Value, int[] Solution) WithoutRep) KnapsackBU(int W, int[] weight, int[] cell)
    {
        int KnapsackWithReps(int w, int[] weight, int[] cell)
        {
            int[] d = new int[W + 1];
            for (int i = 1; i <= W; i++)
            {
                for (int j = 0; j < weight.Length; j++)
                {
                    if (weight[j] <= i)
                        d[i] = Math.Max(d[i], d[i - weight[j]] + cell[j]);
                }
            }
            return d[W];
        }

        (int, int[]) KnapsackWithoutReps(int W, int[] weight, int[] cell)
        {
            int[,] d = new int[W + 1, weight.Length + 1];
            int[,] solution = new int[W + 1, weight.Length + 1];

            for (int i = 1; i <= W; i++)
            {
                for (int j = 1; j <= weight.Length; j++)
                {
                    d[i, j] = d[i, j - 1];
                    if (weight[j - 1] <= i)
                    {
                        int newValue = d[i - weight[j - 1], j - 1] + cell[j - 1];
                        if (newValue > d[i, j])
                        {
                            d[i, j] = newValue;
                            solution[i, j] = 1;
                        }
                    }
                }
            }

            int[] reconstructedSolution = new int[weight.Length];
            int W_remaining = W;
            for (int j = weight.Length; j > 0; j--)
            {
                if (solution[W_remaining, j] == 1)
                {
                    reconstructedSolution[j - 1] = 1;
                    W_remaining -= weight[j - 1];
                }
            }

            return (d[W, weight.Length], reconstructedSolution);
        }

        int withRep = KnapsackWithReps(W, weight, cell);
        var withoutRep = KnapsackWithoutReps(W, weight, cell);

        return (withRep, withoutRep);
    }
}
