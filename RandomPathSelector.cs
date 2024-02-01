using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public static class PathFinder
{
    public static NavMeshPath FindRandomPath(NavMeshAgent agent, Vector3 destination, float pathAlterationRadius = 2f, int maxPathsToConsider = 5)
    {
        List<NavMeshPath> paths = new List<NavMeshPath>();
        List<float> pathLengths = new List<float>();

        for (int i = 0; i < maxPathsToConsider; i++)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 alteredDestination = destination + Random.insideUnitSphere * pathAlterationRadius;
            alteredDestination.y = destination.y;

            if (agent.CalculatePath(alteredDestination, path))
            {
                paths.Add(path);
                pathLengths.Add(CalculatePathLength(path));
            }
        }

        if (paths.Count == 0) return null;

        int shortestPathIndex = FindShortestPathIndex(pathLengths);
        return paths[shortestPathIndex];
    }

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
}
