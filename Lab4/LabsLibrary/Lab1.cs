using System;
using System.IO;

namespace LabsLibrary
{
    public class Lab1
    {
        private string inputFilename;
        private string outputFilename;

        public Lab1(string inputFilename, string outputFilename)
        {
            this.inputFilename = inputFilename;
            this.outputFilename = outputFilename;
        }

        public void Run()
        {
            var input = ReadInput(inputFilename);
            if (input != null)
            {
                int result = MaxHoles(input.Item1, input.Item2, input.Item3, input.Item4);
                Console.WriteLine($"Result: {result}");
                WriteOutput(outputFilename, result);
            }
        }

        private int IntersectionArea(int[] rect1, int[] rect2)
        {
            int xOverlap = Math.Max(0, Math.Min(rect1[2], rect2[2]) - Math.Max(rect1[0], rect2[0]));
            int yOverlap = Math.Max(0, Math.Min(rect1[3], rect2[3]) - Math.Max(rect1[1], rect2[1]));
            return xOverlap * yOverlap;
        }

        private Tuple<int, int, int[][], int[][]> ReadInput(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string[] sizes = reader.ReadLine().Split();
                    int N = int.Parse(sizes[0]);
                    int M = int.Parse(sizes[1]);

                    int K1 = int.Parse(reader.ReadLine());
                    int[][] holes1 = new int[K1][];
                    for (int i = 0; i < K1; i++)
                    {
                        holes1[i] = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
                    }

                    int K2 = int.Parse(reader.ReadLine());
                    int[][] holes2 = new int[K2][];
                    for (int i = 0; i < K2; i++)
                    {
                        holes2[i] = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
                    }

                    return Tuple.Create(N, M, holes1, holes2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading input: {ex.Message}");
                return null;
            }
        }

        private int MaxHoles(int N, int M, int[][] holes1, int[][] holes2)
        {
            int maxHolesCount = 0;

            foreach (var rect1 in holes1)
            {
                foreach (var rect2 in holes2)
                {
                    int xOverlap = Math.Max(0, Math.Min(rect1[2], rect2[2]) - Math.Max(rect1[0], rect2[0]));
                    int yOverlap = Math.Max(0, Math.Min(rect1[3], rect2[3]) - Math.Max(rect1[1], rect2[1]));

                    // Рахуємо площу перетину
                    int areaOverlap = xOverlap * yOverlap;

                    // Збільшуємо лічильник дірок на площу перетину
                    maxHolesCount += areaOverlap;
                }
            }

            return maxHolesCount;
        }



        private void WriteOutput(string filename, int result)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing output: {ex.Message}");
            }
        }
    }
}
