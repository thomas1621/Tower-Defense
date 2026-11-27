using System;
using System.IO;

namespace TowerDefense.Models.Maps;

public class MapLoader
{
    public Map Load(string filePath)
    {
        // 1. Lire toutes les lignes du fichier
        string[] lines;
        lines = File.ReadAllLines(filePath);

        // 2. Calculer la taille de la carte
        int height;
        height = lines.Length;

        int width;
        width = lines[0].Length;

        // 3. Créer une carte vide
        Map map;
        map = new Map(width, height);

        // 4. Parcourir chaque ligne, puis chaque lettre
        int y;
        y = 0;

        foreach (string line in lines)
        {
            if (line.Length != width)
            {
                throw new Exception("La ligne " + y + " n'a pas la même longueur que la première.");
            }

            int x;
            x = 0;

            foreach (char letter in line)
            {
                Tile tile;
                tile = CreateTile(letter, x, y);
                map.SetTile(tile);

                x = x + 1;      // colonne suivante
            }

            y = y + 1;          // ligne suivante
        }

        // 5. Rendre la carte remplie
        return map;
    }

    private Tile CreateTile(char letter, int x, int y)
    {
        switch (letter)
        {
            case 'G':
                return new GrassTile(x, y);
            case 'P':
                return new PathTile(x, y);
            case 'B':
                return new BuildableTile(x, y);
            case 'S':
                return new StartTile(x, y);
            case 'X':
                return new EndTile(x, y);
            default:
                throw new Exception("Lettre inconnue dans la carte : " + letter);
        }
    }
}