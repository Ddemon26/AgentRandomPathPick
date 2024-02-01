using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/// <summary>
/// Provides functionality to find paths for NavMesh agents.
/// </summary>
public static class PathFinder
{
    /// <summary>
    /// Finds a random path to a destination within a specified alteration radius.
    /// </summary>
    /// <param name="agent">The NavMesh agent that is navigating.</param>
    /// <param name="destination">The target destination.</param>
    /// <param name="pathAlterationRadius">Radius within which the destination can be altered to find a path.</param>
    /// <param name="maxPathsToConsider">The maximum number of paths to consider.</param>
    /// <returns>The most optimal NavMeshPath found, or null if no path is found.</returns>
    public static NavMeshPath FindRandomPath(NavMeshAgent agent, Vector3 destination, float pathAlterationRadius = 2f, int maxPathsToConsider = 5)
    {
        var paths = new List<NavMeshPath>();
        var pathLengths = new List<float>();

        for (int i = 0; i < maxPathsToConsider; i++)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 alteredDestination = GetAlteredDestination(destination, pathAlterationRadius);
            
            if (agent.CalculatePath(alteredDestination, path))
            {
                paths.Add(path);
                pathLengths.Add(CalculatePathLength(path));
            }
        }

        return paths.Count > 0 ? paths[FindShortestPathIndex(pathLengths)] : null;
    }

    /// <summary>
    /// Calculates the index of the shortest path from a list of path lengths.
    /// </summary>
    /// <param name="pathLengths">A list of path lengths.</param>
    /// <returns>The index of the shortest path.</returns>
    private static int FindShortestPathIndex(List<float> pathLengths)
    {
        int index = 0;
        float shortestLength = float.MaxValue;

        for (int i = 0; i < pathLengths.Count; i++)
        {
            if (pathLengths[i] < shortestLength)
            {
                shortestLength = pathLengths[i];
                index = i;
            }
        }

        return index;
    }

    /// <summary>
    /// Calculates the total length of a given NavMeshPath.
    /// </summary>
    /// <param name="path">The NavMeshPath to calculate the length of.</param>
    /// <returns>The total length of the path.</returns>
    private static float CalculatePathLength(NavMeshPath path)
    {
        float length = 0;
        if (path.corners.Length < 2) return length;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return length;
    }

    /// <summary>
    /// Alters the destination within a specified radius.
    /// </summary>
    /// <param name="destination">Original destination.</param>
    /// <param name="radius">Radius within which to alter the destination.</param>
    /// <returns>An altered destination.</returns>
    private static Vector3 GetAlteredDestination(Vector3 destination, float radius)
    {
        Vector3 alteredDestination = destination + Random.insideUnitSphere * radius;
        alteredDestination.y = destination.y; // Keep the original y-coordinate to maintain altitude.
        return alteredDestination;
    }
}
