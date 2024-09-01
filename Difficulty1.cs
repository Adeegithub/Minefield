using System;
using System.Collections.Generic;

public class Difficulty1
{
    public static void Main()
    {
        int[,] graph = {
            {0, 0, 1, 1, 0},
            {1, 1, 0, 1, 0},
            {0, 1, 1, 0, 1},
            {1, 0, 1, 0, 1},
            {0, 1, 0, 1, 1}
        };

        int startX = 0, startY = 0;
        int endX = 4, endY = 2;

        List<int[]> path = FindSafePath(graph, startX, startY, endX, endY);

        if (path.Count == 0)
        {
            Console.WriteLine("No safe path Found");
        }
        else
        {
            Console.WriteLine("Safe path found:");
            foreach (var step in path)
            {
                Console.WriteLine($"[{step[0]}, {step[1]}]");
            }
        }

        Console.WriteLine();
        Console.WriteLine("*********** Difficulty 2 ***********");
         Console.WriteLine();

        Difficulty2.DifficultyTwo();
    }

    //Defining All Possible Moves
    private static readonly int[,] DIRECTIONS = {
        {-1, -1}, {-1, 0}, {-1, 1},{0, -1},{0, 1},{1, -1}, {1, 0}, {1, 1}
    };

    public static List<int[]> FindSafePath(int[,] graph, int startX, int startY, int endX, int endY)
    {
        var path = new List<int[]>();
        var visited = new bool[graph.GetLength(0), graph.GetLength(1)];
        if (DFS(graph, startX, startY, endX, endY, visited, path))
        {
            return path;
        }
        return new List<int[]>(); // Return an empty path if no path is found
    }

    private static bool DFS(int[,] graph, int x, int y, int endX, int endY, bool[,] visited, List<int[]> path)
    {
        if (x < 0 || y < 0 || x >= graph.GetLength(0) || y >= graph.GetLength(1) || graph[x, y] == 1 || visited[x, y])
        {
            return false;
        }

        // Checking whether the destination is reached.
        if (x == endX && y == endY)
        {
            path.Add(new int[] { x, y });
            return true;
        }

        // Marking visited cell
        visited[x, y] = true;
        path.Add(new int[] { x, y });

        // Search all 8 possible directions in order to move safely
        for (int i = 0; i < DIRECTIONS.GetLength(0); i++)
        {
            int nextX = x + DIRECTIONS[i, 0];
            int nextY = y + DIRECTIONS[i, 1];
            if (DFS(graph, nextX, nextY, endX, endY, visited, path))
            {
                return true;
            }
        }

        // Backtrack if no path is found
        path.RemoveAt(path.Count - 1);
        return false;
    }
}

