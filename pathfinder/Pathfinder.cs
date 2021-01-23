using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder {
    private Grid grid;

    public Pathfinder(Grid grid) {
        this.grid = grid;
    }

    public Path FindPath(Point origin, Point destination) {
        //Normally I dont use this like this. Point Connection should be cached to increase performance.
        foreach(var point in grid.points.Values) {
            point.SetConnections();
        }
        //
        var frontier = new HeapPriorityQueue<Point>();
        var cameFrom = new Dictionary<Point, Point>();
        var costSoFar = new Dictionary<Point, float>();
        frontier.Enqueue(origin, 0);
        cameFrom.Add(origin, default);
        costSoFar.Add(origin, 0);
        while(frontier.Count != 0) {
            var current = frontier.Dequeue();
            if(current == destination) break;
            foreach(var next in current.Connections.Keys) {
                var cost = costSoFar[current] + current.Connections[next];
                if(costSoFar.ContainsKey(next) && cost >= costSoFar[next]) continue;
                costSoFar[next] = cost;
                var priority = cost + GetEuclideanDistance(next, destination);
                frontier.Enqueue(next, priority);
                cameFrom[next] = current;
            }
        }
        var path = new Path();
        if(!cameFrom.ContainsKey(destination)) return path;
        path.Add(destination);
        var temp = destination;
        while(temp != origin) {
            var waypoint = cameFrom[temp];
            path.Add(waypoint);
            temp = waypoint;
        }
        if(!path.Contains(origin)) path.Add(origin);
        path.Reverse();
        return path;
    }

    public List<Point> GetEuclideanRange(Point origin, int distance) {
        var range = new List<Point>();
        for(int x = -distance; x <= distance; x++) {
            for(int y = -distance; y <= distance; y++) {
                var point = grid.GetPoint(origin.Position + new Vector2(x, y));
                if(point == null || point == origin) continue;
                if(GetEuclideanDistance(origin, point) > distance * Point.DIAGONALCOST) continue;
                range.Add(point);
            }
        }
        return range.Distinct().ToList();
    }

    public List<Point> GetEuclideanRange(Point origin, int minDistance, int maxDistance) {
        var range = GetEuclideanRange(origin, maxDistance);
        var inner = GetEuclideanRange(origin, minDistance);
        return range.Except(inner).Distinct().ToList();
    }

    public float GetEuclideanDistance(Point a, Point b) {
        return Mathf.Sqrt(Mathf.Pow(a.Position.x - b.Position.x, 2) + Mathf.Pow(a.Position.y - b.Position.y, 2));
    }

    public float GetManhattanDistance(Point a, Point b) {
        return Mathf.Abs(a.Position.x - b.Position.x) + Mathf.Abs(a.Position.y - b.Position.y);
    }
}
