using System.Collections.Generic;
using UnityEngine;

public class Grid {
    public int width;
    public int height;
    public Dictionary<Vector2, Point> points;

    public Grid(int width, int height) {
        this.width = width;
        this.height = height;
        this.Create();
    }

    private void Create() {
        points = new Dictionary<Vector2, Point>(width * height);
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                var position = new Vector2(x, y);
                var point = new Point(position);
                points[position] = point;
            }
        }
        foreach(var point in points.Values) {
            foreach(var direction in Point.Directions) {
                var neighbour = GetPoint(point.Position + direction);
                if(neighbour == null) continue;
                point.SetNeighbour(direction, neighbour);
            }
        }
        foreach(var point in points.Values) {
            point.SetConnections();
        }
    }

    public Point GetPoint(Vector2 position) {
        var x = Mathf.RoundToInt(position.x);
        var y = Mathf.RoundToInt(position.y);
        points.TryGetValue(new Vector2(x, y), out var point);
        return point;
    }

    public Point GetPoint(int x, int y) {
        var clampedX = Mathf.Clamp(x, 0, width - 1);
        var clampedY = Mathf.Clamp(y, 0, height - 1);
        return GetPoint(new Vector2(clampedX, clampedY));
    }
}
