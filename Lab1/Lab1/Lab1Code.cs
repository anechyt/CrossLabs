
namespace Lab1
{
     public static class Lab1Code
     {
        static int IntersectionArea(int[] rect1, int[] rect2)
        {
            int xOverlap = Math.Max(0, Math.Min(rect1[2], rect2[2]) - Math.Max(rect1[0], rect2[0]));
            int yOverlap = Math.Max(0, Math.Min(rect1[3], rect2[3]) - Math.Max(rect1[1], rect2[1]));
            return xOverlap * yOverlap;
        }

        static Tuple<int, int, int[][], int[][]> ReadInput(string filename)
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

        static int MaxHoles(int[][] holes1, int[][] holes2)
        {
            int maxHolesCount = 0;

            foreach (var rect1 in holes1)
            {
                foreach (var rect2 in holes2)
                {
                    int intersection = IntersectionArea(rect1, rect2);
                    maxHolesCount = Math.Max(maxHolesCount, intersection);
                }
            }

            return maxHolesCount;
        }

        static void WriteOutput(string filename, int result)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(result);
            }
        }

        public static void Run()
        {
            string inputFilename = @"C:\\Users\\Admin\\source\\repos\\CrossLabs\\Lab1\\Lab1\\Input.txt";
            string outputFilename = @"C:\\Users\\Admin\\source\\repos\\CrossLabs\\Lab1\\Lab1\\Output.txt";

            var input = ReadInput(inputFilename);
            int result = MaxHoles(input.Item3, input.Item4);
            WriteOutput(outputFilename, result);
        }
    }
}