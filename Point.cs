using System.Collections.Generic;
using UnityEngine;

public class Point {
    public const float DIAGONALCOST = 1.41421356237f;
    public static Vector2[] Directions = new Vector2[8]{
            new Vector2(-1, 1), //NW
            new Vector2(0, 1),  //N
            new Vector2(1, 1),  //NE
            new Vector2(-1, 0), //W
            new Vector2(1, 0),  //E
            new Vector2(-1, -1),//SW
            new Vector2(0, -1), //S
            new Vector2(1, -1)  //SE
    };
    public Vector2 Position { get; private set; }
    public Dictionary<Vector2, Point> Neighbours { get; private set; }
    public Dictionary<Point, float> Connections { get; private set; }
    public bool IsTaken { get; private set; }

    public Point(Vector2 position) {
        this.Position = position;
    }

    public void SetNeighbour(Vector2 direction, Point neighbour) {
        if(Neighbours is null) Neighbours = new Dictionary<Vector2, Point>();
        if(Neighbours.ContainsKey(direction)) return;
        Neighbours.Add(direction, neighbour);
    }

    public Point GetNeighbour(Vector2 direction) {
        Neighbours.TryGetValue(direction, out var point);
        return point;
    }

    public bool IsTraversable() {
        return !IsTaken;
    }

    public void SetConnections() {
        Connections = new Dictionary<Point, float>();
        foreach(var direction in Neighbours.Keys) {
            if(!Neighbours[direction].IsTraversable()) continue;
            Connections[Neighbours[direction]] = IsDiagonal(direction) ? DIAGONALCOST : 1f;
        }
    }

    private bool IsDiagonal(Vector2 direction) {
        return (direction == Directions[0]) || (direction == Directions[2]) || (direction == Directions[5]) || (direction == Directions[7]);
    }
}
