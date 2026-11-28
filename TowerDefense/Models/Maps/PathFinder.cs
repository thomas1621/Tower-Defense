using System;
using System.Collections.Generic;

namespace TowerDefense.Models.Maps;

// Lit la carte et construit le chemin des ennemis, du départ (S) jusqu'à la base (X).
// Suppose que le chemin n'a pas de bifurcation.
public class PathFinder
{
    // Les 4 directions possibles : droite, gauche, bas, haut
    private static readonly (int Dx, int Dy)[] Directions =
    {
        (1, 0), (-1, 0), (0, 1), (0, -1)
    };

    public EnemyPath FindPath(Map map)
    {
        Tile current = FindStart(map);
        Tile? previous = null;
        List<Position> points = new List<Position>();

        while (true)
        {
            points.Add(new Position(current.X, current.Y));

            if (current is EndTile)
                break;

            Tile next = FindNext(map, current, previous);
            previous = current;
            current = next;
        }

        return new EnemyPath(points);
    }

    private Tile FindStart(Map map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                Tile tile = map.GetTile(x, y);
                if (tile is StartTile)
                    return tile;
            }
        }
        throw new InvalidOperationException("La carte ne contient pas de case de départ (S).");
    }

    private Tile FindNext(Map map, Tile current, Tile? previous)
    {
        foreach (var (dx, dy) in Directions)
        {
            int nx = current.X + dx;
            int ny = current.Y + dy;

            if (!map.IsInside(nx, ny))
                continue;

            Tile neighbour = map.GetTile(nx, ny);
            if (neighbour.IsWalkable && neighbour != previous)
                return neighbour;
        }
        throw new InvalidOperationException(
            $"Le chemin s'interrompt en ({current.X}, {current.Y}) sans atteindre la base (X).");
    }
}