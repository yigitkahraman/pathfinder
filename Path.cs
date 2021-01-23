using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path {
    public readonly List<Point> points;

    public Path() {
        points = new List<Point>();
    }

    public Path(List<Point> points) {
        this.points = points;
    }

    public bool Contains(Point point) => points.Contains(point);
    public void Reverse() => points.Reverse();
    public Point GetStartPoint() => points == null ? null : points[0];
    public Point GetEndPoint() => points == null ? null : points[points.Count - 1];
    public List<Vector2> GetPositions() => points.Select(x => x.Position).ToList();

    public void Add(Point point) {
        if(Contains(point)) return;
        points.Add(point);
    }

    public float GetCost() {
        var cost = 0f;
        for(int i = 0; i < points.Count - 1; i++) {
            cost += points[i].Connections[points[i + 1]];
        }
        return cost;
    }
}
