using System;

namespace TowerDefense.Models.Maps;

public readonly record struct Position(double X, double Y)
{
    public double DistanceTo(Position other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}