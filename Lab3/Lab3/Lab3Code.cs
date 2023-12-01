namespace Lab3
{
    public class Lab3Code
    {
        public static void Run()
        {
            string inputFilename = @"C:\\Users\\Admin\\source\\repos\\CrossLabs\\Lab3\\Lab3\\Input.txt";
            string outputFilename = @"C:\\Users\\Admin\\source\\repos\\CrossLabs\\Lab3\\Lab3\\Output.txt";

            using (StreamReader reader = new StreamReader(inputFilename))
            {
                int N = int.Parse(reader.ReadLine());
                int[,] matrix = new int[N, N];

                for (int i = 0; i < N; i++)
                {
                    string[] line = reader.ReadLine().Split();
                    for (int j = 0; j < N; j++)
                    {
                        matrix[i, j] = int.Parse(line[j]);
                    }
                }

                int result = CountOneWayRoads(matrix);

                using (StreamWriter writer = new StreamWriter(outputFilename))
                {
                    writer.Write(result);
                }
            }
        }

        public static int CountOneWayRoads(int[,] matrix)
        {
            int count = 0;
            int N = matrix.GetLength(0);

            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (matrix[i, j] == 1 && matrix[j, i] == 1)
                    {
                        matrix[i, j] = 0;
                        if (!IsReachable(matrix, N))
                        {
                            count++;
                        }
                        matrix[i, j] = 1;
                    }
                }
            }

            return count;
        }

        public static bool IsReachable(int[,] matrix, int N)
        {
            bool[] visited = new bool[N];
            DFS(matrix, 0, visited);
            return Array.TrueForAll(visited, v => v);
        }

        public static void DFS(int[,] matrix, int vertex, bool[] visited)
        {
            visited[vertex] = true;
            for (int i = 0; i < visited.Length; i++)
            {
                if (matrix[vertex, i] == 1 && !visited[i])
                {
                    DFS(matrix, i, visited);
                }
            }
        }
    }
}