using System;
using System.Text;

namespace MatrixRaining
{
    class Program
    {
        public class Queue
        {
            public int i;
            public int j;
            public int Length;
            public int Speed;
        }

        public static int thinness = 6;

        static void Main(string[] args)
        {
            int column = 30, row = 30;
            Console.Title = "matrix";
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(2 * column, row + 1);
            int time = 10, time2 = 9, time3 = 9;
            bool control = true, at_queue = false, at_last = false, at_between = false;
            char[,] matrix = new char[row, column];
            Queue[] Queues = new Queue[2 * column];
            Random random = new Random();

            for (int k = 0; k < Queues.Length; k++) { Queues[k] = new Queue(); }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = (char)random.Next(48, 58);
                }
            }

            for (int k = 0; k < Queues.Length; k++)
            {
                Queues[k].i = random.Next(matrix.GetLength(0)) - random.Next(matrix.GetLength(0));
                Queues[k].j = random.Next(matrix.GetLength(1));
                Queues[k].Length = random.Next(matrix.GetLength(0) / 10, 3 * matrix.GetLength(0) / 4);
                Queues[k].Speed = random.Next(1, 4);
            }

            while (control)
            {
                if (time % 1 == 0)
                {
                    Console.SetCursorPosition(0, 0);

                    for (int k = 0; k < Queues.Length; k++)
                    {
                        if (Queues[k].i - Queues[k].Length > matrix.GetLength(0))
                        {
                            Queues[k].i = random.Next(matrix.GetLength(0)) - random.Next(matrix.GetLength(0));
                            Queues[k].j = random.Next(matrix.GetLength(1));
                            Queues[k].Length = random.Next(matrix.GetLength(0) / 10, 3 * matrix.GetLength(0) / 4);
                            Queues[k].Speed = random.Next(1, 4);
                        }
                        else
                        { Queues[k].i += Queues[k].Speed; }

                    }

                    for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix[matrix.GetLength(0) - i - 1, matrix.GetLength(1) - j - 1] = matrix[matrix.GetLength(0) - i - 2, matrix.GetLength(1) - j - 1];
                            if (i == 0) { matrix[i, j] = (char)random.Next(48, 58); }

                            Define(i, j, ref Queues, ref at_queue, ref at_last, ref at_between);

                            if (at_queue)
                            {
                                if (at_last) { Write(matrix[i, j] + " ", ConsoleColor.White); }
                                else
                                {
                                    if (at_between) { Write(matrix[i, j] + " ", ConsoleColor.Green); }
                                    else { Write(matrix[i, j] + " ", ConsoleColor.DarkGreen); }
                                }
                            }
                            else
                            { Write(matrix[i, j] + " ", ConsoleColor.Black); }

                        }
                        Console.WriteLine();
                    }
                }
                if (--time < 0) { time += 10; if (--time2 < 0) { time2 += 10; if (--time3 < 0) { control = false; time = 0; time2 = 0; time3 = 0; } } }
                if (time % 5 == 0) { Write("\n" + time + ":" + time2 + ":" + time3, ConsoleColor.DarkGray); }
                else { Write("\n" + time + " " + time2 + " " + time3, ConsoleColor.DarkGray); }
            }

            Console.ReadKey();
        }

        static void Write(string s, ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            Console.Write(s);
        }

        static void Define(int x, int y, ref Queue[] queues, ref bool at_queue, ref bool at_last, ref bool at_between)
        {
            at_queue = false;
            at_last = false;
            at_between = false;

            for (int k = 0; k < queues.Length; k++)
            {
                if (queues[k].i >= 0)
                {
                    if (queues[k].i > x && queues[k].i - queues[k].Length < x && queues[k].j == y)
                    {
                        at_queue = true;
                        if (queues[k].i - (queues[k].Length / thinness) > x && queues[k].i - ((thinness - 1) * queues[k].Length / thinness) < x)
                        { at_between = true; break; }
                        else { at_last = true; break; }
                    }
                }
            }
        }
    }
}