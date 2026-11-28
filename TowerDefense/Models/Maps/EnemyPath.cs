using System;
using System.Collections.Generic;

namespace TowerDefense.Models.Maps;

// Chemin suivi par les ennemis, du départ (S) à la base (X).
// Les points doivent être des cases voisines : chaque morceau fait exactement 1 case.
// C'est garanti par le PathFinder.
public class EnemyPath
{
    private readonly IReadOnlyList<Position> _points;

    public double Length { get; }
    public Position Start => _points[0];

    public EnemyPath(IReadOnlyList<Position> points)
    {
        if (points.Count < 2)
            throw new ArgumentException("Un chemin doit contenir au moins deux points.");

        for (int i = 1; i < points.Count; i++)
        {
            if (points[i - 1].DistanceTo(points[i]) != 1)
                throw new ArgumentException($"Les points {i - 1} et {i} ne sont pas des cases voisines.");
        }

        _points = points;
        Length = points.Count - 1;
    }

    public Position GetPositionAt(double distance)
    {
        if (distance <= 0)
            return _points[0];
        if (distance >= Length)
            return _points[^1];

        int index = (int)distance;      // sur quel morceau se trouve l'ennemi
        double t = distance - index;    // où sur ce morceau (entre 0 et 1)

        Position from = _points[index];
        Position to = _points[index + 1];

        return new Position(
            from.X + (to.X - from.X) * t,
            from.Y + (to.Y - from.Y) * t);
    }
}