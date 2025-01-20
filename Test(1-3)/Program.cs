using System;

class Program
{
    static void Main()
    {   //Вводим количество игроков за столом
        Console.Write("Введите количество игроков: ");
        int n = int.Parse(Console.ReadLine());

        int[] chips = new int[n];
        Console.WriteLine("Введите количество фишек для каждого игрока через пробел:");

        // Вводим количество фишек для каждого игрока
        string input = Console.ReadLine();
        string[] inputs = input.Split(' ');
        //Проверка ввода данных
        if (inputs.Length != n)
        {
            Console.WriteLine("Ошибка: количество введенных значений не совпадает с количеством игроков.");
            return;
        }

        for (int i = 0; i < n; i++)
        {
            chips[i] = int.Parse(inputs[i]);
        }

        try
        {
            int moves = CalculateMinimumMoves(chips);
            Console.WriteLine($"Минимальное количество ходов для равномерного распределения фишек: {moves}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static int CalculateMinimumMoves(int[] chips)
    {
        int totalChips = 0;
        int n = chips.Length;

        // Считаем общее количество фишек
        foreach (var chip in chips)
        {
            totalChips += chip;
        }

        // Проверяем, можно ли равномерно распределить фишки
        if (totalChips % n != 0)
        {
            throw new InvalidOperationException("Невозможно равномерно распределить фишки.");
        }

        int target = totalChips / n;
        int moves = 0;

        // Перераспределяем фишки
        for (int i = 0; i < n; i++)
        {
            while (chips[i] != target)
            {
                // Находим индексы соседей
                if (chips[i] > target)
                {
                    int nextIndex = (i + 1) % n;
                    int prevIndex = (i - 1 + n) % n;

                    // Выбираем, кому отдать фишку (соседу с меньшим количеством фишек)
                    if (chips[nextIndex] < chips[prevIndex])
                    {
                        // Передаем фишку следующему
                        chips[i]--;
                        chips[nextIndex]++;
                    }
                    else
                    {
                        // Передаем фишку предыдущему
                        chips[i]--;
                        chips[prevIndex]++;
                    }
                    moves++;
                }
                else
                {
                    // Находим индексы соседей
                    int nextIndex = (i + 1) % n;
                    int prevIndex = (i - 1 + n) % n;

                    if (chips[nextIndex] > chips[prevIndex])
                    {
                        // Берем фишку у следующего
                        chips[i]++;
                        chips[nextIndex]--;
                    }
                    else
                    {
                        // Берем фишку у предыдущего
                        chips[i]++;
                        chips[prevIndex]--;
                    }
                    moves++;
                }
            }
        }

        return moves;
    }
}