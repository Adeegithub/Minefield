using System;
using System.Collections.Generic;

public class Difficulty2
{
        public static void DifficultyTwo()
    {
        int[,] grid = {
            {0, 0, 1, 1, 0},
            {1, 1, 0, 1, 0},
            {0, 1, 1, 0, 1},
            {1, 0, 1, 0, 1},
            {0, 1, 0, 1, 1}
        };

        int startX = 0, startY = 0;
        int endX = 4, endY = 2;

        List<int[]> snifferPupPath = FindSafePath(grid, startX, startY, endX, endY);

        if (snifferPupPath.Count == 0)
        {
            Console.WriteLine("No safe path for Sniffer Pup found.");
        }
        else
        {
            Console.WriteLine("Safe path for Sniffer Pup found:");
            foreach (var step in snifferPupPath)
            {
                Console.WriteLine($"[{step[0]}, {step[1]}]");
            }

            List<int[]> allyPath = FindSafePathForAlly(grid, snifferPupPath);

            if (allyPath.Count == 0)
            {
                Console.WriteLine("No safe path for Ally found.");
            }
            else
            {
                Console.WriteLine("Safe path for Ally found:");
                foreach (var step in allyPath)
                {
                    Console.WriteLine($"[{step[0]}, {step[1]}]");
                }
            }
        }
    }

    // Directions for moving in all 8 possible ways
    private static readonly int[,] DIRECTIONS = {
        {-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, -1}, {1, 0}, {1, 1}  
    };

    public static List<int[]> FindSafePath(int[,] grid, int startX, int startY, int endX, int endY)
    {
        var path = new List<int[]>();
        var visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        if (DFS(grid, startX, startY, endX, endY, visited, path))
        {
            return path;
        }
        return new List<int[]>(); // Return an empty path if no path is found
    }

    private static bool DFS(int[,] grid, int x, int y, int endX, int endY, bool[,] visited, List<int[]> path)
    {
        if (x < 0 || y < 0 || x >= grid.GetLength(0) || y >= grid.GetLength(1) || grid[x, y] == 1 || visited[x, y])
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
            if (DFS(grid, nextX, nextY, endX, endY, visited, path))
            {
                return true;
            }
        }

        // Backtrack if no path is found
        path.RemoveAt(path.Count - 1);
        return false;
    }

    public static List<int[]> FindSafePathForAlly(int[,] grid, List<int[]> snifferPupPath)
    {
        var allyPath = new List<int[]>();
        var visited = new bool[grid.GetLength(0), grid.GetLength(1)];

        // Mark Sniffer Pup's path as visited
        foreach (var step in snifferPupPath)
        {
            visited[step[0], step[1]] = true;
        }

        // Start and end points for Ally
        var start = snifferPupPath[0];
        var end = snifferPupPath[snifferPupPath.Count - 1];

        if (DFSAlly(grid, start[0], start[1], end[0], end[1], visited, allyPath))
        {
            return allyPath;
        }
        return new List<int[]>(); // Return an empty path if no path is found
    }

    private static bool DFSAlly(int[,] grid, int x, int y, int endX, int endY, bool[,] visited, List<int[]> path)
    {
        if (x < 0 || y < 0 || x >= grid.GetLength(0) || y >= grid.GetLength(1) || grid[x, y] == 1 || visited[x, y])
        {
            return false;
        }

        // Check if the destination is reached
        if (x == endX && y == endY)
        {
            path.Add(new int[] { x, y });
            return true;
        }

        // Mark this cell as visited
        visited[x, y] = true;
        path.Add(new int[] { x, y });

        // Explore all 8 possible directions
        for (int i = 0; i < DIRECTIONS.GetLength(0); i++)
        {
            int nextX = x + DIRECTIONS[i, 0];
            int nextY = y + DIRECTIONS[i, 1];
            if (DFSAlly(grid, nextX, nextY, endX, endY, visited, path))
            {
                return true;
            }
        }

        // Backtrack if no path is found
        path.RemoveAt(path.Count - 1);
        return false;
    }
}

